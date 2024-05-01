namespace Task2.DTO
{
    public class DepartmentDTO
    {
        public int Dept_Id { get; set; }
        public string Dept_Name { get; set; }
        public string Dept_Location { get; set; }

        public List<string> Students { get; set; }
    }
}
