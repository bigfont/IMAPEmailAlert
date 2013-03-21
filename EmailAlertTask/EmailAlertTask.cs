using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailAlertTask
{
    class EmailAlertTask
    {
        static void Main(string[] args)
        {
            try
            {
                EmailAlertClassLibrary.ImapAndSmtp imapAndSmtp = new EmailAlertClassLibrary.ImapAndSmtp();
                imapAndSmtp.CountEmailsAndSendAlert();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
    }
}
