using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.API.ViewModel
{
    public class TripItem
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int IdDriver { get; set; }
        public string Commentary { get; set; }
    }
}
