namespace P7CreateRestApi.Entities
{
    public class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Json { get; set; } = null!;
        public string Template { get; set; } = null!;
        public string SqlStr { get; set; } = null!;
        public string SqlPart { get; set; } = null!;
    }
}