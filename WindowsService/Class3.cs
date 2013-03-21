﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    class Class3
    {
        private Email.Net.Imap.ImapClient client;

        private void AuthenticateWithImapServer()
        {
            client = new Email.Net.Imap.ImapClient();

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
                WriteToLogFile("Login succeeded.");
            }
            else
            {
                WriteToLogFile("Login failed.");
            }
        }

        private void RetrieveMessagesFromImapServer()
        {
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
                return (!e.Flags.Contains(Email.Net.Imap.EFlag.Seen));
            });
            WriteToLogFile(string.Format("You have {0} unread messages.", unseenMessageCount.ToString()));
        }

        private void WriteToLogFile(string message)
        {
            using (System.IO.StreamWriter outfile = new System.IO.StreamWriter("log.txt"))
            {
                outfile.Write(message);
            }
        }
    }
}
