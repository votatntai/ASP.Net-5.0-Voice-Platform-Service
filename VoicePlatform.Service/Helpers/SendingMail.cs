using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VoicePlatform.Service.Helpers
{
    public class SendingMail
    {
        public SendingMail()
        {

        }

        public async Task<object> Send(string email, string otp, string name)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("no-reply@voiceplatform.social", "Voice Platform");
            try
            {
                //Fetching Email Body Text from EmailTemplate File.  
                string FilePath = Directory.GetCurrentDirectory() + "\\OTPTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();

                //Repalce [VERIFICATION_CODE] = code
                MailText = MailText.Replace("[VERIFICATION_CODE]", otp);
                MailText = MailText.Replace("[NAME]", name);

                var subject = "Mã xác thực";
                var to = new EmailAddress(email);
                var plainTextContent = "Mã xác thực email";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, MailText);
                var response = await client.SendEmailAsync(msg);
                return true;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
