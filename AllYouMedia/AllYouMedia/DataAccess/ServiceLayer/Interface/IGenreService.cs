using AllYouMedia.DataAccess.EntityLayer.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllYouMedia.DataAccess.ServiceLayer.Interface
{
   public interface IGenreService
    {
        List<GenreCategory> GetAll();
        GenreCategory GetById(long id);

        void Insert(GenreCategory model);

        void Update(GenreCategory model);

        void Delete(GenreCategory model);
        IEnumerable<System.Web.Mvc.SelectListItem> GenderCbo();
        List<System.Web.Mvc.SelectListItem> GenreInstrumentCategory(int MembershipType, long GenreID);
    }
}
