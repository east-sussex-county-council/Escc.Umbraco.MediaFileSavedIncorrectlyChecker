using Escc.Services;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker
{
    internal class EmailLogger : ILogger
    {
        private readonly Uri _baseUrl;
        private readonly string _emailFrom;
        private readonly string _emailTo;

        public EmailLogger(Uri baseUrl, string emailFrom, string emailTo)
        {
            if (baseUrl == null) throw new ArgumentNullException(nameof(baseUrl));
            _baseUrl = baseUrl;

            if (String.IsNullOrEmpty(emailFrom)) throw new ArgumentNullException(nameof(emailFrom));
            _emailFrom = emailFrom;

            if (String.IsNullOrEmpty(emailTo)) throw new ArgumentNullException(nameof(emailTo));
            _emailTo = emailTo;
        }

        public void Log(IEnumerable<string> files)
        {
            var emailService = ServiceContainer.LoadService<IEmailSender>(new ConfigurationServiceRegistry());

            var mailMessage = new MailMessage();
            mailMessage.Subject = "Multiple files uploaded to the same folder in Umbraco";
            mailMessage.From = new MailAddress(_emailFrom);
            mailMessage.To.Add(_emailTo);
            mailMessage.IsBodyHtml = true;

            var emailHtml = new StringBuilder();
            emailHtml.Append("<p>The following files have been uploaded to the same media folder due to a bug in Umbraco. They need to be re-uploaded, otherwise when one gets deleted the other one(s) will be deleted as well without warning.</p>");
            emailHtml.Append("<ul>");
            foreach(var file in files)
            {
                var url = new Uri(_baseUrl, file);
                emailHtml.Append("<li><a href=\"").Append(url.ToString()).Append("\">").Append(file).Append("</a></li>");
            }
            emailHtml.Append("</ul>");
            emailHtml.Append("<p>You can leave the first item in this list alone. For each subsequent item, follow these steps:</p>");
            emailHtml.Append("<ol>");
            emailHtml.Append("<li>Download a copy of the file by clicking on the link above</li>");
            emailHtml.Append("<li>Find the media item for this file using the 'Media search' tool in the 'Editor tools' section of Umbraco</li>");
            emailHtml.Append("<li>Tick 'Remove file(s)' and click Save to remove the file from the media item</li>");
            emailHtml.Append("<li>Click 'Choose file', select the copy you downloaded, and click Save to upload it to a new folder</li>");
            emailHtml.Append("</ol>");
            emailHtml.Append("<p>If there is no media item for one of these files, you can ask a developer to delete the file to prevent this warning reoccurring.</p>");


            mailMessage.Body = emailHtml.ToString();

            emailService.SendAsync(mailMessage);
        }
    }
}