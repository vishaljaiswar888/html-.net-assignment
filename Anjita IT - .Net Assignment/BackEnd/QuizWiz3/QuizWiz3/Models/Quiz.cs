using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizWiz3.Models
{
    public class Quiz
    {
        [Key]
        public int quiz_Id { get; set; }

        [Required]
        public string question { get; set; }

        [Required]
        public string option1 { get; set; }

        [Required]
        public string option2 { get; set; }

        [Required]
        public string option3 { get; set; }

        [Required]
        public string option4 { get; set; }

        [Required]
        public string correct_Answer { get; set; }
    }
}
