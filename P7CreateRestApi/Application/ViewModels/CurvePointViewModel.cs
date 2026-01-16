using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Application.ViewModels
{
    public class CurvePointViewModel
    {
        public int Id { get; set; }

        [Range(0, 255, ErrorMessage = "CurveId doit être compris entre 0 et 255.")]
        public byte? CurveId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? AsOfDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Term doit être supérieur ou égal à 0.")]
        public double? Term { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "CurvePointValue doit être supérieur ou égal à 0.")]
        public double? CurvePointValue { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreationDate { get; set; }
    }
}
