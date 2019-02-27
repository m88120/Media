namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    public interface IAdminService
    {
        DataTable GetDashboard(long AspNetUserID);
    }
}