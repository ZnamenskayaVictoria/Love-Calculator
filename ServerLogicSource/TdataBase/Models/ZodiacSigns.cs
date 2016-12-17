using System.Collections.Generic;
namespace TdataBase.Models
{
    public class ZodiacSigns
    {
        //Counting statistic of different signs for given one
        public ZodiacSigns(List<Partners> partners)
        {
            Total = partners.Count;
            partners.ForEach(x =>
            {
                switch (x.SecZodiac)
                {
                    case 0:
                        Aries++;
                        break;
                    case 1:
                        Taurus++;
                        break;
                    case 2:
                        Gemini++;
                        break;
                    case 3:
                        Cancer++;
                        break;
                    case 4:
                        Leo++;
                        break;
                    case 5:
                        Virgo++;
                        break;
                    case 6:
                        Libra++;
                        break;
                    case 7:
                        Scorpio++;
                        break;
                    case 8:
                        Sagittarius++;
                        break;
                    case 9:
                        Capricorn++;
                        break;
                    case 10:
                        Aquarius++;
                        break;
                    case 11:
                        Pisces++;
                        break;
                }
            });
        }
        public int Total { get; set; }
        public int Aries { get; set; }
        public int Taurus { get; set; }
        public int Gemini { get; set; }
        public int Cancer { get; set; }


        public int Leo { get; set; }
        public int Virgo { get; set; }
        public int Libra { get; set; }
        public int Scorpio { get; set; }


        public int Sagittarius { get; set; }
        public int Capricorn { get; set; }
        public int Aquarius { get; set; }
        public int Pisces { get; set; }

    }
}