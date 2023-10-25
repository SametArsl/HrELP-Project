using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
