using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace SatsTelegramBot
{
    public class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("805960903:AAGnFRDO08e0R7mGL_UcyMy0Kz9UJZuHDEw");
        
        static void Main(string[] args)
        {
            Bot.OnMessage += Bot_OnMessage;

            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var cmd = new Commands(Bot, e);

            Console.WriteLine(e.Message.From.Id.ToString());
            Console.WriteLine(e.Message.From.Username);

            try
            {
                if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                {
                    if (e.Message.Text.ToCharArray()[0] != '/') return;

                    var wordArray = e.Message.Text.Split(' ');
                    var command = wordArray[0].Replace("@sats_telegram_bot", "");

                    switch (command)
                    {
                        case "/start":
                            //cmd.Start();
                            break;
                        case "/roll":
                            cmd.RollDice();
                            break;
                        case "/daddy":
                            cmd.CallMeDaddy();
                            break;
                        case "/insult":
                            cmd.InsultReplied();
                            break;
                        case "/weather":
                            cmd.ShowWeather();
                            break;
                        case "/summon":
                            cmd.SummonPerson();
                            break;
                        case "/feedback":
                            cmd.CreateFeedback();
                            break;
                        default:
                            Bot.SendTextMessageAsync(e.Message.Chat.Id, "Your query is unrecognized, make it better, dipshit.");
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                cmd.Error(exception);
            }
        }
    }
}
