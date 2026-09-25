

namespace EmployeeManagementSystem
{
    public class Employee
    {
        private int id;
        private string name;
        private double salary;

        public int Id
        {
            get { return id; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Invalid ID");
                    return;
                }
                id = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Name cannot be empty");
                    return;
                }
                name = value;
            }
        }

        public double Salary
        {
            get { return salary; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Salary cannot be negative");
                    return;
                }
                salary = value;
            }
        }

        public string Department { get; set; }

        public Employee()
        {
        }

        public Employee(int id, string name, double salary, string department)
        {
            this.Id = id;
            this.Name = name;
            this.Salary = salary;
            this.Department = department;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine("ID: " + Id + " | Name: " + Name + " | Dept: " + Department + " | Salary: " + Salary);
        }
    }

    public class Manager : Employee
    {
        public double Bonus { get; set; }

        public Manager(int id, string name, double salary, string department, double bonus)
            : base(id, name, salary, department)
        {
            Bonus = bonus;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Bonus: " + Bonus + " | Total Salary: " + (Salary + Bonus));
        }
    }

    class Program
    {
        static List<Employee> employees = new List<Employee>();
        static string filePath = "data.txt";

        static void Main(string[] args)
        {
            LoadFromFile();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Show All Employees");
                Console.WriteLine("3. Search Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Save and Exit");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    AddEmployee();
                }
                else if (choice == "2")
                {
                    ShowEmployees();
                }
                else if (choice == "3")
                {
                    SearchEmployee();
                }
                else if (choice == "4")
                {
                    DeleteEmployee();
                }
                else if (choice == "5")
                {
                    SaveToFile();
                    running = false;
                    Console.WriteLine("Saved. Goodbye!");
                }
                else
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
            }
        }

        static void AddEmployee()
        {
            try
            {
                Console.Write("Enter ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Salary: ");
                double salary = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter Department: ");
                string dept = Console.ReadLine();

                Employee emp = new Employee(id, name, salary, dept);
                employees.Add(emp);

                Console.WriteLine("Employee added successfully!");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Please enter valid numbers for ID and Salary.");
            }
        }

        static void ShowEmployees()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees to show.");
                return;
            }

            for (int i = 0; i < employees.Count; i++)
            {
                employees[i].DisplayInfo();
            }
        }

        static void SearchEmployee()
        {
            try
            {
                Console.Write("Enter ID to search: ");
                int id = Convert.ToInt32(Console.ReadLine());

                bool found = false;
                foreach (var emp in employees)
                {
                    if (emp.Id == id)
                    {
                        emp.DisplayInfo();
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Employee not found.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        static void DeleteEmployee()
        {
            try
            {
                Console.Write("Enter ID to delete: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Employee target = null;
                foreach (var emp in employees)
                {
                    if (emp.Id == id)
                    {
                        target = emp;
                        break;
                    }
                }

                if (target != null)
                {
                    employees.Remove(target);
                    Console.WriteLine("Deleted successfully.");
                }
                else
                {
                    Console.WriteLine("ID not found.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        static void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var emp in employees)
                {
                    writer.WriteLine(emp.Id + "," + emp.Name + "," + emp.Salary + "," + emp.Department);
                }
            }
        }

        static void LoadFromFile()
        {
            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 4)
                {
                    try
                    {
                        int id = Convert.ToInt32(parts[0]);
                        string name = parts[1];
                        double salary = Convert.ToDouble(parts[2]);
                        string dept = parts[3];

                        Employee emp = new Employee(id, name, salary, dept);
                        employees.Add(emp);
                    }
                    catch (Exception)
                    {
                        // Skip bad lines
                    }
                }
            }
        }
    }
}
