using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class BidListViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ Account est obligatoire.")]
        [MaxLength(50, ErrorMessage = "Le champ Account ne peut pas dépasser 50 caractères.")]
        public string Account { get; set; } = null!;

        [Required(ErrorMessage = "Le champ BidType est obligatoire.")]
        [MaxLength(50)]
        public string BidType { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "BidQuantity doit être supérieur ou égal à 0.")]
        public double? BidQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "AskQuantity doit être supérieur ou égal à 0.")]
        public double? AskQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Bid doit être supérieur ou égal à 0.")]
        public double? Bid { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Ask doit être supérieur ou égal à 0.")]
        public double? Ask { get; set; }

        [Required]
        public string Benchmark { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime? BidListDate { get; set; }

        [Required]
        [MaxLength(250)]
        public string Commentary { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string BidSecurity { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string BidStatus { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Trader { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Book { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string CreationName { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime? CreationDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string RevisionName { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime? RevisionDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string DealName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string DealType { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string SourceListId { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string Side { get; set; } = null!;
    }
}
