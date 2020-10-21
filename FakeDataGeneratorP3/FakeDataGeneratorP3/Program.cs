using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace FakeDataGeneratorP3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Settings:
            int amountOfFiles = 5;
            int numberOfEmployees = 50;
            //-----------------
            GenerateDays(amountOfFiles, numberOfEmployees);
        }

        static DataTable MakeNewAAFTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Timestamp", typeof(DateTime));
            table.Columns.Add("PcNavn", typeof(string));
            table.Columns.Add("Primaerbruger", typeof(string));
            table.Columns.Add("Navn", typeof(string));
            table.Columns.Add("Afdeling", typeof(string));
            table.Columns.Add("Operativsystem", typeof(string));
            table.Columns.Add("CPU", typeof(int));
            table.Columns.Add("RAM", typeof(string));
            table.Columns.Add("HDD", typeof(string));
            table.Columns.Add("Service Pack", typeof(string));
            table.Columns.Add("Serienummer", typeof(string));
            table.Columns.Add("Fakturadato", typeof(DateTime));
            table.Columns.Add("Gateway", typeof(string));
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("Pctype", typeof(string));

            return table;
        }

        static List<Employee> MakeEmployeeList(int count)
        {
            List<Employee> employeeList = new List<Employee>();
            for (int i = 0; i < count; i++)
            {
                employeeList.Add(new Employee());
            }

            return employeeList;
        }

        static void InsertFakeData(DataTable dt, List<Employee> employees, int daysSinceDay0)
        {
            foreach (Employee employee in employees)
            {
                InsertFakeDataRow(dt, employee, daysSinceDay0);
            }
        }

        static void InsertFakeDataRow(DataTable dt, Employee employee, int daysSinceDay0)
        {
            dt.Rows.Add(DateTime.Now.AddDays(daysSinceDay0),
                employee.Computer.PCNavn,
                employee.Username,
                employee.Navn,
                employee.Afdeling,
                employee.Computer.OS,
                employee.Computer.CPU,
                employee.Computer.RAM,
                employee.Computer.HDD,
                employee.Computer.ServicePack,
                employee.Computer.Serienummer,
                employee.Computer.FakturaDato,
                employee.Computer.Gateway,
                employee.Computer.Model,
                employee.Computer.PCType);
        }

        static void ExportToCSV(DataTable dt, string outputName)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                  Select(column => column.ColumnName);

            sb.AppendLine(string.Join(";", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(";", fields));
            }

            File.WriteAllText($"{outputName}.csv", sb.ToString());
        }

        static void GenerateDays(int dayCount, int numberOfEmployees)
        {
            List<Employee> employees = MakeEmployeeList(numberOfEmployees);

            for (int i = 0; i < dayCount; i++)
            {
                //Make table
                DataTable AAFTable = MakeNewAAFTable();
                MakeChangesToEmployees(employees);
                InsertFakeData(AAFTable, employees, i);

                ExportToCSV(AAFTable, $"dataDay{i}");
            }
        }

        static void MakeChangesToEmployees(List<Employee> employees)
        {
            Random rnd = new Random();

            //Nogle employess forsvinder og deres computer forsvinder med dem
            for (int i = 0; i < 5; i++)
            {
                employees.RemoveAt(rnd.Next(0, employees.Count));
            }

            //Nogle employess forsvinder og deres computer bliver givet til en anden employee
            for (int i = 0; i < 5; i++)
            {
                int rndEmployeeIndex = rnd.Next(0, employees.Count);
                Computer transferComputer = employees[rndEmployeeIndex].Computer;
                employees.RemoveAt(rndEmployeeIndex);
                employees[rnd.Next(0, employees.Count)].Computer = transferComputer;
            }

            //Nogle employees får en helt ny computer
            for (int i = 0; i < 5; i++)
            {
                employees[rnd.Next(0, employees.Count)].Computer = new Computer();
            }

            //Nogle nye employees dukker op
            for (int i = 0; i < 10; i++)
            {
                employees.Add(new Employee());
            }
        }
    }
}
