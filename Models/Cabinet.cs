namespace Test_ex.Models
{
    public class Cabinet
    {
        public long Id { get; set; }
        public int Number {  get; set; }
        public bool IsDeleted { get; set; }=false;

        public ICollection<Doctor>? Doctors { get; set; }
    }
}
