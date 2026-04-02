using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUZ43X_GUI.Models
{

    namespace TrainingLog.Models
    {
        public class Exercise
        {

            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }
            public Exercise()
            {
                Id = Guid.NewGuid();
                Name = string.Empty;
                Category = string.Empty;
                Description = string.Empty;
                IsActive = true;
            }
            public override string ToString()
            {
                return $"{Name} ({Category})";
            }
        }
    }
}
