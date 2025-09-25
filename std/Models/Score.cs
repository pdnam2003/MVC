using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMarksMVC.Models
{
    [Table("Scores")]
    public class Score
    {
        public int ScoreId { get; set; }      // ánh xạ SQL: ScoreId
        public int StudentId { get; set; }    // ánh xạ SQL: StudentId
        public int SubjectId { get; set; }    // ánh xạ SQL: SubjectId

        // ⚡ Đây là phần bạn hỏi: đặt ngay trong class
        [Column("Score")]
        public int ScoreValue { get; set; }        // ánh xạ SQL: Score

        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
