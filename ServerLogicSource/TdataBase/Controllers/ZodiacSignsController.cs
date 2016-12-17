using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TdataBase.Models;

namespace TdataBase.Controllers
{
    public class ZodiacSignsController : ApiController
    {
        private PartnersContext db = new PartnersContext();
        // api/ZodiacSigns/id
        //Returns statistic for given zodiac sign
        public ZodiacSigns GetSigns(int id)
        {
            if (id < 0 || id > 11 )
                return null;
            List<Partners> partners = (from par in db.Partner
                                       where par.Zodiac == id
                                       select par).ToList();
            ZodiacSigns signs = new ZodiacSigns(partners);
            return signs;
        }
    }
}
