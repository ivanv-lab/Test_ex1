namespace Test_ex.Models
{
    public class Doctor
    {
        public long Id { get; set; }
        public string Fullname {  get; set; }
        public long CabinetId {  get; set; }
        public long SpecializationId {  get; set; }
        public long RegionId {  get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual Cabinet? Cabinet { get; set; }
        public virtual Specialization? Specialization { get; set; }
        public virtual Region? Region { get; set; }
    }
}
