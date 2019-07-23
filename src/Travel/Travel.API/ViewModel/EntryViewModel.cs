using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.API.ViewModel
{
    public class EntryViewModel
    {
        public int IdCollection { get; set; }
        public DateTime Date { get; set; }
        public int IdTraveler { get; set; }
        public decimal Amount { get; set; }
    }
}
