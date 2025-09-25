namespace StudentMarksMVC.ViewModels
{
    public class StudentWithScoresViewModel
    {
        public int StudentId { get; set; }
        public string FullName { get; set; } = "";
        public string Grade { get; set; } = "";
        public string Class { get; set; } = "";
        public double Math { get; set; }
        public double Science { get; set; }
        public double English { get; set; }
        public double History { get; set; }
        public double Art { get; set; }


        public double Average => (Math + Science + English + History + Art) / 8.0;
    }
}
