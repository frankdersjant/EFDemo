using System.Collections.Generic;

namespace Domain
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmployeeRole> employeeRoles { get; set; }
    }
}
