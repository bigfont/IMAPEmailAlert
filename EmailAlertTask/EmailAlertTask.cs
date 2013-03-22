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
                imapAndSmtp.CountEmailsAndSendAlert("test@shaunluttin.com", "4Wn2!XKfJF", "admin@shaunluttin.com");
                imapAndSmtp.CountEmailsAndSendAlert("chair@innovativecommunities.org", "richmond1984", "john@humanhorizons.net");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
    }
}
