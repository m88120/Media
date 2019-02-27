using AllYouMedia.DataAccess.ServiceLayer;
using AllYouMedia.Models;
using BusinessEntity.ConcreateEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AllYouMedia.Mailers;
using PayPal.Api;

namespace AllYouMedia.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ICategoryTypeService categoryTypeService;
        private ISubCategoryService subCategoryService;
        private IItemService itemService;
        private ICartService cartService;
        private IAspNetUserAddressService aspNetUserAddressService;
        private ILogService logService;
        private IOrderService orderService;
        private IAspNetUserService aspNetUserService;
        private IPayoutDistributionService payoutDistributionService;
        public HomeController(CategoryTypeService categoryTypeService, SubCategoryService subCategoryService, ItemService itemService, CartService cartService, AspNetUserAddressService aspNetUserAddressService, LogService logService, OrderService orderService, AspNetUserService aspNetUserService, PayoutDistributionService payoutDistributionService)
        {
            this.categoryTypeService = categoryTypeService;
            this.subCategoryService = subCategoryService;
            this.itemService = itemService;
            this.cartService = cartService;
            this.aspNetUserAddressService = aspNetUserAddressService;
            this.logService = logService;
            this.orderService = orderService;
            this.aspNetUserService = aspNetUserService;
            this.payoutDistributionService = payoutDistributionService;
        }
        private void BindCommonViewBag()
        {
            if (Session["CategoryTypeList"] == null) Session["CategoryTypeList"] = categoryTypeService.GetAllActiveCatDataForMenu(1);
            ViewBag.CategoryTypeList = Session["CategoryTypeList"];
            ViewBag.SessionCart = GetCart();
        }
        public ActionResult Index()
        {
            BindCommonViewBag();
            ViewBag.FeaturedItemList = itemService.GetFeaturedItems();
            ViewBag.PromotedItemList = itemService.GetPromotedItems();
            return View();
        }
        public ActionResult ProductListing(long SubCategoryID)
        {
            BindCommonViewBag();
            if (SubCategoryID == -1) SubCategoryID = subCategoryService.GetFirstSubCategory();
            ViewBag.SubCategoryList = subCategoryService.GetSimilarSubcategoryBySubCategoryID(SubCategoryID);
            ViewBag.ItemList = itemService.GetBySubCategoryID(SubCategoryID);
            return View();
        }
        public ActionResult ProductListingByCatType(long CT)
        {
            long SubCategoryID = subCategoryService.GetFirstSubCategoryIDByCategoryTypeID(CT);
            return RedirectToAction("ProductListing", "Home", new { SubCategoryID = SubCategoryID });
        }
        public ActionResult ProductDetail(long ItemID)
        {
            BindCommonViewBag();
            ViewBag.ItemDetail = itemService.GetById(ItemID);
            ViewBag.SimilarItemList = itemService.GetSimilarItems(ItemID);
            return View();
        }
        private SessionCart GetCart()
        {
            SessionCart sessionCart = null;
            if (Session["SessionCart"] == null)
            {
                sessionCart = new SessionCart();
                if (User.Identity.IsAuthenticated)
                {
                    var cart = cartService.GetCartByAspNetUserID(Convert.ToInt64(User.Identity.GetUserId()));
                    foreach (var cartItem in cart.CartItems)
                    {
                        sessionCart.SetToCart(cartItem.ItemID, cartItem.Qty, cartItem.Item);
                    }
                }
                Session["SessionCart"] = sessionCart;
            }
            sessionCart = (SessionCart)Session["SessionCart"];
            return sessionCart;
        }
        public ActionResult AddToCart(long ItemID, int Qty, string ReturnURL)
        {
            SessionCart sessionCart = null;
            if (Session["SessionCart"] == null) sessionCart = new SessionCart();
            else sessionCart = (SessionCart)Session["SessionCart"];
            if (User.Identity.IsAuthenticated)
            {
                var AspNetUserID = Convert.ToInt64(User.Identity.GetUserId());
                cartService.AddToCart(AspNetUserID, ItemID, Qty);
            }
            var item = itemService.GetById(ItemID);
            sessionCart.SetToCart(ItemID, Qty, item);
            Session["SessionCart"] = sessionCart;
            if (Qty > 0)
                TempData["Success"] = string.Format("Item: {0}, Qty: {1} updated in cart.", item.Name, Qty);
            else
                TempData["Success"] = string.Format("Item: {0} removed from cart.", item.Name);
            return Redirect(ReturnURL);
        }

        public ActionResult Cart()
        {
            BindCommonViewBag();
            return View();
        }

        public ActionResult Checkout()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Register", "Account", new { MembershipType = Convert.ToInt32(AllYouMedia.Code.SharedLibrary.MemberTypeEnum.Customer), ReturnURL = "/Home/Checkout" });
            BindCommonViewBag();
            ViewBag.AddressList = aspNetUserAddressService.AspNetUserAddressCbo(Convert.ToInt64(User.Identity.GetUserId()));
            return View();
        }

        [Authorize]
        public ActionResult AddShippingAddress(string AddressLine1, string AddressLine2, string City, string Province, string Country, string PinCode, string Landmark)
        {
            var aspNetUserAddress = new AllYouMedia.DataAccess.EntityLayer.DBEntity.AspNetUserAddress()
            {
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                Province = Province,
                Country = Country,
                PinCode = PinCode,
                Landmark = Landmark,
                AspNetUserID = Convert.ToInt64(User.Identity.GetUserId())
            };
            aspNetUserAddressService.Insert(aspNetUserAddress);
            return RedirectToAction("Checkout", "Home");
        }

        public ActionResult CreatePaypalPayment(SessionCart sessionCart, long OrderID)
        {
            var config = ConfigManager.Instance.GetProperties();

            // Use OAuthTokenCredential to request an access token from PayPal
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            var itemList = new ItemList();
            itemList.items = new List<Item>();
            foreach (var cartItem in sessionCart.CartItems)
            {
                itemList.items.Add(new Item
                {
                    currency = AllYouMedia.MvcApplication.Currency,
                    name = cartItem.ItemName,
                    price = cartItem.SellPrice.ToString(),
                    quantity = cartItem.Qty.ToString(),
                    tax = cartItem.Tax.ToString(),
                });
            }
            //var deliveryAddress = orderService.GetShippingAddressByOrderID(OrderID);

            //itemList.shipping_address = new ShippingAddress
            //{
            //    city = deliveryAddress.City,
            //    default_address = true,
            //    line1 = deliveryAddress.AddressLine1,
            //    line2 = deliveryAddress.AddressLine2,
            //    postal_code = deliveryAddress.PinCode,
            //    preferred_address = true,
            //    //recipient_name=deliveryAddress.AspNetUser.Name,
            //    //state=deliveryAddress.Province,

            //};
            // ###Payer
            // A resource representing a Payer that funds a payment
            // Payment Method
            // as `paypal`
            var payer = new Payer() { payment_method = "paypal" };

            // ###Redirect URLS
            // These URLs will determine how the user is redirected from PayPal once they have either approved or canceled the payment.
            var baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/ProcessPayment?";
            var redirectUrl = baseURI + "OrderID=" + OrderID;
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&cancel=true",
                return_url = redirectUrl
            };

            // ###Details
            // Let's you specify details of a payment amount.
            var details = new Details()
            {
                tax = sessionCart.Tax.ToString(),
                shipping = sessionCart.Shipping.ToString(),
                subtotal = sessionCart.NetAmount.ToString()
            };

            // ###Amount
            // Let's you specify a payment amount.
            var amount = new Amount()
            {
                currency = AllYouMedia.MvcApplication.Currency,
                total = sessionCart.PayableAmount.ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            // ###Transaction
            // A transaction defines the contract of a
            // payment - what is the payment for and who
            // is fulfilling it. 
            var transactionList = new List<Transaction>();

            // The Payment creation API requires a list of
            // Transaction; add the created `Transaction`
            // to a List
            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = OrderID.ToString(),
                amount = amount,
                item_list = itemList,
            });

            // ###Payment
            // A Payment Resource; create one using
            // the above types and intent as `sale` or `authorize`
            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            ////////////////////////////////////////////////////LOG Payment initiate

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);


            // Using the `links` provided by the `createdPayment` object, we can give the user the option to redirect to PayPal to approve the payment.
            var links = createdPayment.links.GetEnumerator();
            string approval_url = "";
            while (links.MoveNext())
            {
                var link = links.Current;
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    approval_url = link.href;
                }
            }
            return Redirect(approval_url);
        }

        [Authorize]
        public ActionResult PlaceOrder(long ShippingAddressID)
        {
            var sessionCart = GetCart();
            if (sessionCart.CartItems.Count < 1) { TempData["Error"] = "No item in cart. Please add item and try again."; return RedirectToAction("Cart", "Home"); }
            var result = orderService.ValidateAndPlaceOrder(sessionCart, Convert.ToInt64(User.Identity.GetUserId()), ShippingAddressID);
            if (result.Item1 == false) { TempData["Error"] = result.Item2; return RedirectToAction("Checkout", "Home"); }
            else
            {
                //////////////////// Go to payment, on completion update order payment details clearup cart;
                return CreatePaypalPayment(sessionCart, result.Item3);
            }
        }
        [Authorize]
        public ActionResult RetryPayment(long OrderID)
        {
            var sessionCart = GetCart();
            if (sessionCart.CartItems.Count < 1) { TempData["Error"] = "No item in cart. Please add item and try again."; return RedirectToAction("Cart", "Home"); }
            //////////////////// Go to payment, on completion update order payment details clearup cart;
            return CreatePaypalPayment(sessionCart, OrderID);
        }
        [Authorize]
        public ActionResult ProcessPayment(string OrderID, string PayerID, string paymentId, string token, bool cancel = false)
        {
            if (cancel == false)  ////////Process Order
            {
                var aspnetUser = aspNetUserService.GetById(Convert.ToInt64(User.Identity.GetUserId()));
                var order = orderService.GetById(Convert.ToInt64(OrderID));
                order.PayerRefCode = PayerID;
                order.PaymentRefCode = paymentId;
                order.PaymentMode = "Paypal";
                orderService.Update(order);

                var config = ConfigManager.Instance.GetProperties();
                var paymentExecution = new PaymentExecution() { payer_id = PayerID };
                var payment = new Payment() { id = paymentId };
                var accessToken = new OAuthTokenCredential(config).GetAccessToken();
                var apiContext = new APIContext(accessToken);

                var executedPayment = payment.Execute(apiContext, paymentExecution);

                var deliveryAddress = aspNetUserAddressService.GetById(order.AspNetUserAddressID);
                var sessionCart = GetCart();
                AllYouMediaMailer mailer = new AllYouMediaMailer();
                mailer.OrderSuccessfull(User.Identity.Name, aspnetUser.Name, sessionCart, deliveryAddress.CBOExpression, order.ID).SendAsync();
                ///////////// Payout distribution
                try
                {
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        payoutDistributionService.CreatePayoutForOrder(order.ID);
                    });
                }
                catch (Exception ex)
                {
                    logService.Insert(new DataAccess.EntityLayer.DBEntity.Log
                    {
                        ActivityType = "PayoutDistributionERROR",
                        AspNetUserID = 1,///admin
                        Description = string.Format("Error occured while creating payout for order {0}", order.ID),
                    });
                }

                Session["SessionCart"] = null;
                TempData["Success"] = "Your order placed successfully. An email has been sent to you for details.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Seems like you have canceled your order. Please try again.";
                return RedirectToAction("RetryPayment", "Home", new { OrderID = OrderID });
            }
        }
        public class SessionCart
        {
            public SessionCart() { this.CartItems = new List<SessionCartItem>(); Tax = 0; }
            public List<SessionCartItem> CartItems { get; set; }
            public decimal NetAmount
            {
                get
                {
                    return this.CartItems.Sum(x => x.LineAmount);
                }
            }
            public decimal TotalDiscount
            {
                get
                {
                    return this.CartItems.Sum(x => x.LineDiscount);
                }
            }
            public decimal Shipping
            {
                get
                {
                    return 0;
                }
            }
            public decimal PayableAmount
            {
                get
                {
                    return this.NetAmount - this.TotalDiscount + this.Shipping;
                }
            }

            public void SetToCart(long ItemID, int Qty, AllYouMedia.DataAccess.EntityLayer.DBEntity.Item dbItem)
            {
                var cartItem = this.CartItems.FirstOrDefault(x => x.ItemID == ItemID);
                if (cartItem == null)
                {
                    this.CartItems.Add(new SessionCartItem { ItemID = ItemID, Qty = Qty, BasePrice = dbItem.BasePrice, SellPrice = dbItem.SellPrice, ItemName = dbItem.Name, PhotoIMG = dbItem.PhotoIMG });
                }
                else if (Qty == 0)
                    this.CartItems.Remove(cartItem);
                else
                    cartItem.Qty = Qty;
            }
            public void ClearCart()
            {
                this.CartItems.RemoveAll(x => true);
            }

            public decimal Tax { get; set; }
        }
        public class SessionCartItem
        {
            public SessionCartItem()
            {
                Tax = 0;
            }
            public long ItemID { get; set; }
            public string PhotoIMG { get; set; }
            public string ItemName { get; set; }
            public int Qty { get; set; }
            public decimal BasePrice { get; set; }
            public decimal SellPrice { get; set; }
            public decimal LineAmount
            {
                get
                {
                    return this.SellPrice * this.Qty;
                }
            }
            public decimal LineDiscount { get; set; }

            public decimal Tax { get; set; }
        }


        #region About()
        public ActionResult About()
        {
            BindCommonViewBag();
            return View();
        }
        #endregion

        #region Membership()
        public ActionResult Membership()
        {
            BindCommonViewBag();
            return View();
        }
        #endregion

        #region PrivacyPolicy()
        public ActionResult PrivacyPolicy()
        {
            BindCommonViewBag();
            return View();
        }
        #endregion

        #region TermsCondition()
        public ActionResult TermsCondition()
        {
            BindCommonViewBag();
            return View();
        }
        #endregion

        #region TalentAgreement()
        public ActionResult TalentAgreement()
        {
            BindCommonViewBag();
            return View();
        }
        #endregion

        #region MediaAgreement()
        public ActionResult MediaAgreement()
        {
            BindCommonViewBag();
            return View();
        }
        #endregion

        #region AreaPromoterAgreement()
        public ActionResult AreaPromoterAgreement()
        {
            BindCommonViewBag();
            return View();
        }
        #endregion

        #region ContestRule()
        public ActionResult ContestRule()
        {
            BindCommonViewBag();
            return View();
        }
        #endregion

        #region Contact()
        public ActionResult Contact()
        {
            BindCommonViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactUsModel model)
        {
            object message = string.Empty;
            string _emailaddress = System.Configuration.ConfigurationManager.AppSettings["mailaddress_admin"].ToString();
            string _body = "Dear " + _CodeClass.GetCompanyName() + ",<br/><br/>One person has contacted you. His/her contact information is following..<br/><br/>";
            _body = _body + "Name: " + model.Name + "<br/>";
            _body = _body + "Email: " + model.Email + "<br/>";
            _body = _body + "Phone: " + model.Phone + "<br/>";
            _body = _body + "Message: " + model.Message + "<br/>";

            if (_CodeClass.SendMail(_emailaddress, model.Subject, _body))
            {
                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + "Your message has been sent to our customer care team." + "</div>";
                ModelState.Clear();
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + "Fatal error found. Please try again!" + "</div>";
            }
            model.PageHtml = new CommonDataEntity().Page_Select_Detail("contact");
            BindCommonViewBag();
            return View(model);
        }
        #endregion
    }
}