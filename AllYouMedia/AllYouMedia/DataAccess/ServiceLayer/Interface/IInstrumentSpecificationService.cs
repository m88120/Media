using AllYouMedia.DataAccess.EntityLayer.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllYouMedia.DataAccess.ServiceLayer.Interface
{
  public  interface IInstrumentSpecificationService
    {
        List<InstrumentSpecificationCategory> GetAll();
        InstrumentSpecificationCategory GetById(long id);

        void Insert(InstrumentSpecificationCategory model);

        void Update(InstrumentSpecificationCategory model);

        void Delete(InstrumentSpecificationCategory model);
        IEnumerable<System.Web.Mvc.SelectListItem> GenderCbo();
        List<System.Web.Mvc.SelectListItem> InstrumentSpecificationCategory(int MembershipType, long GenreID);
    }
}
