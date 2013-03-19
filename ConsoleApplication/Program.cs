using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Email.Net.Imap.ImapClient client = new Email.Net.Imap.ImapClient();
            
            // Authenticate
            // ----------------------

            //Create IMAP4 client with parameters needed
            //URL of host to connect to
            client.Host = "imap.gmail.com";
            //TCP port for connection
            client.Port = (ushort)993;
            //Username to login to the IMAP server
            client.Username = "admin@shaunluttin.com";
            //Password to login to the IMAP server
            client.Password = "eP6249*K6@";
            //Interaction type
            client.SSLInteractionType = Email.Net.Common.Configurations.EInteractionType.SSLPort;
            //Login to the server
            Email.Net.Imap.Responses.CompletionResponse response = (Email.Net.Imap.Responses.CompletionResponse)client.Login();
            if (response.CompletionResult == Email.Net.Imap.Responses.ECompletionResponseType.OK)
            {
                Console.WriteLine("Login succeeded.");
            }
            else
            {
                Console.WriteLine("Login failed.");
            }                        

            // Retrieve Messages
            // ------------------------

            Email.Net.Imap.Mailbox folders = client.GetMailboxTree();
            //Get inbox folder
            Email.Net.Imap.Mailbox inbox = Email.Net.Imap.Mailbox.Find(folders, "INBOX");
            Email.Net.Imap.Collections.MessageCollection tmp = client.GetAllMessageHeaders(inbox);
            //Count unread messages in INBOX
            //TODO Count unread messages that are in other folders too, just in case the user has filters to skip the inbox.
            int unseenMessageCount = tmp.Count(e => 
            { 
                return ( !e.Flags.Contains(Email.Net.Imap.EFlag.Seen));
            });
            Console.WriteLine(string.Format("You have {0} unread messages.", unseenMessageCount.ToString()));
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
    }
}
