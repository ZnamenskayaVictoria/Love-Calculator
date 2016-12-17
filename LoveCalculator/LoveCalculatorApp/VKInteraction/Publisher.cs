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
        
        /// <summary>
        /// Post message on the wall 
        /// </summary>
        /// <param name="login">Login to VK</param>
        /// <param name="password">Password</param>
        /// <param name="message">Message to post</param>
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
