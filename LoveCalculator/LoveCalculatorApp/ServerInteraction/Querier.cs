using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LoveCalculatorApp.ServerInteraction
{
    public class Querier
    {
        // URL to perform queries to server
        private string mainUrl = "http://lovecalculator.somee.com/api/PerfectDate";

        public DateTime GetPerfectMatch(DateTime person)
        {
            WebClient client = new WebClient();
            // perform query by adding month number and day number at the end of main url
            string json = client.DownloadString($"{mainUrl}/{person.Month}/{person.Day}");

            // regular expression to extract date from string without Json framework
            Regex extractDate = new Regex("(?<=(\"Date\":\")).+?(?=(\"))");
            string strDate = extractDate.Match(json).Value;
            DateTime result;

            if(!DateTime.TryParse(strDate, out result))
                throw new Exception("Query failed");

            return result;
        }
    }
}
