using System;

public class Employee
{
    public string Name { get; set; }

    public decimal Salary { get; set; }

    public Employee(string name, decimal salary)
    {
        Name = name;

        if (salary < 0)
        {
            Salary = 0;
        }
        else
        {
            Salary = salary;
        }
    }

    public static Employee operator +(Employee employee, decimal amount)
    {
        return new Employee(employee.Name, employee.Salary + amount);
    }

    public static Employee operator -(Employee employee, decimal amount)
    {
        return new Employee(employee.Name, employee.Salary - amount);
    }

    public static bool operator ==(Employee first, Employee second)
    {
        if (ReferenceEquals(first, second))
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Salary == second.Salary;
    }

    public static bool operator !=(Employee first, Employee second)
    {
        return !(first == second);
    }

    public static bool operator <(Employee first, Employee second)
    {
        return first.Salary < second.Salary;
    }

    public static bool operator >(Employee first, Employee second)
    {
        return first.Salary > second.Salary;
    }

    public static Employee operator ++(Employee employee)
    {
        employee.Salary = employee.Salary + 500;
        return employee;
    }

    public static Employee operator --(Employee employee)
    {
        if (employee.Salary >= 500)
        {
            employee.Salary = employee.Salary - 500;
        }
        else
        {
            employee.Salary = 0;
        }

        return employee;
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is Employee other))
        {
            return false;
        }

        return Salary == other.Salary;
    }

    public override int GetHashCode()
    {
        return Salary.GetHashCode();
    }

    public override string ToString()
    {
        return Name + ": " + Salary + " грн";
    }
}
