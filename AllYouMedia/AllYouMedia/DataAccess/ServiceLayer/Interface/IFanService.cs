namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IFanService
    {
        List<Fan> GetAll();

        Fan GetById(long id);

        void Insert(Fan model);

        void Update(Fan model);

        void Delete(Fan model);
        int GetFanCount(long AspNetUserID);

        Tuple<bool,string> BecomeFan(long ID, string Name, string Email, int Rating);

        double GetFanRating(long ID);

        Tuple<bool, string> GenerateFanShareRequestToUser(long RequestedFromAspNetUserID, long RequestedToAspNetUserID);

        int GetFanSharingRequestCount(long AspNetUserID);

        void UpdateFanSharingUserRequest(long ID, bool IsAccepted);

        Tuple<dynamic, int> GetFanSharingUserRequestForDT(long AspNetUserID, string search, int start, int length);
    }
}