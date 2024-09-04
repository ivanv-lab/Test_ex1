namespace Test_ex.Models
{
    public class Specialization
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<Doctor> Doctors { get; set; }
    }
}
