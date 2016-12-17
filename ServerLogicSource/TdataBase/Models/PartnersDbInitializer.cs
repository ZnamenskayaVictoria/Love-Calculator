
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;

namespace TdataBase.Models
{
    //Data base initializer
    //You must put "All_data.csv" to your IIS Express directory
    public class PartnersDbInitializer : CreateDatabaseIfNotExists<PartnersContext>
    {
        List<Partners> ReadCsv(string path)
        {
            List<Partners> result = new List<Partners>();
            FileInfo a = new FileInfo(path);
            string s = a.DirectoryName;
            StreamReader reader = new StreamReader(path,System.Text.Encoding.Default);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split(';');
                result.Add(new Partners
                {
                    Name = data[0],
                    Surname = data[1],
                    Day = int.Parse(data[2]),
                    Month = int.Parse(data[3]),
                    Zodiac = int.Parse(data[4]),

                    SecName = data[5],
                    SecSurname = data[6],
                    SecDay = int.Parse(data[7]),
                    SecMonth = int.Parse(data[8]),
                    SecZodiac = int.Parse(data[9])
                });
            }
            return result;
        }
        protected override void Seed(PartnersContext db)
        {
            //Reading database from file and create filling the database
            List<Partners> partners = ReadCsv(@"All_data.csv");
            partners.ForEach(x => db.Partner.Add(x));            
            base.Seed(db);
        }
    }
}