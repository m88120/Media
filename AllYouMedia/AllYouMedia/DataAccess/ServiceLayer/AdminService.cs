namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System.Data;
    using AllYouMedia.Models;

    public class AdminService : IAdminService
    {
        private IRepository<AspNetUserRole> aspNetUserRoleRepository;

        public AdminService(IRepository<AspNetUserRole> aspNetUserRoleRepository)
        {
            this.aspNetUserRoleRepository = aspNetUserRoleRepository;
        }

        public DataTable GetDashboard(long AspNetUserID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TotalAllTalent");
            dt.Columns.Add("TotalProduction");
            dt.Columns.Add("TotalMediaPromoter");
            dt.Columns.Add("TotalCustomer");
            dt.Rows.Add(
                aspNetUserRoleRepository.GetCount(x => x.RoleId != 1 && x.RoleId == 3),
                aspNetUserRoleRepository.GetCount(x => x.RoleId != 1 && x.RoleId == 2),
                aspNetUserRoleRepository.GetCount(x => x.RoleId != 1 && x.RoleId == 4),
                aspNetUserRoleRepository.GetCount(x => x.RoleId != 1 && x.RoleId == 6)
                );
            return dt;
        }
    }
}