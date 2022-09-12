namespace CRUD3.Model
{
    public class Employee
    {
        public int eId { get; set; }
        public string? eName { get; set; }
        public string? eAddress { get; set; }
        public double Salary { get; set; }
        public int cId { get; set; }
        public List<Project> plist { get; set; }
    }
}
////eId,eName,eAddress,Salary,cId