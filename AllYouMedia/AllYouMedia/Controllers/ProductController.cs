using AllYouMedia.Models;
using BusinessEntity.ConcreateEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllYouMedia.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        List<SubMenuModel> SubMenuList = new List<SubMenuModel>();

        #region Index()  
        [AllowAnonymous]
        public ActionResult Index(string CUID = "", string SUID = "", string AUID = "")
        {
            IndexModel model = new IndexModel();
            List<ProductModel> PD = new List<ProductModel>();
            List<ProductModel> PDAlbum = new List<ProductModel>();
            DataSet ds = new DataSet();
            ds = new MasterDataEntity().Master_Select_Top6_Product();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PD.Add(new ProductModel
                {
                    UID = dr["UID"].ToString(),
                    Code = dr["Code"].ToString().ToString(),
                    MediaURL = dr["Media URL"].ToString().Replace("~/", "/"),
                    Name = dr["Media File Name"].ToString(),
                    RetailPrice = dr["Retail Price"].ToString(),
                    Category = dr["Category"].ToString(),
                    SubCategory = dr["SubCategory"].ToString(),
                    CategoryNAME = dr["CategoryName"].ToString(),
                });
            }
            for (var i = 0; i < PD.Count; i++)
            {
                string extFile = System.IO.Path.GetExtension(PD[i].MediaURL).ToLower();
                if (extFile == ".mp4" || extFile == ".avi" || extFile == ".wmv")
                {
                    PD[i].MediaURL = "/Images/document/mp4.jpg";
                }
                else if (extFile == ".pdf")
                {
                    PD[i].MediaURL = "/Images/document/.pdf.png";
                }
                else if (extFile == ".ppt" || extFile == ".pptx")
                {
                    PD[i].MediaURL = "/Images/document/.ppt.png";
                }
                else if (extFile == ".doc" || extFile == ".docx")
                {
                    PD[i].MediaURL = "/Images/document/.doc.PNG";
                }
                else if (extFile == ".xls" || extFile == ".xlsx")
                {
                    PD[i].MediaURL = "/Images/document/.xls.PNG";
                }
                else if (extFile == ".mp3")
                {
                    PD[i].MediaURL = "/Images/document/mp3.jpg";
                }
            }
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                PDAlbum.Add(new ProductModel
                {
                    UID = dr["AlbumUID"].ToString(),
                    Code = dr["AlbumUID"].ToString().ToString(),
                    MediaURL = dr["AlbumCover"].ToString().Replace("~/", "/"),
                    Name = dr["AlbumName"].ToString(),
                    RetailPrice = dr["AlbumPrice"].ToString(),
                    CategoryNAME = "Music Album",
                });
            }
            if (CUID != "")
            {
                PD = PD.Where(x => x.Category == CUID).ToList();
                var result = PD.Concat(PDAlbum);
                model.Product = result.ToList();
            }
            else if (SUID != "")
            {
                model.Product = PD.Where(x => x.SubCategory == SUID).ToList();
            }
            else if (AUID != "")
            {
                model.Product = PDAlbum.Where(x => x.UID == AUID).ToList();
            }
            else
            {
                var result = PD.Concat(PDAlbum);
                model.Product = result.ToList();
            }
            ProductDetailModel ProductDetail = new ProductDetailModel();
            model.ProductDetail = ProductDetail;
            List<MenuModel> Menu = new List<MenuModel>();
            DataTable dt_Menu = new DataTable();
            dt_Menu = new MasterDataEntity().Master_Select_CategoryDefaultCondition();
            foreach (DataRow dr in dt_Menu.Rows)
            {
                Menu.Add(new MenuModel
                {
                    UID = dr["UID"].ToString(),
                    Text = dr["Text"].ToString(),
                    Name = dr["Name"].ToString(),
                });
                ViewData["UID"] = dr["UID"].ToString();
                this.GetSubMenu();
            }          
            model.Menu = Menu;
            model.SubMenu = SubMenuList;
            return View(model);
        }
        #endregion

        #region ProductSelectByCode(Code)
        [AllowAnonymous]
        [HttpGet]
        public PartialViewResult ProductSelectByCode(string Code)
        {
            IndexModel PM = new IndexModel();
            List<ProductModel> PD = new List<ProductModel>();
            ProductDetailModel model = new ProductDetailModel();
            DataTable dt = new DataTable();
            if (Code != "")
            {
                dt = new MasterDataEntity().Master_Select_ItemWithCode(Code);
                if (dt.Rows.Count > 0)
                {
                    string url = dt.Rows[0]["Media URL"].ToString();

                    string extFile = System.IO.Path.GetExtension(url).ToLower();
                    if (extFile == ".jpg" || extFile == ".png" || extFile == ".gif" || extFile == ".jpeg")
                    {
                        model.MediaURL = dt.Rows[0]["Media URL"].ToString().Replace("~/", "/");
                    }
                    else if (extFile == ".mp4" || extFile == ".avi" || extFile == ".wmv")
                    {
                        model.MediaURL = "/Images/document/mp4.jpg";
                    }
                    else if (extFile == ".pdf")
                    {
                        model.MediaURL = "/Images/document/.pdf.png";
                    }
                    else if (extFile == ".ppt" || extFile == ".pptx")
                    {
                        model.MediaURL = "/Images/document/.ppt.png";
                    }
                    else if (extFile == ".doc" || extFile == ".docx")
                    {
                        model.MediaURL = "/Images/document/.doc.PNG";
                    }
                    else if (extFile == ".xls" || extFile == ".xlsx")
                    {
                        model.MediaURL = "/Images/document/.xls.PNG";
                    }
                    else if (extFile == ".mp3")
                    {
                        model.MediaURL = "/Images/document/mp3.jpg";
                    }

                    model.UID = dt.Rows[0]["UID"].ToString();
                    model.Name = dt.Rows[0]["Name"].ToString();
                    model.Code = dt.Rows[0]["CODE"].ToString();
                    model.RetailPrice = _CodeClass.GetCurrency(dt.Rows[0]["Retail Price"]);
                    model.DESCRIPTION = dt.Rows[0]["DESCRIPTION"].ToString();
                    model.CategoryNAME = dt.Rows[0]["Category NAME"].ToString();
                    model.Quantity = "1";
                }
                PM.ProductDetail = model;
                return PartialView("_ProductDetail", PM.ProductDetail);
            }
            else
            {
                List<ProductModel> PDAlbum = new List<ProductModel>();
                DataSet ds = new DataSet();
                ds = new MasterDataEntity().Master_Select_Top6_Product();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PD.Add(new ProductModel
                    {
                        UID = dr["UID"].ToString(),
                        Code = dr["Code"].ToString().ToString(),
                        MediaURL = dr["Media URL"].ToString().Replace("~/", "/"),
                        Name = dr["Media File Name"].ToString(),
                        RetailPrice = dr["Retail Price"].ToString(),
                        Category = dr["Category"].ToString(),
                        SubCategory = dr["SubCategory"].ToString(),
                        CategoryNAME = dr["CategoryName"].ToString(),
                    });
                }
                for (var i = 0; i < PD.Count; i++)
                {
                    string extFile = System.IO.Path.GetExtension(PD[i].MediaURL).ToLower();
                    if (extFile == ".mp4" || extFile == ".avi" || extFile == ".wmv")
                    {
                        PD[i].MediaURL = "/Images/document/mp4.jpg";
                    }
                    else if (extFile == ".pdf")
                    {
                        PD[i].MediaURL = "/Images/document/.pdf.png";
                    }
                    else if (extFile == ".ppt" || extFile == ".pptx")
                    {
                        PD[i].MediaURL = "/Images/document/.ppt.png";
                    }
                    else if (extFile == ".doc" || extFile == ".docx")
                    {
                        PD[i].MediaURL = "/Images/document/.doc.PNG";
                    }
                    else if (extFile == ".xls" || extFile == ".xlsx")
                    {
                        PD[i].MediaURL = "/Images/document/.xls.PNG";
                    }
                    else if (extFile == ".mp3")
                    {
                        PD[i].MediaURL = "/Images/document/mp3.jpg";
                    }
                }
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    PDAlbum.Add(new ProductModel
                    {
                        UID = dr["AlbumUID"].ToString(),
                        Code = dr["AlbumUID"].ToString().ToString(),
                        MediaURL = dr["AlbumCover"].ToString().Replace("~/", "/"),
                        Name = dr["AlbumName"].ToString(),
                        RetailPrice = dr["AlbumPrice"].ToString(),
                        CategoryNAME = "Music Album",
                    });
                }
                var result = PD.Concat(PDAlbum);
                PM.Product = result.ToList();
                return PartialView("_Product", PD);
            }
        }
        #endregion

        #region ProductSelectBySubCategory(Keyword)
        [HttpGet]
        public PartialViewResult ProductSelectBySubCategory(string Category = "", string SubCategory = "")
        {
            IndexModel PM = new IndexModel();
            List<ProductModel> PD = new List<ProductModel>();
            List<ProductModel> PDAlbum = new List<ProductModel>();
            DataSet ds = new DataSet();
            ds = new MasterDataEntity().Master_Select_Top6_Product();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PD.Add(new ProductModel
                {
                    UID = dr["UID"].ToString(),
                    Code = dr["Code"].ToString().ToString(),
                    MediaURL = dr["Media URL"].ToString().Replace("~/", "/"),
                    Name = dr["Media File Name"].ToString(),
                    RetailPrice = dr["Retail Price"].ToString(),
                    Category = dr["Category"].ToString(),
                    SubCategory = dr["SubCategory"].ToString(),
                    CategoryNAME = dr["CategoryName"].ToString(),
                });
            }
            for (var i = 0; i < PD.Count; i++)
            {
                string extFile = System.IO.Path.GetExtension(PD[i].MediaURL).ToLower();
                if (extFile == ".mp4" || extFile == ".avi" || extFile == ".wmv")
                {
                    PD[i].MediaURL = "/Images/document/mp4.jpg";
                }
                else if (extFile == ".pdf")
                {
                    PD[i].MediaURL = "/Images/document/.pdf.png";
                }
                else if (extFile == ".ppt" || extFile == ".pptx")
                {
                    PD[i].MediaURL = "/Images/document/.ppt.png";
                }
                else if (extFile == ".doc" || extFile == ".docx")
                {
                    PD[i].MediaURL = "/Images/document/.doc.PNG";
                }
                else if (extFile == ".xls" || extFile == ".xlsx")
                {
                    PD[i].MediaURL = "/Images/document/.xls.PNG";
                }
                else if (extFile == ".mp3")
                {
                    PD[i].MediaURL = "/Images/document/mp3.jpg";
                }
            }
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                PDAlbum.Add(new ProductModel
                {
                    UID = dr["AlbumUID"].ToString(),
                    Code = dr["AlbumUID"].ToString().ToString(),
                    MediaURL = dr["AlbumCover"].ToString().Replace("~/", "/"),
                    Name = dr["AlbumName"].ToString(),
                    RetailPrice = dr["AlbumPrice"].ToString(),
                    CategoryNAME = "Music Album",
                });
            }                    
            if (Category != "")
            {
                PM.Product = PD.Where(x => x.Category == Category.ToString()).ToList();
            }
            else if (SubCategory != "")
            {
                PM.Product = PD.Where(x => x.SubCategory == SubCategory.ToString()).ToList();
            }
            else
            {
                var result = PD.Concat(PDAlbum);
                PM.Product = result.ToList();
            }
            return PartialView("_Product", PM.Product);
        }
        #endregion

        #region GetSubMenu()
        public List<SubMenuModel> GetSubMenu()
        {
            if (ViewData["UID"] != null)
            {
                DataTable dt = new DataTable();
                dt = new MasterDataEntity().Master_Select_MainCategoryListWithMenu(ViewData["UID"].ToString());
                foreach (DataRow dr in dt.Rows)
                {
                    SubMenuList.Add(new SubMenuModel
                    {
                        UID = dr["UID"].ToString(),
                        Name = dr["Name"].ToString(),
                        MenuUID = dr["MenuUID"].ToString(),
                        AlbumUID = dr["AlbumUID"].ToString(),
                        AlbumName = dr["AlbumName"].ToString(),
                    });
                }
                ViewData["UID"] = null;
                SubMenuList = SubMenuList.OrderBy(x => x.Name).ToList();
            }
            return SubMenuList;
        }
        #endregion

        #region SubMenu(UID)
        [HttpGet]
        public PartialViewResult SubMenu(string UID)
        {
            List<MenuModel> SMM = new List<MenuModel>();
            if (UID == "" || UID == null)
            {
                DataTable dt = new DataTable();
                dt = new MasterDataEntity().Master_Select_MainCategoryListWithMenu(UID);
                foreach (DataRow dr in dt.Rows)
                {
                    SMM.Add(new MenuModel
                    {
                        UID = dr["UID"].ToString(),
                        Name = dr["Name"].ToString(),
                    });
                }
                SMM = SMM.OrderBy(x => x.Name).ToList();
            }
            return PartialView("_SubMenu", SMM);
        }
        #endregion

        #region ProductSearch(PriceSort)    
        [HttpPost]
        public PartialViewResult ProductSearch(int PriceSort, string Name)
        {
            IndexModel model = new IndexModel();
            List<ProductModel> PD = new List<ProductModel>();
            List<ProductModel> PDAlbum = new List<ProductModel>();
            DataSet ds = new DataSet();
            ds = new MasterDataEntity().Master_Select_Top6_Product();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PD.Add(new ProductModel
                {
                    UID = dr["UID"].ToString(),
                    Code = dr["Code"].ToString().ToString(),
                    MediaURL = dr["Media URL"].ToString().Replace("~/", "/"),
                    Name = dr["Media File Name"].ToString(),
                    RetailPrice = dr["Retail Price"].ToString(),
                    Category = dr["Category"].ToString(),
                    SubCategory = dr["SubCategory"].ToString(),
                    CategoryNAME = dr["CategoryName"].ToString(),
                });
            }
            for (var i = 0; i < PD.Count; i++)
            {
                string extFile = System.IO.Path.GetExtension(PD[i].MediaURL).ToLower();
                if (extFile == ".mp4" || extFile == ".avi" || extFile == ".wmv")
                {
                    PD[i].MediaURL = "/Images/document/mp4.jpg";
                }
                else if (extFile == ".pdf")
                {
                    PD[i].MediaURL = "/Images/document/.pdf.png";
                }
                else if (extFile == ".ppt" || extFile == ".pptx")
                {
                    PD[i].MediaURL = "/Images/document/.ppt.png";
                }
                else if (extFile == ".doc" || extFile == ".docx")
                {
                    PD[i].MediaURL = "/Images/document/.doc.PNG";
                }
                else if (extFile == ".xls" || extFile == ".xlsx")
                {
                    PD[i].MediaURL = "/Images/document/.xls.PNG";
                }
                else if (extFile == ".mp3")
                {
                    PD[i].MediaURL = "/Images/document/mp3.jpg";
                }
            }
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                PDAlbum.Add(new ProductModel
                {
                    UID = dr["AlbumUID"].ToString(),
                    Code = dr["AlbumUID"].ToString().ToString(),
                    MediaURL = dr["AlbumCover"].ToString().Replace("~/", "/"),
                    Name = dr["AlbumName"].ToString(),
                    RetailPrice = dr["AlbumPrice"].ToString(),
                    CategoryNAME = "Music Album",
                });
            }
            var result = PD.Concat(PDAlbum);
            if (PriceSort == 0)
            {
                result = result.OrderBy(x => x.Name).ToList();
            }
            else if (PriceSort == 1)
            {
                result = result.OrderBy(x => x.RetailPrice).ToList();
            }
            else if (PriceSort == 2)
            {
                result = result.OrderByDescending(x => x.RetailPrice).ToList();
            }
            else if (PriceSort == 3)
            {
                result = (from x in result orderby x.Category, x.Name ascending select x).ToList();
            }
            else if (Name != "")
            {
                result = result.Where(x => x.Name.ToUpperInvariant().Contains(Name.ToUpperInvariant())).ToList();
            }          
           
            model.Product = result.ToList();

            return PartialView("_Product", PD);
        }
        #endregion

        #region ProductDetail()       
        public ActionResult ProductDetail(string UID = "")
        {
            IndexModel PM = new IndexModel();
            List<ProductModel> PD = new List<ProductModel>();
            List<ProductModel> PDAlbum = new List<ProductModel>();
            List<AlbumModel> PDAlbumList = new List<AlbumModel>();
            List<AlbumModel> PDSongList = new List<AlbumModel>();
            ProductDetailModel model = new ProductDetailModel();
            DataTable dt = new DataTable();
            if (UID != "")
            {
                dt = new MasterDataEntity().Master_Select_ItemWithCode(UID);
                if (dt.Rows.Count > 0)
                {
                    string url = dt.Rows[0]["Media URL"].ToString();

                    string extFile = System.IO.Path.GetExtension(url).ToLower();
                    if (extFile == ".jpg" || extFile == ".png" || extFile == ".gif" || extFile == ".jpeg")
                    {
                        model.MediaURL = dt.Rows[0]["Media URL"].ToString().Replace("~/", "/");
                    }
                    else if (extFile == ".mp4" || extFile == ".avi" || extFile == ".wmv")
                    {
                        model.MediaURL = "/Images/document/mp4.jpg";
                    }
                    else if (extFile == ".pdf")
                    {
                        model.MediaURL = "/Images/document/.pdf.png";
                    }
                    else if (extFile == ".ppt" || extFile == ".pptx")
                    {
                        model.MediaURL = "/Images/document/.ppt.png";
                    }
                    else if (extFile == ".doc" || extFile == ".docx")
                    {
                        model.MediaURL = "/Images/document/.doc.PNG";
                    }
                    else if (extFile == ".xls" || extFile == ".xlsx")
                    {
                        model.MediaURL = "/Images/document/.xls.PNG";
                    }
                    else if (extFile == ".mp3")
                    {
                        model.MediaURL = "/Images/document/mp3.jpg";
                    }
                    model.UID = dt.Rows[0]["UID"].ToString();
                    model.Name = dt.Rows[0]["Name"].ToString();
                    model.Code = dt.Rows[0]["CODE"].ToString();
                    model.RetailPrice = _CodeClass.GetCurrency(dt.Rows[0]["Retail Price"]);
                    model.DESCRIPTION = dt.Rows[0]["DESCRIPTION"].ToString();
                    model.CategoryNAME = dt.Rows[0]["Category NAME"].ToString();
                    model.Quantity = "1";
                    model.Type = dt.Rows[0]["Type"].ToString();
                }
                PM.ProductDetail = model;

                DataSet dt_Related = new MasterDataEntity().Master_Select_Top6_Product();
                foreach (DataRow dr in dt_Related.Tables[0].Rows)
                {
                    PD.Add(new ProductModel
                    {
                        UID = dr["UID"].ToString(),
                        Code = dr["Code"].ToString().ToString(),
                        MediaURL = dr["Media URL"].ToString().Replace("~/", "/"),
                        Name = dr["Media File Name"].ToString(),
                        RetailPrice = dr["Retail Price"].ToString(),
                        Category = dr["Category"].ToString(),
                        SubCategory = dr["SubCategory"].ToString(),
                    });
                }
                for (var i = 0; i < PD.Count; i++)
                {
                    string extFile = System.IO.Path.GetExtension(PD[i].MediaURL).ToLower();
                    if (extFile == ".mp4" || extFile == ".avi" || extFile == ".wmv")
                    {
                        PD[i].MediaURL = "/Images/document/mp4.jpg";
                    }
                    else if (extFile == ".pdf")
                    {
                        PD[i].MediaURL = "/Images/document/.pdf.png";
                    }
                    else if (extFile == ".ppt" || extFile == ".pptx")
                    {
                        PD[i].MediaURL = "/Images/document/.ppt.png";
                    }
                    else if (extFile == ".doc" || extFile == ".docx")
                    {
                        PD[i].MediaURL = "/Images/document/.doc.PNG";
                    }
                    else if (extFile == ".xls" || extFile == ".xlsx")
                    {
                        PD[i].MediaURL = "/Images/document/.xls.PNG";
                    }
                    else if (extFile == ".mp3")
                    {
                        PD[i].MediaURL = "/Images/document/mp3.jpg";
                    }                  
                }
                foreach (DataRow dr in dt_Related.Tables[1].Rows)
                {
                    PDAlbum.Add(new ProductModel
                    {
                        UID = dr["AlbumUID"].ToString(),
                        Code = dr["AlbumUID"].ToString().ToString(),
                        MediaURL = dr["AlbumCover"].ToString().Replace("~/", "/"),
                        Name = dr["AlbumName"].ToString(),
                        RetailPrice = dr["AlbumPrice"].ToString(),
                        CategoryNAME = "Music Album",
                    });
                }
                DataTable dt_AlbumList = new MasterDataEntity().Master_Select_AlbumListWithUID(UID);
                int z = 1;
                foreach (DataRow dr in dt_AlbumList.Rows)
                {
                    PDAlbumList.Add(new AlbumModel
                    {
                        Sno = z++,
                        UID = dr["UID"].ToString(),
                        Name = dr["Name"].ToString().ToString(),
                        FileUrl = dr["FileUrl"].ToString().Replace("~/", "/"),
                        Length = Math.Round(Convert.ToDecimal(dr["Length"]),2),
                        Price = dr["Price"].ToString(),                      
                    });
                }
                DataTable dt_SongList = new MasterDataEntity().Master_Select_SongListWithUID(UID);
                int y = 1;
                foreach (DataRow dr in dt_SongList.Rows)
                {
                    PDSongList.Add(new AlbumModel
                    {
                        Sno = y++,
                        UID = dr["UID"].ToString(),
                        Name = dr["Name"].ToString().ToString(),
                        FileUrl = dr["FileUrl"].ToString().Replace("~/", "/"),
                        Length = Math.Round(Convert.ToDecimal(dr["Length"]), 2),
                        Price = dr["Price"].ToString(),
                    });
                }
                var result = PD.Concat(PDAlbum);
                PM.Product = result.Where(x => x.UID != UID).ToList();
                PM.AlbumList = PDAlbumList;
                PM.SongList = PDSongList;
                return View(PM);
            }
            return RedirectToAction("Index", "Product");
        }
        #endregion

        #region AddCart(UID)
        [HttpPost]
        public ActionResult AddCart(string UID)
        {
            DataTable dt = _CodeClass.GetProductCart();

            dt.Rows.Add(UID, 1);
            return Json(new { ok = true, newurl = Url.Action("Cart") });
        }
        #endregion

        #region Cart()       
        public ActionResult Cart()
        {
            DataTable dt = new ShoppingDataEntity().Shopping_Select_Cart(_CodeClass.GetProductCart(), User.Identity.Name);
            if (dt.Rows.Count > 0)
            {
              
                List<CartModel> CM = new List<CartModel>();
                int j = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    CM.Add(new CartModel
                    {
                        SrNo = ++j,
                        CartUID = dr["Cart_UID"].ToString(),
                        itemUID = dr["UID"].ToString(),
                        Code = dr["Code"].ToString(),
                        Name = dr["Name"].ToString(),
                        IMAGE = dr["Product URL"].ToString().Replace("~/", "/"),
                        ItemType1 = dr["VA"].ToString(),
                        ItemType2 = dr["DI"].ToString(),
                        Shipping = Convert.ToDecimal(dr["SHPNGCHARGE"].ToString()),
                        Rate = Convert.ToDecimal(dr["Rate"].ToString()),
                        QTY = dr["QTY"].ToString(),
                        Amount = Convert.ToDecimal(dr["Amount"].ToString()),
                        Tax = Convert.ToDecimal(dr["Tax"].ToString()),
                        NetAmount = Convert.ToDecimal(dr["Net Amount"].ToString()),
                    });
                }
                for (var i = 0; i < CM.Count; i++)
                {
                    string extFile = System.IO.Path.GetExtension(CM[i].IMAGE).ToLower();
                    if (extFile == ".mp4" || extFile == ".avi" || extFile == ".wmv")
                    {
                        CM[i].IMAGE = "/Images/document/mp4.jpg";
                    }
                    else if (extFile == ".pdf")
                    {
                        CM[i].IMAGE = "/Images/document/.pdf.png";
                    }
                    else if (extFile == ".ppt" || extFile == ".pptx")
                    {
                        CM[i].IMAGE = "/Images/document/.ppt.png";
                    }
                    else if (extFile == ".doc" || extFile == ".docx")
                    {
                        CM[i].IMAGE = "/Images/document/.doc.PNG";
                    }
                    else if (extFile == ".xls" || extFile == ".xlsx")
                    {
                        CM[i].IMAGE = "/Images/document/.xls.PNG";
                    }
                    else if (extFile == ".mp3")
                    {
                        CM[i].IMAGE = "/Images/document/mp3.jpg";
                    }
                }
                ViewBag.SubTotal = _CodeClass.GetCurrency(Convert.ToDecimal(dt.Compute("SUM([Net Amount])", "")));
                ViewBag.Shipping = _CodeClass.GetCurrency(Convert.ToDecimal(dt.Compute("SUM([Expedited Shipping])", "")));
                ViewBag.GrossAmount = _CodeClass.GetCurrency(Convert.ToDecimal(dt.Rows[0]["Expedited Shipping"]) + Convert.ToDecimal(dt.Compute("SUM([Net Amount])", "")));
                ViewBag.TotalAmount = Convert.ToDecimal(dt.Rows[0]["Expedited Shipping"]) + Convert.ToDecimal(dt.Compute("SUM([Net Amount])", ""));
                return View("Cart", CM);
            }
            return RedirectToAction("Index", "Product");
        }
        #endregion

        #region DeleteCartItem(UID)              
        [HttpGet]
        public JsonResult DeleteCartItem(string UID)
        {
            object message = string.Empty;
            if (UID != "")
            {
                if (new ShoppingDataEntity().Shopping_Delete_CartItem(_CodeClass.GetProductCart(), User.Identity.Name, UID, out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    message = "1";
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    message = "0";
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UpdateCartItem(UID, QTY)              
        [HttpGet]
        public JsonResult UpdateCartItem(string UID, int QTY)
        {
            object message = string.Empty;
            if (UID != "" & QTY >= 1)
            {
                if (new ShoppingDataEntity().Shopping_Update_CartItem(_CodeClass.GetProductCart(), User.Identity.Name, UID, QTY, out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    message = "1";
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                    message = "0";
                }
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
                message = "0";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ShoppingInvoice
        public ActionResult ShoppingInvoice()
        {
            string SUID = (Request.QueryString["SUID"] != null) ? Request.QueryString["SUID"].ToString().Trim() : "";
            if (SUID != "")
            {
                ShopInvoiceModel model = new ShopInvoiceModel();
                DataTable dt = new ShoppingDataEntity().Shopping_Select_ShopInvoice(SUID);
                if (dt.Rows.Count > 0)
                {
                    model.Name = dt.Rows[0]["Name"].ToString().ToString();
                    model.ADDRESS = dt.Rows[0]["ADDRESS"].ToString();
                    model.ADDRESS2 = dt.Rows[0]["ADDRESS 2"].ToString();
                    model.PinCode = dt.Rows[0]["PinCode"].ToString();
                    model.EMAIL = dt.Rows[0]["EMAIL"].ToString();
                    model.Date = dt.Rows[0]["Date"].ToString();
                    model.Rate = _CodeClass.GetCurrency(dt.Rows[0]["Rate"].ToString());
                    model.Quantity = dt.Rows[0]["Quantity"].ToString();
                    model.Amount = _CodeClass.GetCurrency(dt.Rows[0]["Amount"].ToString());
                    model.AmountWord = dt.Rows[0]["Amount in Word"].ToString();
                    model.ProductName = dt.Rows[0]["ProductName"].ToString();
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }
        #endregion
    }
}