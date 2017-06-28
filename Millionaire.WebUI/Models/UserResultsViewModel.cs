using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Millionaire.WebUI.Models
{
    public class UserResultsViewModel
    {
        public string Name { get; set; }
        public int GameCount { get; set; }
        public int TotalPrize { get; set; }
        public int Range { get; set; }
        public int WinPerGame { get; set; }
    }
}