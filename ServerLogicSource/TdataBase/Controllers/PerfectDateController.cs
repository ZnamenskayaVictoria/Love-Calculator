using System;
using System.Linq;
using System.Web.Http;
using TdataBase.Models;

namespace TdataBase.Controllers
{
    public class PerfectDateController : ApiController
    {
        private PartnersContext db = new PartnersContext();

        // api/PerfectDate/month/day
        //Returns perfect date for given one
        public PerfectDate GetDate(int month, int day)
        {
            try
            {
                DateTime target = new DateTime(1996, month, day);
                return new PerfectDate(db.Partner.ToList(), target);
            }
            catch
            {
                return null;
            }
        }
    }
}
