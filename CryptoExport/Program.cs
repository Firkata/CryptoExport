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
                string header = string.Format("Date,BTC/USD,ETH/USD,ETH/BTC,XRP/USD,XRP/BTC,MIOTA/USD,MIOTA/BTC,{0}", Environment.NewLine);
                File.WriteAllText(file, header);
            }

            while (Console.Read() != 'q') ;
        }
        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Interval = 5000;
            List<Coin> bitcoin;
            List<Coin> ethereum;
            List<Coin> ripple;
            List<Coin> iota;
            List<Coin> allCoins = new List<Coin>();

            string coins = string.Empty;
            string time = DateTime.Now.ToString("hh:mm:ss");

            using (WebClient wc = new WebClient())
            {
                bitcoin = JsonConvert.DeserializeObject<List<Coin>>(wc.DownloadString(Url.bitcoinUrl));
                ethereum = JsonConvert.DeserializeObject<List<Coin>>(wc.DownloadString(Url.ethereumUrl));
                ripple = JsonConvert.DeserializeObject<List<Coin>>(wc.DownloadString(Url.rippleUrl));
                iota = JsonConvert.DeserializeObject<List<Coin>>(wc.DownloadString(Url.iotaUrl));
                allCoins.AddRange(bitcoin);
                allCoins.AddRange(ethereum);
                allCoins.AddRange(ripple);
                allCoins.AddRange(iota);
            }

            coins = coins + allCoins[0].PriceUsd + ",";
            for (int i = 1; i < allCoins.Count; i++)
            {
                coins = coins + allCoins[i].PriceUsd + "," + allCoins[i].PriceBtc + ",";

            }
            try
            {
            File.AppendAllText(file, string.Format("{0},{1}{2}", time, coins, Environment.NewLine));
            }
            catch
            {
                Console.WriteLine("You are using the file in another program. Please close it to continue. " + "[" + time + "]");
                return;
            }
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
