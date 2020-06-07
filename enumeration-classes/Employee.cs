namespace enumeration_classes
{
    public class Employee
    {
        public string Name { get; set; }
        public EmployeeTypeAbstract Type { get; set; }
        public decimal Bonus { get { return Type.BonusSize;}}

        public Employee(string name, EmployeeTypeAbstract type){
            this.Name = name;
            this.Type = type;
        }

        public void UpdateStatus(EmployeeTypeAbstract type){
            this.Type = type;
        }
    }
}