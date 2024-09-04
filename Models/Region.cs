namespace Test_ex.Models
{
    public class Region
    {
        public long Id { get; set; }
        public string Number {  get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
    }
}
