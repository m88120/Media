using System.Collections.Generic;
using AllYouMedia.DataAccess.EntityLayer;
using AllYouMedia.Models;
using AllYouMedia.DataAccess.EntityLayer.DBEntity;
using System;

namespace AllYouMedia.DataAccess.ServiceLayer.Interface
{
   public interface IGenderService
    {
        List<GenderSpecific> GetAll();
        GenderSpecific GetById(long id);

        void Insert(GenderSpecific model);

        void Update(GenderSpecific model);

        void Delete(GenderSpecific model);
        IEnumerable<System.Web.Mvc.SelectListItem> GenderCbo();
        List<System.Web.Mvc.SelectListItem> GenderGenreCategory(int MembershipType, long GenderTypeID);

        //  List<System.Web.Mvc.SelectListItem> CategoryCboByCategoryTypeMembershipType(int MembershipType, long CategoryTypeID);


    }
}
