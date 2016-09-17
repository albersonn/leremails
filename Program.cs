using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LerEmail
{

    public static class Program
    {
        const int maxChars = 32767;

        static void Main(string[] args)
        {
            try
            {
                var pasta = ConfigurationManager.AppSettings["Pasta"];
                if (string.IsNullOrWhiteSpace(pasta) == false)
                {
                    using (var db = new LerEmailsContext())
                    {
                        ReadMail(db, pasta);
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Processo concluído");
                    }
                }
                else
                {
                    Console.WriteLine("Uso: LerEmails -p <Pasta para ler os e-mails>");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Ocorreu um erro");
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine(ex.StackTrace);
                var inner = ex.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                Console.ReadKey();
            }
        }

        static void ReadMail(LerEmailsContext db, string pasta)
        {
            Microsoft.Office.Interop.Outlook.Application app = null;
            Microsoft.Office.Interop.Outlook._NameSpace ns = null;
            Microsoft.Office.Interop.Outlook.MAPIFolder inboxFolder = null;


            Console.WriteLine("Tentando abrir o outlook para a consulta");
            app = new Microsoft.Office.Interop.Outlook.Application();
            ns = app.GetNamespace("MAPI");
            //ns.Logon(null, null, false, false);

            //inboxFolder = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);

            Console.Write("Buscando a pasta {0}", pasta);
            inboxFolder = app.GetFolderPath(pasta);

            // subFolder = inboxFolder.Folders["Inbox"]; //folder.Folders[1]; also works
            Console.WriteLine("Folder Name: {0}, EntryId: {1}", inboxFolder.Name, inboxFolder.EntryID);
            Console.WriteLine("Num Items: {0}", inboxFolder.Items.Count.ToString());


            //System.IO.StreamWriter strm = new System.IO.StreamWriter("C:/Test/Inbox.txt");
            for (int counter = 1; counter <= inboxFolder.Items.Count; counter++)
            {
                var email = db.Emails.Add(new Models.Email());
                dynamic item = inboxFolder.Items[counter];
                //item = (Microsoft.Office.Interop.Outlook.MailItem)inboxFolder.Items[counter];
                Console.WriteLine($"Item: {counter.ToString()}");

                email.Subject = item.Subject;
                email.Data = (item.SentOn as DateTime?)?.ToString("dd/MM/yyyy");
                email.Hora = (item.SentOn as DateTime?)?.ToString("HH:mm:ss");
                email.From = item.SenderEmailAddress;
                email.Cc = item.CC;
                email.Categories = item.Categories;
                email.To = item.To;
                email.Body = item.Body;

                Console.WriteLine(email.ToString());
                //strm.WriteLine(counter.ToString() + "," + item.Subject + "," + item.SentOn.ToShortDateString() + "," + item.SenderName);

            }

            db.SaveChanges();
            //strm.Close();
        }

        public static Microsoft.Office.Interop.Outlook.MAPIFolder GetFolderPath(this Microsoft.Office.Interop.Outlook.Application app, string folderPath)
        {
            var foldersArray = folderPath.Split('\\');
            var folder = app.Session.Folders[foldersArray[0]];
            Microsoft.Office.Interop.Outlook.MAPIFolder result = null;
            if (folder != null)
            {
                for (var i = 1; i < foldersArray.Length; i++)
                {
                    var subFolders = folder.Folders;
                    folder = subFolders[foldersArray[i]];
                    if (folder != null)
                        result = folder;
                }
            }
            return result;
        }
    }
}
