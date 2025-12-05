using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class CurvePointViewModel
    {
        public int Id { get; set; }

        [Range(0, 255, ErrorMessage = "CurveId doit être entre 0 et 255")]
        public byte? CurveId { get; set; }

        public DateTime? AsOfDate { get; set; }
        
        [Range(0, double.MaxValue)]
        public double? Term { get; set; }

        [Range(0, double.MaxValue)]
        public double? CurvePointValue { get; set; }

        public DateTime? CreationDate { get; set; }
    }

}
