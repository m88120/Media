namespace AllYouMedia.DataAccess.ServiceLayer
{
    using System.Collections.Generic;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.EntityLayer.DBEntity;
    using System;

    public interface ILogService
    {
        List<Log> GetAll();

        Log GetById(long id);

        void Insert(Log model);

        void Update(Log model);

        void Delete(Log model);
        List<Log> GetUserLogForHeader(long AspNetUserID);
    }
}