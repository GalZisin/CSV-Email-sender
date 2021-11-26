using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments, string path);

    }
}
