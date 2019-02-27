using AllYouMedia.Models;
using BusinessEntity.ConcreateEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AllYouMedia.Controllers
{
    [AllowAnonymous]
    public class EventController : Controller
    {
        #region Private Properties                
        #endregion

        #region Index
        public ActionResult Index(string reff = "")
        {
            TempData["Message"] = "";
            EventModel model = new EventModel();
            List<EventListModel> EventList = new List<EventListModel>();
            DataTable dt = new DataTable();
            ViewBag.EventCount = Convert.ToString(new EventDataEntity().GetTotalEventCount());
            dt = new EventDataEntity().Event_Select_EventList();
            foreach (DataRow dr in dt.Rows)
            {
                EventList.Add(new EventListModel
                {
                    UID = dr["UID"].ToString(),
                    EventName = dr["EventName"].ToString().ToString(),
                    DateFrom = dr["DateFrom"].ToString(),
                    DateTo = dr["DateTo"].ToString(),
                    ImageURL = dr["ImageURL"].ToString().Replace("~/", "/"),
                    Time = dr["Time"].ToString(),
                    City = dr["City"].ToString(),
                    State = dr["State"].ToString(),
                    Country = dr["Country"].ToString(),
                    Fee = dr["Fee"].ToString(),
                    Remark = dr["Remark"].ToString(),
                });
            }
            if (reff != "")
            {
                model.hdnReff = reff;
                Session["hdnReff"] = reff;
            }
            else
            {
                Session["hdnReff"] = "";
            }
            model.EventList = EventList;
            return View(model);
        }
        #endregion

        #region EventDetail(UID)
        public ActionResult EventDetail(string UID = "")
        {
            if (UID == "")
            {
                return RedirectToAction("Index", "Event");
            }
            else
            {
                EventDetailModel model = new EventDetailModel();
                if (null == Session["hdnReff"])
                {
                    Session["hdnReff"] = "";
                }
                else if(Session["hdnReff"].ToString() != null)
                {
                    model.hdnReff = Session["hdnReff"].ToString(); Session["hdnReff"] = Session["hdnReff"].ToString();
                   
                }
                Session["Event_UID"] = UID;
                DataTable dt_Image = new EventDataEntity().Event_Select_EventMainDetailWithUID(UID);
                if (dt_Image.Rows.Count > 0)
                {
                    model.Remark = dt_Image.Rows[0]["Remark"].ToString();
                    model.EventImage = dt_Image.Rows[0]["Image"].ToString().Replace("~/", "/");
                    model.Fee = dt_Image.Rows[0]["Fee"].ToString();
                    model.UID = dt_Image.Rows[0]["UID"].ToString();
                }
                else
                {
                    return RedirectToAction("Index", "Event");
                }
                DataTable dt_EventDetail = new EventDataEntity().Event_Select_EventDetailWithUID(UID);
                if (dt_EventDetail.Rows.Count > 0)
                {
                    model.Host = dt_EventDetail.Rows[0]["Host"].ToString();
                    model.Venue = dt_EventDetail.Rows[0]["Venue"].ToString();
                    model.State = dt_EventDetail.Rows[0]["State"].ToString();
                    model.Country = dt_EventDetail.Rows[0]["Country"].ToString();
                    model.DateFrom = dt_EventDetail.Rows[0]["DateFrom"].ToString();
                    model.Time = dt_EventDetail.Rows[0]["Time"].ToString();
                    model.MapUrl = dt_EventDetail.Rows[0]["MapUrl"].ToString();
                }
                else
                {
                    return RedirectToAction("Index", "Event");
                }
                List<PerformerDetailModel> PerformerDetail = new List<PerformerDetailModel>();
                DataTable dt_PerformerDetail = new EventDataEntity().Event_Select_EventDayDetailWithUID(UID);
                foreach (DataRow dr in dt_PerformerDetail.Rows)
                {
                    PerformerDetail.Add(new PerformerDetailModel
                    {
                        PerformarName = dr["PerformarName"].ToString().ToString(),
                        PerformarCategory = dr["PerformarCategory"].ToString(),
                        Date = dr["Date"].ToString(),
                        PerformerImage = dr["PerformerImage"].ToString().Replace("~/", "/"),
                        PerformarDescription = dr["PerformarDescription"].ToString(),
                        Time = dr["Time"].ToString(),
                    });
                }
                model.PerformerDetail = PerformerDetail;
                return View(model);
            }
        }
        #endregion

        #region PerformerEdit(model)
        [HttpPost]
        public ActionResult PerformerEdit(EventDetailModel model)
        {
            RoleDataEntity rd = new RoleDataEntity();
            object message = string.Empty;
            if ((new EventDataEntity().Event_Insert_VisitorPerformerDetail(Session["Event_UID"].ToString(), model.Performer.Name, model.Performer.Category, model.Performer.TimeFrom, model.Performer.TimeTo, model.Performer.Email, model.Performer.Phone, model.Performer.Message, out message)) > 0)
            {
                TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-danger'>" + message + "</div>";
            }
            return RedirectToAction("EventDetail", "Event", new { UID = Session["Event_UID"].ToString() });
        }
        #endregion

        #region AddEventCart(UID)
        [HttpPost]
        public ActionResult AddEventCart(string UID = "")
        {
            DataTable dt = _CodeClass.GetEventCart();
            dt.Rows.Add(UID, 1);
            DataTable dt1 = new EventDataEntity().Event_InsertCartItem(UID, Convert.ToInt32(1));
            if (dt1.Rows.Count > 0)
            {
                Session["CUID"] = dt1.Rows[0]["UID"].ToString();
            }
            return Json(new { ok = true, newurl = Url.Action("EventCart", "Event") });
        }
        #endregion

        #region EventCart()       
        public ActionResult EventCart()
        {
            if (!string.IsNullOrEmpty(Session["CUID"] as string))
            {
                if (Session["hdnReff"].ToString() != null)
                { Session["hdnReff"] = Session["hdnReff"].ToString(); }
                else { Session["hdnReff"] = ""; }
                DataTable dt = new EventDataEntity().Event_Select_Cart(_CodeClass.GetEventCart(), Session["CUID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    _CodeClass.SetEventCart(dt);
                    List<EventCartModel> CM = new List<EventCartModel>();
                    int j = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        CM.Add(new EventCartModel
                        {
                            SrNo = ++j,
                            UID = dr["UID"].ToString(),
                            EventName = dr["EventName"].ToString(),
                            Fee = dr["Fee"].ToString(),
                            Amount = dr["Amount"].ToString(),
                            EventURL = dr["EventURL"].ToString().Replace("~/", "/"),
                            Qty = dr["Qty"].ToString(),
                        });
                    }
                    ViewBag.SubTotal = _CodeClass.GetCurrency(Convert.ToDecimal(dt.Compute("SUM([Amount])", "")));
                    ViewBag.Fee = dt.Compute("SUM([Amount])", "");
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + "Event has been added to your Event cart!" + "</div>";
                    Session["CUID"] = Session["CUID"].ToString();
                    return View("EventCart", CM);
                }
                else
                {
                    return RedirectToAction("Index", "Event");
                }
            }
            else
            {
                return RedirectToAction("Index", "Event");
            }
        }
        #endregion

        #region DeleteEventCartItem(UID)              
        [HttpGet]
        public JsonResult DeleteEventCartItem(string UID)
        {
            object message = string.Empty;
            if (UID != "")
            {
                if (new EventDataEntity().Event_Delete_CartItem(_CodeClass.GetEventCart(), UID, out message) > 0)
                {
                    TempData["Message"] = "<div class='alert alert-success'><i class='fa fa-check'></i>" + message + "</div>";
                    message = "1";
                    Session["CUID"] = Session["CUID"].ToString();
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

        #region UpdateEventCartItem(UID, QTY)              
        [HttpGet]
        public JsonResult UpdateEventCartItem(string UID, int QTY)
        {
            object message = string.Empty;
            if (UID != "" & QTY >= 1)
            {
                DataTable dt = new EventDataEntity().Event_UpdateCartItem(_CodeClass.GetEventCart(), UID, Convert.ToInt32(QTY));
                if (dt.Rows.Count > 0)
                {
                    var Qty = dt.Rows[0]["Qty"].ToString();
                    var Amount = dt.Rows[0]["Amount"].ToString();
                    ViewBag.Fee = dt.Compute("SUM([Amount])", "");
                    return Json(new { Qty = Qty, Amount = Amount }, JsonRequestBehavior.AllowGet);
                    //return Json((RenderRazorViewToString("EventCart", CM)), JsonRequestBehavior.AllowGet);
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

        #region EventTicketInvoice
        public ActionResult EventTicketInvoice()
        {
            string EUID = (Request.QueryString["EUID"] != null) ? Request.QueryString["EUID"].ToString().Trim() : "";
            if (EUID != "")
            {
                EventInvoiceModel model = new EventInvoiceModel();
                DataTable dt = new ShoppingDataEntity().Shopping_Select_VisitorEventTicketInvoice(EUID);
                if (dt.Rows.Count > 0)
                {
                    model.Name = dt.Rows[0]["Name"].ToString().ToString();
                    model.ADDRESS = dt.Rows[0]["ADDRESS"].ToString();
                    model.ADDRESS2 = dt.Rows[0]["ADDRESS 2"].ToString();
                    model.PinCode = dt.Rows[0]["PinCode"].ToString();
                    model.EMAIL = dt.Rows[0]["EMAIL"].ToString();
                    model.InvoiceNo = dt.Rows[0]["Invoice No"].ToString();
                    model.EventNAME = dt.Rows[0]["EventNAME"].ToString();
                    model.EventADDRESS = dt.Rows[0]["EventADDRESS"].ToString();
                    model.Date = dt.Rows[0]["Date"].ToString();
                    model.Time = dt.Rows[0]["Time"].ToString();
                    model.Rate = _CodeClass.GetCurrency(dt.Rows[0]["Rate"].ToString());
                    model.Quantity = dt.Rows[0]["Quantity"].ToString();
                    model.Amount = _CodeClass.GetCurrency(dt.Rows[0]["Amount"].ToString());
                    model.AmountWord = dt.Rows[0]["Amount in Word"].ToString();
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Event");
            }
        }
        #endregion

        [NonAction]
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}