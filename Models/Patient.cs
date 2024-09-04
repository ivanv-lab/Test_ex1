namespace Test_ex.Models
{
    public class Patient
    {
        public long Id { get; set; }
        public string Surname {  get; set; }
        public string Name { get; set; }
        public string Lastname {  get; set; }
        public string Address {  get; set; }
        public DateOnly BirthDate {  get; set; }
        public bool Gender { get; set; }
        public long RegionId {  get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual Region? Region { get; set; }
    }
}
