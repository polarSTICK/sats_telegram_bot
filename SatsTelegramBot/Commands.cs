using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Text.RegularExpressions;

namespace SatsTelegramBot
{
    public class Commands
    {
        private readonly TelegramBotClient _bot;
        private readonly MessageEventArgs _messageEventArgs;
        private readonly Random _rnd = new Random();

        public Commands(TelegramBotClient bot, MessageEventArgs messageEventArgs)
        {
            _bot = bot;
            _messageEventArgs = messageEventArgs;
        }

        public void Start()
        {
            _bot.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, @"Usage:
/roll - To roll the dice. Specify a number of the dice and how many sides the dice needs to have. Like this '4d6'
");
        }

        public void RollDiceNew() //Todo: hit counting, dice roller
        {
            if (_messageEventArgs.Message.Text.Contains("d"))
            {
                var message = _messageEventArgs.Message.Text.Replace("/roll ", "");

                int total = 0;

                var rawDice = message.Split('d');

                int numberOfDice = int.Parse(rawDice[0]);
                int numberOfSides = int.Parse(rawDice[1]);

                string[] allRolledDice = new string[numberOfDice];

                for (int i = 0; i < numberOfDice; i++)
                {
                    var rolledDice = _rnd.Next(1, numberOfSides);
                    total += rolledDice;
                    allRolledDice[i] = rolledDice.ToString();
                }

                string result = string.Join(", ", allRolledDice.OrderByDescending(x => x));

                _bot.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, $"Rolled dice: {result} \nwith total: {total}");
            }
            else
            {
                _bot.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "specify the amount of dice and kind of dice.");
                _bot.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "dipshit.");
            }
        }

        public void RollDice()
        {
            Regex rx = new Regex(@"\/\b\broll\b(\s\d+[d]\d+)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (!rx.IsMatch(_messageEventArgs.Message.Text))
            {
                Error();
                _bot.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "dipshit.");
                return;
            }

            var stringArray = _messageEventArgs.Message.Text.Split(' ');

            for (int i = 1; i < stringArray.Length; i++)
            {
                var rawDice = stringArray[i].Split('d');

                var total = 0;

                int diceAmount = int.Parse(rawDice[0]);
                int sides = int.Parse(rawDice[1]);

                int[] allRolledDice = new int[diceAmount];

                for (int j = 0; j < diceAmount; j++)
                {
                    var rolledDice = _rnd.Next(1, sides);
                    total += rolledDice;
                    allRolledDice[j] = rolledDice;
                }

                var result = string.Join(", ", allRolledDice.OrderByDescending(x => x));

                _bot.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, $"Rolled d{sides}: {result} \nwith total: {total}");
            }
        }

        public void CallMeDaddy()
        {
            throw new NotImplementedException();
        }

        public void InsultReplied()
        {
            throw new NotImplementedException();
        }

        public void ShowWeather()
        {
            throw new NotImplementedException();
        }

        public void SummonPerson()
        {
            throw new NotImplementedException();
        }

        public void CreateFeedback()
        {
            throw new NotImplementedException();
        }

        public void Error(Exception e = null)
        {
            if (e != null)
            {
                _bot.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, $"Would you look at that, you made me crash. Good job, you twat. Enjoy the error message: {e.Message}");
                return;
            }

            _bot.SendTextMessageAsync(_messageEventArgs.Message.Chat.Id, "Input was incorrect");
        }
    }
}
