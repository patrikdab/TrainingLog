namespace NUZ43X_GUI.Models
{
    public class SetEntry
    {
        
        public Guid Id { get; set; }
        public int SetNumber { get; set; }
        public double Weight { get; set; }
        public int Repetitions { get; set; }       
        public double? Rpe { get; set; }
        
        public SetEntry()
        {
            Id = Guid.NewGuid();
            SetNumber = 1;
            Weight = 0;
            Repetitions = 0;
            Rpe = null;
            
        }
        public override string ToString()
        {
            return $"{SetNumber}. sorozat: {Weight} kg x {Repetitions}";
        }
    }
}
