using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;
public class Customer
{
    private int _id;
    private string _name;
    private DateTime _time;
    private static int _counterCustomers = 1;

    public int Id
    {
        get { return _id; }
        set
        {
            if (value != null)
            {
                _id = value;
            }
        }
    }

    public string Name
    {
        get { return _name; }
        set
        {
            if (value != null)
            {
                _name = value;
            }
        }
    }

    public DateTime Time
    {
        get { return _time; }
        set
        {
            if (value != null)
            {
                _time = value;
            }
        }
    }

    public Customer()
    {
        Id = _counterCustomers;
        Name = "";
        Time = DateTime.Now;
        _counterCustomers++;
    }

    public Customer(string name)
    {
        Id = _counterCustomers;
        Name = name;
        Time = DateTime.Now;
        _counterCustomers++;
    }

    public Customer(int id, string name)
    {
        Id = id;
        Name = name;
        Time = DateTime.Now;
        _counterCustomers++;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"Id : {Id}");
        stringBuilder.AppendLine($"Name customer: {Name}");
        stringBuilder.AppendLine($"Time order: {Time.ToString("HH:mm:ss")}");
        return stringBuilder.ToString();
    }
}

public enum NamesCustomers
{
    Maksim,
    Daniil,
    Matvei,
    Danila,
    Lesha,
    Ylia
}
