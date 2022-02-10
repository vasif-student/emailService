using Api.Tools.EmailHandler.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Tools.EmailHandler.Implementation
{
    public class SendgridService : IEmailService
    {
        public Task SendEmailAsync(MailRequest mailRequest)
        {
            throw new NotImplementedException();
        }
    }
}
