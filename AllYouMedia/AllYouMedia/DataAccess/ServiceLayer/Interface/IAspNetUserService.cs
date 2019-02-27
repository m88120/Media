namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IAspNetUserService
    {
        List<AspNetUser> GetAll();

        AspNetUser GetById(long id);

        void Insert(AspNetUser model);

        void Update(AspNetUser model);

        void Delete(AspNetUser model);
        Tuple<string, AspNetUser> UserLoginChecknUpdate(string UserName, string CurrentIP);
        long GetAspNetUserIDByUserName(string UserName);
        long GetAspNetUserIDByRefferCode(string RefferCode);
        void AddToAspNetUserHierarchy(long AspNetUserID, long ParentAspNetUserID);

        UserTree GetUserTree(long AspNetUserID);

        Tuple<List<AspNetUser>, int> GetTalentSearchForDT(long AspNetUserID, long CategoryTypeID, long CategoryID, long SubCategoryID, long AttributeID, string search, int start, int length);

        long GetAspNetUserRoleIDByUserAndRole(long AspNetUserID, string Role);
        Dictionary<long, string> GetRolesDictionary();

        Dictionary<string, string> GetUserDetails(long AspNetUserID);
    }
}