using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millionaire.Domain.Entities
{
    public class UserStatistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int AnswerNumber { get; set; }

        [Required]
        public int Prompt { get; set; }
    }
}
