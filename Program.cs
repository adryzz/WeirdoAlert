using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telegram.Bot;

namespace WeirdoAlert
{
    class Program
    {
        public static Logger MainLogger = new Logger();
        static TelegramBotClient Client;
        static List<int> AuthenticatedUsers = new List<int>();
        static Dictionary<int, int> WaitingUsers = new Dictionary<int, int>();//UserID and the result of the problem.
        public static string DomainName = "https://www.example.com/";
        static void Main(string[] args)
        {
            MainLogger.Log("Application Started!");
            Console.WriteLine("Application Started!");

            MainLogger.Log("Initializing Client...");
            Console.WriteLine("Initializing Client...");

            string token = File.ReadAllText("token.txt");

            Client = new TelegramBotClient(token);

            Client.OnMessage += Client_OnMessage;
            Client.OnMessageEdited += Client_OnMessage;

            MainLogger.Log("Client Initialized");
            Console.WriteLine("Client Initialized");

            MainLogger.Log("Starting Client...");
            Console.WriteLine("Starting Client...");

            Client.StartReceiving();

            MainLogger.Log("Client Started");
            Console.WriteLine("Client Started");

            throw new NotImplementedException();
        }

        private static void Client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            for(int i = 0; i < WaitingUsers.Count; i++)
            {
                if (e.Message.From.Id == WaitingUsers.Keys.ElementAt(i))
                {
                    if (e.Message.Text.Equals(WaitingUsers.Values.ElementAt(i).ToString()))
                    {
                        AuthenticatedUsers.Add(WaitingUsers.Keys.ElementAt(i));
                        WaitingUsers.Remove(i);
                        break;
                    }
                }
            }

            bool authenticated = false;
            foreach(int i in AuthenticatedUsers)
            {
                if (e.Message.From.Id == i)
                {
                    authenticated = true;
                    break;
                }
            }
            if (!authenticated)
            {
                Client.DeleteMessageAsync(e.Message.Chat, e.Message.MessageId);
                Client.SendTextMessageAsync(e.Message.Chat, /* tag user*/ ", Solve this problem to access the server\n24 + the number shown here: " + DomainName + '?' + e.Message.From.Id * 2);//multiply * 2 so smarter users who lnow their UID won't recognize this
            }
        }
    }
}
