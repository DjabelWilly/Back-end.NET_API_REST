using System;

namespace P7CreateRestApi.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public string Account { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public double? BuyQuantity { get; set; }
        public double? SellQuantity { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public DateTime? TradeDate { get; set; }
        public string TradeSecurity { get; set; } = null!;
        public string TradeStatus { get; set; } = null!;
        public string Trader { get; set; } = null!;
        public string? Benchmark { get; set; }
        public string? Book { get; set; }
        public string? CreationName { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? RevisionName { get; set; }
        public DateTime? RevisionDate { get; set; }
        public string? DealName { get; set; }
        public string? DealType { get; set; }
        public string? SourceListId { get; set; }
        public string? Side { get; set; }
    }

}