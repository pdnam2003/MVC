using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMarksMVC.Models
{
    [Table("Subjects")]
    public class Subject
    {
        public int SubjectId { get; set; }        // SQL: SubjectId
        public string SubjectName { get; set; }   // SQL: SubjectName

        public ICollection<Score> Scores { get; set; }
    }
}
