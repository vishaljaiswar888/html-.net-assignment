using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizWiz3.Models
{
    public class Submit
    {
        [Key]
        public int submit_Id { get; set; }

        [Required]
        public string user_Email { get; set; }

        [ForeignKey("Quiz")]
        public int quiz_Id { get; set; }

        [Required]
        public string user_Answer { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}
