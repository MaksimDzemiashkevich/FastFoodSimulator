using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;
public class OrderTaker
{
    protected List<Customer> _customer = new List<Customer>();

    public List<Customer> Customer
    {
        get { return _customer; }
        set
        {
            if (value != null)
            {
                _customer = value;
            }
        }
    }

    public OrderTaker(List<Customer> customer)
    {
        Customer = customer;
    }

    public OrderTaker() { }
}