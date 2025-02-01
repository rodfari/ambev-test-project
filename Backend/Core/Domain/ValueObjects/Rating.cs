using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Rating
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}