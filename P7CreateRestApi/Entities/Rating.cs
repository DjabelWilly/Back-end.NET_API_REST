namespace P7CreateRestApi.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public string MoodysRating { get; set; } = null!;
        public string SandPRating { get; set; } = null!;
        public string FitchRating { get; set; } = null!;
        public byte? OrderNumber { get; set; }
    }

}