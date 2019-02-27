using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntity.ConcreateEntity
{
    public abstract class _Connection
    {
        #region Private Variable
        private string _ConString;
        #endregion

        #region ConnectionString
        public string ConnectionString
        {
            get
            {
                return _ConString;
            }
            set
            {
                _ConString = value;
            }
        }
        #endregion
    }
}
