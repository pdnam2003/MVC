using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMarksMVC.Models
{
    [Table("Students")]
    public class Student
    {
        public int StudentId { get; set; }        // SQL: StudentId
        public string FullName { get; set; }      // SQL: FullName
        public string Grade { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;   


        public ICollection<Score> Scores { get; set; }
    }
}
