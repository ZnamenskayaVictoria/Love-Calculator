using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace LoveCalculatorApp.VKInteraction
{
    class Publisher
    {
        private ulong appID = 5779403;
        
        public void Share(string login, string password, string message)
        {
            VkApi api = new VkApi();
            api.Authorize(new ApiAuthParams
            {
                ApplicationId = appID,
                Login = login,
                Password = password,
                Settings = Settings.All
            });

            api.Wall.Post(new WallPostParams
            {
                OwnerId = api.UserId,
                Message = message
            });
        }
    }
}
