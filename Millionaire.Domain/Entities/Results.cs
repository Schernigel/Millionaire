using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millionaire.Domain.Entities
{
    public class Results
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResultsId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public int QuestionNumber { get; set; }

        [Required]
        public int Prize { get; set; }

        [Required]
        public int FiftyFifty { get; set; }

        [Required]
        public int Call { get; set; }

        [Required]
        public int ViewersHelp { get; set; }

        //navigation

        public virtual User user { get; set; }

    }
}
