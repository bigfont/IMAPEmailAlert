using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailAlertService
{
    public class UserService : System.ServiceProcess.ServiceBase
    {
        public static void Main()
        {
            System.ServiceProcess.ServiceBase.Run(new UserService());
        }
        public UserService()
        {
            this.ServiceName = "EmailAlertService";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }
        protected override void OnStart(string[] args)
        {
            try {
                EmailAlertClassLibrary.ImapAndSmtp imapAndSmtp = new EmailAlertClassLibrary.ImapAndSmtp();
                imapAndSmtp.CountEmailsAndSendAlert();
            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }

        }
        protected override void OnStop()
        {
            // TODO: add shutdown stuff
        }
    }
}