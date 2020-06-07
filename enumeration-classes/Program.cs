using System;

namespace enumeration_classes
{
    class Program
    {
        static void Main(string[] args)
        {
            var employee = new Employee("Vinicius", EmployeeTypeAbstract.Manager);

            System.Console.WriteLine($"{employee.Name} - Type: {employee.Type.DisplayName} - Bonus: {employee.Bonus}");

            employee.UpdateStatus(EmployeeTypeAbstract.Developer);

            System.Console.WriteLine($"{employee.Name} - Type: {employee.Type.DisplayName} - Bonus: {employee.Bonus}");
        }
    }
}
