using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Cook
    {
        private List<Customer> _customer = new List<Customer>();

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

        public Cook()
        {
            
        }
    }
}
