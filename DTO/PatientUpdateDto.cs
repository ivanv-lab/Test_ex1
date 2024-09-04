namespace Test_ex.DTO
{
    public class PatientUpdateDto
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool Gender { get; set; }
        public long RegionId { get; set; }
    }
}
