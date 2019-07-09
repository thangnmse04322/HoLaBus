using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HoLaBus.Utilities
{
    public class Helpers
    {
        public static string GetRandomTicket()
        {
            //string charactersLibrary = "qwertyuiopasdfghjklzxcvbnm1234567890";
            string charactersLibrary = "1234567890";
            var random = new Random();
            var randomTicket = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                int index = random.Next(0, charactersLibrary.Length - 1);
                randomTicket.Append(charactersLibrary[index]);
            }
            return randomTicket.ToString();
        }
        public static string GetRandomIdG()
        {
            //string charactersLibrary = "qwertyuiopasdfghjklzxcvbnm1234567890";
            string charactersLibrary = "1234567890qwertyuiopasdfghjklzxcvbnm";
            var random = new Random();
            var randomTicket = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                int index = random.Next(0, charactersLibrary.Length - 1);
                randomTicket.Append(charactersLibrary[index]);
            }
            return randomTicket.ToString();
        }

        public static string GetDateTime()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("HH:mm - dd/MM/yyyy");
          
        }

        public static string GetDateTime2()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("dd/MM/yyyy");

        }
        public static string GetRanDomPayMent()
        {
            string charactersLibrary = "1234567890";
            var random = new Random();
            var randomTicket = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                int index = random.Next(0, charactersLibrary.Length - 1);
                randomTicket.Append(charactersLibrary[index]);
            }
            return randomTicket.ToString();
        }
    }
}