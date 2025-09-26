using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;

public class CustomerListUI
{
    private Panel _customersPanel;
    private Label _label;
    private List<Customer> _customersList = new List<Customer>();
    private List<Label> _customersLabels = new List<Label>();

    public Panel CustomersPanel
    {
        get { return _customersPanel; }
        set
        {
            if (value != null)
            {
                _customersPanel = value;
            }
        }
    }

    public Label Label
    {
        get { return _label; }
        set
        {
            if (value != null)
            {
                _label = value;
            }
        }
    }


}
