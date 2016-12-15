using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VkNet;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace DataCollection
{
    public class DataCollection
    {
        private VkApi api = new VkApi();

        /// <summary>
        /// Authorize to VK.
        /// </summary>
        /// <param name="login">Account's login.</param>
        /// <param name="pass">Account's password.</param>
        public void Authorize(string login, string pass)
        {
            var authparams = new ApiAuthParams
            {
                ApplicationId = 5779403,
                Login = login,
                Password = pass,
                Settings = Settings.All
            };
            api.Authorize(authparams);
        }

        /// <summary>
        /// Represents a couple.
        /// </summary>
        class RelationPair
        {
            public Relative Partner1 { get; set; }
            public Relative Partner2 { get; set; }
        }

        /// <summary>
        /// Represents person.
        /// </summary>
        class Relative : IEquatable<Relative>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Day { get; set; }
            public int Month { get; set; }

            public int Zodiac => (Month - (Day <= 20 ? 4 : 3) + 24)%12;

            public override string ToString()
            {
                return $"{FirstName};{LastName};{Day};{Month};{Zodiac};";
            }

            public bool Equals(Relative other)
            {
                return FirstName.Equals(other.FirstName) &&
                       LastName.Equals(other.LastName) &&
                       (Day == other.Day) &&
                       (Month == other.Month);
            }
        }

        /// <summary>
        /// Collect Relation couples from VK and stores them in folder inside mainDir.
        /// </summary>
        /// <param name="mainDir"></param>
        public void Collect(DirectoryInfo mainDir)
        {
            // Create directory if there is no one
            if (!mainDir.Exists) mainDir.Create();
            Console.WriteLine("Enter login");
            string login = Console.ReadLine();
            Console.WriteLine("Enter password");
            string password = Console.ReadLine();
            Authorize(login, password);

            // Directory to store results
            DirectoryInfo d = new DirectoryInfo(mainDir.FullName + "/tables_" +
                                                $"{DateTime.Now.ToShortDateString()}____{DateTime.Now.ToShortTimeString()}"
                                                    .Replace(":", "."));

            // Way to handle doubling of info
            string prevNames =
                mainDir.GetDirectories()
                    .ToList()
                    .ConvertAll(dir => dir.GetFiles().ToList().Aggregate("", (s, info) => s + info.Name))
                    .Aggregate("", (s, info) => s + info);
            d.Create();

            // Search for users
            var search = api.Users.Search(new UserSearchParams()
            {
                Status = MaritalStatus.Engaged | MaritalStatus.InLove | MaritalStatus.Married | MaritalStatus.Meets,
                Fields = ProfileFields.All,
                Count = 1000
            }).ToList();

            // Look through each one's friends to find people with Relation partners
            search.ForEach(u =>
            {
                if (prevNames.Contains($"{u.FirstName}_{u.LastName}"))
                {
                    return;
                }
                List<User> users = new List<User>();

                if (u.RelationPartner != null)
                    users.Add(u);

                try
                {
                    users.AddRange(api.Friends.Get(new FriendsGetParams()
                    {
                        Fields = ProfileFields.All,
                        UserId = u.Id
                    }));
                }
                catch (Exception ex)
                {
                    return;
                }
                List<RelationPair> pairs = new List<RelationPair>();

                users.ForEach(user =>
                {
                    User anotherUser = null;
                    // Check if user has partner mentioned in the profile
                    if (user.RelationPartner != null)
                    {
                        // Handle temporary vk api database errors
                        while (anotherUser == null)
                            try
                            {
                                anotherUser = api.Users.Get(user.RelationPartner.Id, ProfileFields.All);
                            }
                            catch
                            {
                            }

                        // Check if users have their birthdate filled
                        if (user.BirthDate != null && anotherUser.BirthDate != null)
                            pairs.Add(new RelationPair()
                            {
                                Partner1 = new Relative()
                                {
                                    Day = int.Parse(user.BirthDate.Split('.')[0]),
                                    Month = int.Parse(user.BirthDate.Split('.')[1]),
                                    FirstName = user.FirstName,
                                    LastName = user.LastName
                                },
                                Partner2 = new Relative()
                                {
                                    Day = int.Parse(anotherUser.BirthDate.Split('.')[0]),
                                    Month = int.Parse(anotherUser.BirthDate.Split('.')[1]),
                                    FirstName = anotherUser.FirstName,
                                    LastName = anotherUser.LastName
                                }
                            });
                    }
                });

                // Write data of pairs in friendlist of each person in search results to separate file
                using (
                    StreamWriter sw1 =
                        new StreamWriter(
                            $"{d.FullName}\\relTable_{pairs.Count}_pairs_{u.FirstName}_{u.LastName}'s_friends.csv",
                            false, Encoding.Default))
                {
                    pairs.ForEach(pair => { sw1.WriteLine($"{pair.Partner1};{pair.Partner2}\n"); });
                }
            });
        }

        /// <summary>
        /// Join data of all files to one file
        /// </summary>
        public void UniteData(DirectoryInfo mainDir)
        {
            int doubles = 0;
            if (!mainDir.Exists) return;

            List<RelationPair> pairs = new List<RelationPair>();

            mainDir.GetDirectories().ToList().ForEach(dir =>
            {
                dir.GetFiles("*.csv").ToList().ForEach(file =>
                {
                    File.ReadAllLines(file.FullName, Encoding.Default).ToList().ForEach(line =>
                    {
                        string[] fields = line.Split(';');
                        if (fields.Length < 10) return;
                        RelationPair r = new RelationPair()
                        {
                            Partner1 = new Relative()
                            {
                                FirstName = fields[0],
                                LastName = fields[1],
                                Day = int.Parse(fields[2]),
                                Month = int.Parse(fields[3])
                            },
                            Partner2 = new Relative()
                            {
                                FirstName = fields[6],
                                LastName = fields[7],
                                Day = int.Parse(fields[8]),
                                Month = int.Parse(fields[9])
                            }
                        };
                        if (pairs.Find(p => (r.Partner1.Equals(p.Partner1)) | (r.Partner1.Equals(p.Partner2))) == null)
                            pairs.Add(r);
                        else
                            doubles++;
                    });
                });
            });
            using (StreamWriter sw = new StreamWriter(mainDir.FullName + "/All_data.csv", false, Encoding.Default))
            {
                pairs.ForEach(p=>sw.WriteLine($"{p.Partner1}{p.Partner2}"));
            }
            Console.WriteLine($"{pairs.Count} pairs in total\n{doubles} doubles in total");
        }
    }
}