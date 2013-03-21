using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    static class Program
    {
        private static void Main(string[] args)
        {
            Email.Net.Imap.ImapClient client;
            Email.Net.Imap.Collections.MessageCollection messageCollection;
            int unseenMessageCount;
            
            client = CreateImapClient();
            if (AuthenticateImapClientWithImapServer(client))
            {
                messageCollection = RetrieveMessagesFromImapServer(client);
                unseenMessageCount = CountUnseenMessages(messageCollection);
                SendAlertEmailToUsersSecondaryEmailAccount(unseenMessageCount);
            }
        }
        private static Email.Net.Imap.ImapClient CreateImapClient()
        {
            Email.Net.Imap.ImapClient client;
            client = new Email.Net.Imap.ImapClient();

            //Create IMAP4 client with parameters needed
            //URL of host to connect to
            client.Host = "imap.gmail.com";
            //TCP port for connection
            client.Port = (ushort)993;
            //Username to login to the IMAP server
            client.Username = "test@shaunluttin.com";
            //Password to login to the IMAP server
            client.Password = "password";
            //Interaction type
            client.SSLInteractionType = Email.Net.Common.Configurations.EInteractionType.SSLPort;

            return client;
        }
        private static bool AuthenticateImapClientWithImapServer(Email.Net.Imap.ImapClient client)
        {
            //Login to the server
            bool loginSuccess;
            Email.Net.Imap.Responses.CompletionResponse response = (Email.Net.Imap.Responses.CompletionResponse)client.Login();
            if (response.CompletionResult == Email.Net.Imap.Responses.ECompletionResponseType.OK)
            {
                loginSuccess = true;
                WriteToLogFile("Login succeeded.");
            }
            else
            {
                loginSuccess = false;
                WriteToLogFile("Login failed.");
            }
            return loginSuccess;
        }
        private static Email.Net.Imap.Collections.MessageCollection RetrieveMessagesFromImapServer(Email.Net.Imap.ImapClient client)
        {
            // Retrieve Messages
            // ------------------------
            Email.Net.Imap.Mailbox folders = client.GetMailboxTree();
            //Get inbox folder
            Email.Net.Imap.Mailbox inbox = Email.Net.Imap.Mailbox.Find(folders, "INBOX");
            Email.Net.Imap.Collections.MessageCollection messageCollection = client.GetAllMessageHeaders(inbox);
            //Count unread messages in INBOX
            //TODO Count unread messages that are in other folders too, just in case the user has filters to skip the inbox.

            return messageCollection;
        }
        private static int CountUnseenMessages(Email.Net.Imap.Collections.MessageCollection messageCollection)
        { 
            int unseenMessageCount = messageCollection.Count(e =>
            {
                return (!e.Flags.Contains(Email.Net.Imap.EFlag.Seen));
            });
            return unseenMessageCount;            
        }
        private static void SendAlertEmailToUsersSecondaryEmailAccount(int unseenMessageCount)
        {
            StringBuilder stringBuilder;
            System.Net.Mail.SmtpClient smtp;
            System.Net.Mail.MailMessage mailMessage;

            stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("You have {0} unread messages.", unseenMessageCount.ToString());
            WriteToLogFile(stringBuilder.ToString());

            smtp = new System.Net.Mail.SmtpClient();
            mailMessage= new System.Net.Mail.MailMessage();;

            // In addition to the web.configuration.system.net.mailSettings, 
            // the following is the only code needed to send an email with SendGrid.            
            mailMessage.To.Add("admin@shaunluttin.com");
            mailMessage.Subject = stringBuilder.ToString();
            mailMessage.Body = stringBuilder.ToString();       
            smtp.Send(mailMessage);
        }
        private static void WriteToLogFile(string message)
        {
            using (System.IO.StreamWriter outfile = new System.IO.StreamWriter("log.txt"))
            {
                outfile.Write(message);
            }
        }
    }
}
