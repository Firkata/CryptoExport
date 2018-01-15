using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace CryptoExport
{
    class Program
    {
        static string path = Directory.GetCurrentDirectory();
        static string file = path + "\\coins.csv";
        static System.Timers.Timer timer = new System.Timers.Timer();
        static void Main(string[] args)
        {
            Console.WriteLine("=============================  Welcome to CryptoExport  =============================");
            
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            if (!File.Exists(file))
            {
                string header = string.Format("Date,BTC/USD,BNB/USD,{0}",Environment.NewLine);
                File.WriteAllText(file, header);
            }

            while (Console.Read() != 'q') ;
        }
        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Interval = 5000;
            List<Coin> binance;
            List<Coin> bitcoin;
            List<Coin> allCoins = new List<Coin>();

            string coins = string.Empty;
            string time = DateTime.Now.ToString("hh:mm:ss");

            using (WebClient wc = new WebClient())
            {
                binance = JsonConvert.DeserializeObject<List<Coin>>(wc.DownloadString(Url.binanceUrl));
                bitcoin = JsonConvert.DeserializeObject<List<Coin>>(wc.DownloadString(Url.bitcoinUrl));
                allCoins.AddRange(bitcoin);
                allCoins.AddRange(binance);
            }

            for (int i = 0; i < allCoins.Count; i++)
            {
                coins = coins + allCoins[i].PriceUsd + ",";
            }

            File.AppendAllText(file, string.Format("{0},{1}{2}", time, coins, Environment.NewLine));
            Console.WriteLine("Last updated at " + time);

            #region Comment
            //---get first 100 coins----
            //List<Coin> allCoins = new List<Coin>();

            //string url = "https://api.coinmarketcap.com/v1/ticker";
            //using (WebClient wc = new WebClient())
            //{
            //    allCoins.AddRange(JsonConvert.DeserializeObject<List<Coin>>(wc.DownloadString(url)));
            //}
            //string date = DateTime.Now.ToString("hh:mm:ss");
            //var line = string.Empty;

            //line = string.Join(",", allCoins);
            //File.AppendAllText(file, date + "," + line + Environment.NewLine);

            //----------------------------------------------------------------------

            //foreach (Coin coin in allCoins)
            //{
            //    File.AppendAllText(file, DateTime.Now.ToString("hh:mm:ss") + ",");
            //    foreach (PropertyInfo info in coin.GetType().GetProperties())
            //    {
            //        string propertyInfo = coin.GetType().GetProperty(info.Name).GetValue(coin).ToString();
            //        File.AppendAllText(file, propertyInfo + ",");
            //    }
            //}
            #endregion
        }
    }
}
