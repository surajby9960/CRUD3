namespace CRUD3.Model
{
    public class Company
    {
        public int cId { get; set; }
        public string? cName { get; set; }
        public string? cAddress { get; set; }
        public string? cCountry { get; set; }
        public List<Employee> empList { get; set; }
    }
}
