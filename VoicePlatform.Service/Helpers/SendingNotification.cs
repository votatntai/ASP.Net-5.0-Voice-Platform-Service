using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Service.Helpers
{
    public class SendingNotification
    {
        private readonly InitFirebaseApp _firebaseApp = new InitFirebaseApp();
        public SendingNotification()
        {
        }

        public async Task<bool> Push(List<string> token, string title, string body)
        {
            FirebaseMessaging.GetMessaging(_firebaseApp.Init());
            var message = new MulticastMessage()
            {
                Tokens = token,
                Notification = new Notification()
                {
                    Title = title,
                    Body = body
                },
                Android = new AndroidConfig()
                {
                    Priority = Priority.High
                },
            };
            var res = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message).ConfigureAwait(false);
            if (res.SuccessCount > 0)
            {
                return true;
            }
            return false;
        }
        
        public async Task<bool> PushAll()
        {
            FirebaseMessaging.GetMessaging(_firebaseApp.Init());
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = "Voice Platform",
                    Body = "Chào mừng bạn tới với Voice Platform. Cảm ơn bạn đã dùng đến phần mềm của nhóm mình. <3 Form VoicePlatform with LOVE <3"
                },
                Android = new AndroidConfig()
                {
                    Priority = Priority.High
                },
                Topic = "voice"
            };
            var res = await FirebaseMessaging.DefaultInstance.SendAsync(message).ConfigureAwait(false);
            if (string.IsNullOrEmpty(res))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> PushToAdmin(string title, string body)
        {
            FirebaseMessaging.GetMessaging(_firebaseApp.Init());
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = title,
                    Body = body
                },
                Android = new AndroidConfig()
                {
                    Priority = Priority.High
                },
                Topic = "admin"
            };
            var res = await FirebaseMessaging.DefaultInstance.SendAsync(message).ConfigureAwait(false);
            if (string.IsNullOrEmpty(res))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SubscribeTokenToTopic(IEnumerable<string> token, string topic)
        {
            FirebaseMessaging.GetMessaging(_firebaseApp.Init());
            if (token.Any())
            {
                var response = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(
                    token.ToList(), topic);
                return response.SuccessCount > 0 ? true : false;
            }
            return false;
        }
        
        public async Task<bool> UnsubscribeTokenToTopic(IEnumerable<string> token, string topic)
        {
            FirebaseMessaging.GetMessaging(_firebaseApp.Init());
            if (token.Any())
            {
                var response = await FirebaseMessaging.DefaultInstance.UnsubscribeFromTopicAsync(
                    token.ToList(), topic);
                return response.SuccessCount > 0 ? true : false;
            }
            return false;
        }

    }
}
