using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class TradeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le compte est obligatoire.")]
        [MaxLength(50, ErrorMessage = "Le compte ne peut pas dépasser 50 caractères.")]
        public string Account { get; set; } = null!;

        [Required(ErrorMessage = "Le type de compte est obligatoire.")]
        [MaxLength(50)]
        public string AccountType { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "BuyQuantity doit être supérieur ou égal à 0.")]
        public double? BuyQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "SellQuantity doit être supérieur ou égal à 0.")]
        public double? SellQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "BuyPrice doit être supérieur ou égal à 0.")]
        public double? BuyPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "SellPrice doit être supérieur ou égal à 0.")]
        public double? SellPrice { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? TradeDate { get; set; }

        [Required(ErrorMessage = "TradeSecurity est obligatoire.")]
        [MaxLength(50)]
        public string TradeSecurity { get; set; } = null!;

        [Required(ErrorMessage = "TradeStatus est obligatoire.")]
        [MaxLength(50)]
        public string TradeStatus { get; set; } = null!;

        [Required(ErrorMessage = "Trader est obligatoire.")]
        [MaxLength(50)]
        public string Trader { get; set; } = null!;

        [MaxLength(50)]
        public string? Benchmark { get; set; }

        [MaxLength(50)]
        public string? Book { get; set; }

        [MaxLength(50)]
        public string? CreationName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreationDate { get; set; }

        [MaxLength(50)]
        public string? RevisionName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? RevisionDate { get; set; }

        [MaxLength(50)]
        public string? DealName { get; set; }

        [MaxLength(50)]
        public string? DealType { get; set; }

        [MaxLength(50)]
        public string? SourceListId { get; set; }

        [MaxLength(10)]
        public string? Side { get; set; }
    }
}
