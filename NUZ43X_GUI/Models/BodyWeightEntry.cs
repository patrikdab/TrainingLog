using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUZ43X_GUI.Models
{
    public class BodyWeightEntry
    {
       
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Weight { get; set; }
        public string Notes { get; set; }

        public BodyWeightEntry()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Weight = 0;
            Notes = string.Empty;
        }

        public override string ToString()
        {
            return $"{Date:yyyy.MM.dd} - {Weight} kg";
        }
    }
}
