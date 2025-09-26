using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;
public class OrderTaker
{
    protected Customer _customer;

    public Customer Customer
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

    public OrderTaker(Customer customer)
    {
        Customer = customer;
    }

    public OrderTaker() { }
}