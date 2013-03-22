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
       
                // Paste PrivateUserData.txt contents here.
                // eg. imapAndSmtp.CountEmailsAndSendAlert("icoEmail", "icoPassword", "secondaryEmail");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
    }
}
