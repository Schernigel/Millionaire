using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Millionaire.Domain.Entities;

namespace Millionaire.WebUI.Models
{
    public class GameViewModel
    {
        public string GameQuestion { get; set; }

        public string First { get; set; }

        public string Second { get; set; }

        public string Third { get; set; }

        public string Fourth { get; set; }

        public int Id { get; set; }

       
    }
}