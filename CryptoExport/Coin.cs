﻿using Newtonsoft.Json;

namespace CryptoExport
{
    class Coin
    {
        //[JsonProperty("id")]
        //public string Id { get; set; }

        //[JsonProperty("name")]
        //public string Name { get; set; }

        //[JsonProperty("symbol")]
        //public string Symbol { get; set; }

        //[JsonProperty("rank")]
        //public string Rank { get; set; }

        [JsonProperty("price_usd")]
        public string PriceUsd { get; set; }

        [JsonProperty("price_btc")]
        public string PriceBtc { get; set; }

        //[JsonProperty("24h_volume_usd")]
        //public string VolumeUsd24H { get; set; }

        //[JsonProperty("market_cap_usd")]
        //public string MarketCap { get; set; }

        //[JsonProperty("available_supply")]
        //public string AvailableSupply { get; set; }

        //[JsonProperty("lototal_supply")]
        //public string TotalSupply { get; set; }

        //[JsonProperty("max_supply")]
        //public object MaxSupply { get; set; }

        //[JsonProperty("percent_change_1h")]
        //public string PercentChangeHour { get; set; }

        //[JsonProperty("percent_change_24h")]
        //public string PercentChangeDay { get; set; }

        //[JsonProperty("percent_change_7d")]
        //public string PercentChangeWeek { get; set; }

        //[JsonProperty("last_updated")]
        //public string LastUpdated { get; set; }

        //public override string ToString()
        //{
        //    return PriceUsd;
        //}

    }
}
