using FirebaseAdmin;
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
    class InitFirebaseApp
    {
        public FirebaseApp Init()
        {
            var path = Directory.GetCurrentDirectory() + "\\voiceplatform-73d6e-firebase-adminsdk-xbb96-34c8d89544.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            var app = FirebaseApp.DefaultInstance;
            if (app is null)
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.GetApplicationDefault(),
                });
            }
            return app;
        }
    }
}
