using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class BidListViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Account { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string BidType { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public double? BidQuantity { get; set; }

        [Range(0, double.MaxValue)]
        public double? AskQuantity { get; set; }

        [Range(0, double.MaxValue)]
        public double? Bid { get; set; }

        [Range(0, double.MaxValue)]
        public double? Ask { get; set; }

        [Required] public string Benchmark { get; set; } = null!;

        public DateTime? BidListDate { get; set; }

        [Required]
        [MaxLength(250)]
        public string Commentary { get; set; } = null!;

        [Required] public string BidSecurity { get; set; } = null!;

        [Required] public string BidStatus { get; set; } = null!;

        [Required] public string Trader { get; set; } = null!;

        [Required] public string Book { get; set; } = null!;

        [Required] public string CreationName { get; set; } = null!;

        public DateTime? CreationDate { get; set; }

        [Required] public string RevisionName { get; set; } = null!;

        public DateTime? RevisionDate { get; set; }

        [Required] public string DealName { get; set; } = null!;

        [Required] public string DealType { get; set; } = null!;

        [Required] public string SourceListId { get; set; } = null!;

        [Required] public string Side { get; set; } = null!;
    }
}