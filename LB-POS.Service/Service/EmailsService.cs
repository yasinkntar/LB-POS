using LB_POS.Data.Helpers;
using LB_POS.Service.IService;
using MailKit.Net.Smtp;
using MimeKit;

namespace LB_POS.Service.Service
{
    public class EmailsService : IEmailsService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion
        #region Constructors
        public EmailsService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        #endregion
        #region Handle Functions
        public async Task<string> SendEmail(string email, string Message, string? reason)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    // التعديل هنا: استخدم SecureSocketOptions.StartTls مع المنفذ 587
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

                    // تأكد أن _emailSettings.Password هي الـ 16 حرفاً بدون مسافات
                    await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);

                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = Message, // تأكد أن المتغير Message يحتوي على محتوى
                        TextBody = "welcome",
                    };

                    var mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress("Future Team", _emailSettings.FromEmail));
                    mimeMessage.To.Add(new MailboxAddress("testing", email));
                    mimeMessage.Subject = reason ?? "No Submitted";
                    mimeMessage.Body = bodybuilder.ToMessageBody();

                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
        #endregion
    }
}