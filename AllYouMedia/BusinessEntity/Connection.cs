using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace BusinessEntity.ConcreateEntity
{
    public class Connection : _Connection
    {
        #region Constructor
        public Connection()
        {
            this.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }

        public Connection(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        #endregion
    }
}
