using AllYouMedia.DataAccess.EntityLayer.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllYouMedia.DataAccess.ServiceLayer.Interface
{
    public interface IInstrumentService
    {
        List<InstrumentCategory> GetAll();
        InstrumentCategory GetById(long id);

        void Insert(InstrumentCategory model);

        void Update(InstrumentCategory model);

        void Delete(InstrumentCategory model);
        IEnumerable<System.Web.Mvc.SelectListItem> GenderCbo();
        List<System.Web.Mvc.SelectListItem> GenreInstrumentCategory(int MembershipType, long GenreID);
    }
}
