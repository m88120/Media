using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace BusinessEntity.ConcreateEntity
{
    public class MessageDataEntity : DataEntity
    {
        private DataEntity _de;

        #region MessageDataEntity
        public MessageDataEntity()
        {
            _de = new DataEntity();
        }
        #endregion

        #region Message_Insert_User
        public int Message_Insert_User(string SenderID, string Message_Subject, string Message_Body, out object message)
        {
            _de.ParaNameArray("@SenderID", "@Message_Subject", "@Message_Body");
            return _de.ExecuteNonQuery("Message_Insert_User", "@Message", out message, SenderID, Message_Subject, Message_Body);
        }
        public int Message_Reply_User(string SenderID, string ReceiverID, string Message_Subject, string Message_Body, out object message)
        {
            _de.ParaNameArray("@SenderID", "@ReceiverID", "@Message_Subject", "@Message_Body");
            return _de.ExecuteNonQuery("Message_Reply_User", "@Message", out message, SenderID, ReceiverID, Message_Subject, Message_Body);
        }
        #endregion

        #region Message_Insert_Admin
        public int Message_Insert_Admin(string SenderID, string Message_Subject, string Message_Body, string MessageToAll, string Reply_UID, out object message)
        {
            _de.ParaNameArray("@SenderID", "@Message_Subject", "@Message_Body", "@MessageToAll", "@Reply_UID");
            return _de.ExecuteNonQuery("Message_Insert_Admin", "@Message", out message, SenderID, Message_Subject, Message_Body, MessageToAll, Reply_UID);
        }
        #endregion

        #region Message_Select_Inbox
        public DataTable Message_Select_Inbox(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Message_Select_Inbox", Reg_User_LoginName);
        }
        public DataTable Message_Select_InboxForHeader(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Message_Select_InboxForHeader", Reg_User_LoginName);
        }
        #endregion

        #region Message_Select_InboxAdmin
        public DataTable Message_Select_InboxAdmin(long UserID)
        {
            _de.ParaNameArray("@UserID");
            return _de.ExecuteDataTable("Message_Select_InboxAdmin", UserID);
        }
        #endregion

        #region Message_Select_OutBox
        public DataTable Message_Select_OutBox(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Message_Select_OutBox", Reg_User_LoginName);
        }
        #endregion

        #region Message_Select_OutBoxAdmin
        public DataTable Message_Select_OutBoxAdmin(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Message_Select_OutBoxAdmin", Reg_User_LoginName);
        }
        #endregion

        #region Message_Delete_Admin()
        public int Message_Delete_Admin(string Message_UID)
        {
            _de.ParaNameArray("@Message_UID");
            return _de.ExecuteNonQuery("Message_Delete_Admin", Message_UID);
        }
        #endregion

        #region Message_Delete_User()
        public int Message_Delete_User(string Message_UID)
        {
            _de.ParaNameArray("@Message_UID");
            return _de.ExecuteNonQuery("Message_Delete_User", Message_UID);
        }
        #endregion

        #region Message_Insert_TalentUser
        public int Message_Insert_TalentUser(string SenderID, string ReceiverID, string Message_Subject, string Message_Body, out object message)
        {
            _de.ParaNameArray("@SenderID", "@ReceiverID", "@Message_Subject", "@Message_Body");
            return _de.ExecuteNonQuery("Message_Insert_TalentUser", "@Message", out message, SenderID, ReceiverID, Message_Subject, Message_Body);
        }
        #endregion

        #region Message_GetMessageDetail
        public DataTable Message_GetMessageDetail(string Reg_User_LoginName, string Message_UID)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Message_UID");
            return _de.ExecuteDataTable("Message_GetMessageDetail", Reg_User_LoginName, Message_UID);
        }
        public DataTable Message_GetMessageDetailAdmin(string Message_UID, bool isCallFromInbox)
        {
            _de.ParaNameArray("@Reg_Admin_LoginID", "@isCallFromInbox", "@Message_UID");
            return _de.ExecuteDataTable("Com_Message_GetMessageDetailAdmin", HttpContext.Current.User.Identity.Name, isCallFromInbox, Message_UID);
        }
        public DataTable Mail_GetMailDetail(string Mail_UID)
        {
            _de.ParaNameArray("@Reg_User_LoginName", "@Mail_UID");
            return _de.ExecuteDataTable("Com_Mail_GetMailDetail", HttpContext.Current.User.Identity.Name, Mail_UID);
        }
        public DataTable Mail_GetMailDetailAdmin(string Mail_UID)
        {
            _de.ParaNameArray("@Reg_Admin_LoginID", "@Mail_UID");
            return _de.ExecuteDataTable("Com_Mail_GetMailDetailAdmin", HttpContext.Current.User.Identity.Name, Mail_UID);
        }
        #endregion

        #region Message_Select_Chatbox
        public DataTable Message_Select_Chatbox(string Reg_User_LoginName)
        {
            _de.ParaNameArray("@Reg_User_LoginName");
            return _de.ExecuteDataTable("Message_Select_Chatbox", Reg_User_LoginName);
        }
        #endregion
    }
}