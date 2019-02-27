namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface IMessageService
    {
        List<Message> GetAll();

        Message GetById(long id);

        void Insert(Message model);

        void Update(Message model);

        void Delete(Message model);
        List<Message> GetMessagesByUserID(long UserID);
        Tuple<dynamic, int> GetForDT(long AspNetUserID, string search, int start, int length);
    }
}