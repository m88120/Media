using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessEntity.ConcreateEntity;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Drawing.Imaging;

namespace AllYouMedia
{
    public class _CodeClass
    {

        #region Constructor()
        public _CodeClass()
        {

        }
        #endregion

        #region FillDropDownList()
        public static void FillDropDownList(DataTable dt, DropDownList oDropDownList, string oDataTextField, string oDataValueField)
        {
            ListItem ddlItem;
            oDropDownList.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ddlItem = new ListItem(dr[oDataTextField].ToString(), dr[oDataValueField].ToString());
                oDropDownList.Items.Add(ddlItem);
            }
        }

        public static void FillDropDownList(DataTable dt, DropDownList oDropDownList)
        {
            ListItem ddlItem;
            oDropDownList.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ddlItem = new ListItem(dr["Text"].ToString(), dr["Value"].ToString());
                oDropDownList.Items.Add(ddlItem);
            }
        }

        public static void FillDropDownList(DataTable dt, DropDownList oDropDownList, string oSelect)
        {
            ListItem ddlItem;
            oDropDownList.Items.Clear();

            ddlItem = new ListItem(oSelect, "");
            oDropDownList.Items.Add(ddlItem);

            foreach (DataRow dr in dt.Rows)
            {
                ddlItem = new ListItem(dr["Text"].ToString(), dr["Value"].ToString());
                oDropDownList.Items.Add(ddlItem);
            }
        }

        public static void FillDropDownList(DataTable dt, DropDownList oDropDownList, string oDataTextField, string oDataValueField, string oSelect)
        {
            ListItem ddlItem;
            oDropDownList.Items.Clear();


            ddlItem = new ListItem(oSelect, oSelect);
            oDropDownList.Items.Add(ddlItem);


            foreach (DataRow dr in dt.Rows)
            {
                ddlItem = new ListItem(dr[oDataTextField].ToString(), dr[oDataValueField].ToString());
                oDropDownList.Items.Add(ddlItem);
            }
        }
        #endregion

        #region FillCheckBoxList()
        public static void FillCheckBoxList(DataTable dt, CheckBoxList oCheckBoxList, string oDataTextField, string oDataValueField)
        {
            ListItem ddlItem;
            oCheckBoxList.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ddlItem = new ListItem(dr[oDataTextField].ToString(), dr[oDataValueField].ToString());
                oCheckBoxList.Items.Add(ddlItem);
            }
        }

        public static void FillCheckBoxList(DataTable dt, CheckBoxList oCheckBoxList)
        {
            ListItem ddlItem;
            oCheckBoxList.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ddlItem = new ListItem(dr["Text"].ToString(), dr["Value"].ToString());
                oCheckBoxList.Items.Add(ddlItem);
            }
        }
        #endregion

        #region FillRadioButtonList()
        public static void FillRadioButtonList(DataTable dt, RadioButtonList oRadioButtonList, string oDataTextField, string oDataValueField)
        {
            ListItem ddlItem;
            oRadioButtonList.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ddlItem = new ListItem(dr[oDataTextField].ToString(), dr[oDataValueField].ToString());
                oRadioButtonList.Items.Add(ddlItem);
            }
        }

        public static void FillRadioButtonList(DataTable dt, RadioButtonList oRadioButtonList)
        {
            ListItem ddlItem;
            oRadioButtonList.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ddlItem = new ListItem(dr["Text"].ToString(), dr["Value"].ToString());
                oRadioButtonList.Items.Add(ddlItem);
            }
        }
        #endregion

        #region SendMail()
        public static bool SendMail(string to, string subject, string body)
        {
            if (to != "")
            {
                try
                {
                    if (!HttpContext.Current.Request.IsLocal)
                    {
                        MailMessage email = new MailMessage();
                        email.IsBodyHtml = true;
                        email.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["mailaddress"].ToString(), GetCompanyName());
                        email.To.Add(to);
                        email.Subject = subject;
                        email.Body = body;

                        SmtpClient smtp = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["hostaddress"].ToString());
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["mailaddress"].ToString(), System.Configuration.ConfigurationManager.AppSettings["mailpass"].ToString());
                        smtp.Send(email);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Session["Error"] = "Email Error: " + ex.Message;
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region GetStartAndEndDate
        public static void GetStartAndEndDate(string dateFrom, string dateTo, out string dateFromModified, out string dateToModified)
        {
            if (dateFrom == "")
                dateFromModified = "01/01/1900";
            else
                dateFromModified = dateFrom;

            if (dateTo == "")
                dateToModified = "01/01/2099";
            else
                dateToModified = dateTo;
        }
        #endregion

        #region GetStartDate
        public static string GetStartDate()
        {
            return "01/01/2014";
        }
        #endregion

        #region GetCurrentDate
        public static string GetCurrentDate()
        {
            DateTime date = Convert.ToDateTime(new CommonDataEntity().GetCurrentDate());

            return date.GetDateTimeFormats()[0].ToString();
        }
        #endregion

        #region GetCurrentUserName()
        public static string GetCurrentUserName()
        {
            return (HttpContext.Current.Request.Cookies["__UN"] != null) ? HttpContext.Current.Request.Cookies["__UN"].Value : "";
        }
        #endregion

        #region GetDirectoryPath
        public static string GetDirectoryPath()
        {
            string path = string.Empty;
            int length = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Split(new Char[] { '/' }).Length;

            for (int i = 0; i < length - 2; i++)
            {
                path = path + "../";
            }

            return path;
        }
        #endregion

        #region GetCurrency
        public static string GetCurrency(object value)
        {
            if (value != null && value.ToString() == "") value = 0;
            //return String.Format(new System.Globalization.CultureInfo("en-US"), "{0:C}", value);
            return string.Format("$ {0:#,###,##0.##}", value);
        }
        public static string GetCurrency()
        {
            return "$";
        }
        #endregion

        #region GetCompanyName
        public static string GetCompanyName()
        {
            return "All You Media";
        }
        #endregion

        #region SaveCroppedImage
        public static void SaveCroppedImage(Stream stream, string imgUrl, int width)
        {
            MemoryStream mStream = new MemoryStream();
            System.Drawing.Image oImageBase = System.Drawing.Image.FromStream(stream);

            decimal lnRatio;
            int lnNewWidth = 0;
            int lnNewHeight = 0;
            int height = width;

            if (oImageBase.Width > width || oImageBase.Height > height)
            {
                if (oImageBase.Width > oImageBase.Height)
                {
                    lnRatio = (decimal)width / oImageBase.Width;
                    lnNewWidth = width;
                    decimal lnTemp = oImageBase.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)height / oImageBase.Height;
                    lnNewHeight = height;
                    decimal lnTemp = oImageBase.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }

                System.Drawing.Image oImage = null;
                Graphics oGraphic;
                try
                {
                    oImage = new Bitmap(lnNewWidth, lnNewHeight);
                    oGraphic = Graphics.FromImage(oImage);
                }
                catch { }
                finally
                {
                    oImage = new Bitmap(lnNewWidth, lnNewHeight);
                    Bitmap bmpNew = new Bitmap(oImage.Width, oImage.Height);
                    oGraphic = Graphics.FromImage(bmpNew);
                    oGraphic.DrawImage(oImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), 0, 0, oImage.Width, oImage.Height, GraphicsUnit.Pixel);
                    oImage = bmpNew;
                }

                oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                oGraphic.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                oGraphic.DrawImage(oImageBase, 0, 0, lnNewWidth, lnNewHeight);

                oImage.Save(imgUrl, oImageBase.RawFormat);

                oImageBase.Dispose();
            }
            else
            {
                oImageBase.Save(imgUrl, oImageBase.RawFormat);
            }
        }
        #endregion

        #region SetImageWidthHeight
        public static void SetImageWidthHeight(System.Web.UI.WebControls.Image oImage)
        {
            decimal lnRatio;
            int width = 40;
            int lnNewWidth = 0;
            int lnNewHeight = 0;
            int height = width;

            try
            {
                System.Drawing.Image oImageTemp = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(oImage.ImageUrl));

                if (oImageTemp.Width > width || oImageTemp.Height > height)
                {
                    if (oImageTemp.Width > oImageTemp.Height)
                    {
                        lnRatio = (decimal)width / oImageTemp.Width;
                        lnNewWidth = width;
                        decimal lnTemp = oImageTemp.Height * lnRatio;
                        lnNewHeight = (int)lnTemp;
                    }
                    else
                    {
                        lnRatio = (decimal)height / oImageTemp.Height;
                        lnNewHeight = height;
                        decimal lnTemp = oImageTemp.Width * lnRatio;
                        lnNewWidth = (int)lnTemp;
                    }

                    oImage.Width = lnNewWidth;
                    oImage.Height = lnNewHeight;
                }

                oImageTemp.Dispose();
            }
            catch
            {
                oImage.Width = width;
                oImage.Height = height;
            }
        }
        public static void SetImageWidthHeight(System.Web.UI.WebControls.ImageButton oImage)
        {
            decimal lnRatio;
            int width = 98;
            int lnNewWidth = 0;
            int lnNewHeight = 0;
            int height = width;

            try
            {
                System.Drawing.Image oImageTemp = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(oImage.ImageUrl));

                if (oImageTemp.Width > width || oImageTemp.Height > height)
                {
                    if (oImageTemp.Width > oImageTemp.Height)
                    {
                        lnRatio = (decimal)width / oImageTemp.Width;
                        lnNewWidth = width;
                        decimal lnTemp = oImageTemp.Height * lnRatio;
                        lnNewHeight = (int)lnTemp;
                    }
                    else
                    {
                        lnRatio = (decimal)height / oImageTemp.Height;
                        lnNewHeight = height;
                        decimal lnTemp = oImageTemp.Width * lnRatio;
                        lnNewWidth = (int)lnTemp;
                    }

                    oImage.Width = lnNewWidth;
                    oImage.Height = lnNewHeight;
                }

                oImageTemp.Dispose();
            }
            catch
            {
                oImage.Width = width;
                oImage.Height = height;
            }
        }
        #endregion

        #region GetThumbnailImage
        public static byte[] GetThumbnailImage(Stream stream)
        {
            MemoryStream mStream = new MemoryStream();
            System.Drawing.Image oImageBase = System.Drawing.Image.FromStream(stream);
            int oWidth = 180;

            if (oImageBase.Width > oWidth)
            {
                double oHeight1 = oImageBase.Height;
                double TempWidth = oImageBase.Width;
                double Temp = TempWidth / oHeight1;
                int oHeight = Convert.ToInt32(oWidth / Temp);

                System.Drawing.Image oImage = new Bitmap(oWidth, oHeight, oImageBase.PixelFormat);
                Graphics oGraphic;
                try
                {
                    oGraphic = Graphics.FromImage(oImage);
                }
                finally
                {
                    Bitmap bmpNew = new Bitmap(oImage.Width, oImage.Height);
                    oGraphic = Graphics.FromImage(bmpNew);
                    oGraphic.DrawImage(oImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), 0, 0, oImage.Width, oImage.Height, GraphicsUnit.Pixel);
                    oImage = bmpNew;
                }
                oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle oRectangle = new Rectangle(0, 0, oWidth, oHeight);
                oGraphic.DrawImage(oImageBase, oRectangle);

                oImage.Save(mStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                oImageBase.Save(mStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return mStream.ToArray();
        }
        #endregion

        #region GetMemberTable
        public static DataTable GetMemberTable()
        {
            DataTable dtMember = (DataTable)HttpContext.Current.Session["Member"];

            if (dtMember == null)
            {
                dtMember = new DataTable("Member");
                DataColumn dcUID = new DataColumn("Reg_UserMember_ProductionSub_UID", typeof(string));

                dtMember.Columns.Add(dcUID);

                HttpContext.Current.Session["Member"] = dtMember;
            }
            return dtMember;
        }
        #endregion

        #region GetProductCart()
        public static DataTable GetProductCart()
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["Cart"];

            if (dt == null)
            {
                dt = new DataTable("Cart");
                DataColumn dcUID = new DataColumn("Cart_UID", typeof(string));
                DataColumn dcQty = new DataColumn("Cart_Qty", typeof(string));
                DataColumn dcSize_UID = new DataColumn("Cart_Size_UID", typeof(string));
                DataColumn dcColour_UID = new DataColumn("Cart_Color_UID", typeof(string));

                dt.Columns.Add(dcUID);
                dt.Columns.Add(dcQty);
                dt.Columns.Add(dcSize_UID);
                dt.Columns.Add(dcColour_UID);

                HttpContext.Current.Session["Cart"] = dt;
            }
            return dt;
        }
        #endregion

        #region GetEventCart()
        public static DataTable GetEventCart()
        {
            DataTable dt1 = (DataTable)HttpContext.Current.Session["EventCart"];

            if (dt1 == null)
            {
                dt1 = new DataTable("Cart");
                DataColumn dcUID = new DataColumn("Cart_UID", typeof(string));
                DataColumn dcQty = new DataColumn("Cart_Qty", typeof(string));


                dt1.Columns.Add(dcUID);
                dt1.Columns.Add(dcQty);
                HttpContext.Current.Session["EventCart"] = dt1;
            }
            return dt1;
        }
        #endregion

        #region SetEventCart()
        public static DataTable SetEventCart(DataTable dtFromDB)
        {
            DataTable dt = GetEventCart();
            dt.Rows.Clear();

            foreach (DataRow dr in dtFromDB.Rows)
            {
                dt.Rows.Add(dr["UID"], dr["QTY"]);
            }

            return dt;
        }
        #endregion

        #region SetProductCart()
        public static DataTable SetProductCart(DataTable dtFromDB)
        {
            DataTable dt = GetProductCart();
            dt.Rows.Clear();

            foreach (DataRow dr in dtFromDB.Rows)
            {
                dt.Rows.Add(dr["UID"], dr["QTY"], dr["Size UID"], dr["Color UID"]);
            }

            return dt;
        }
        #endregion

        #region ValidatorMessage
        public static string ValidatorMessage(ValidatorCollection Validators)
        {
            string errorMessage = String.Empty;
            int count = 1;

            foreach (IValidator validator in Validators)
            {
                if (!validator.IsValid)
                {
                    errorMessage = errorMessage + count.ToString() + ") " + validator.ErrorMessage + "<br />";
                    count++;
                }
            }
            return errorMessage;
        }
        #endregion

        public enum Status
        {
            Active = 1
        }
    }
}