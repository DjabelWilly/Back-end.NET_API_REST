using System;

namespace P7CreateRestApi.Entities
{
    public class BidList
    {
        public int Id { get; set; }
        public string Account { get; set; } = null!;
        public string BidType { get; set; } = null!;
        public double? BidQuantity { get; set; }
        public double? AskQuantity { get; set; }
        public double? Bid { get; set; }
        public double? Ask { get; set; }
        public string Benchmark { get; set; } = null!;
        public DateTime? BidListDate { get; set; }
        public string Commentary { get; set; } = null!;
        public string BidSecurity { get; set; } = null!;
        public string BidStatus { get; set; } = null!;
        public string Trader { get; set; } = null!;
        public string Book { get; set; } = null!;
        public string CreationName { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public string RevisionName { get; set; } = null!;
        public DateTime? RevisionDate { get; set; }
        public string DealName { get; set; } = null!;
        public string DealType { get; set; } = null!;
        public string SourceListId { get; set; } = null!;
        public string Side { get; set; } = null!;
    }
}
