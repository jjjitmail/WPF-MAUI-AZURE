namespace Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string Photo {  get; set; }
    }
}
