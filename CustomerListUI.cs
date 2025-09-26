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

    private Random _random = new Random();
    private string[] _names = Enum.GetNames(typeof(NamesCustomers));

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

    public List<Customer> CustomersList
    {
        get { return _customersList; }
        set
        {
            if (value != null)
            {
                _customersList = value;
            }
        }
    }

    public CustomerListUI(Panel panel)
    {
        _customersPanel = panel;
    }

    public void AddNewCustomer()
    {
        string randomName = _names[_random.Next(_names.Length)];
        Customer customer = new Customer(randomName);
        _customersList.Add(customer);

        Label label = CreaterLabel(new Size(CustomersPanel.Width - 10, 70),
            new Point(5, 5 + (CustomersList.Count - 1) * 75 + CustomersPanel.AutoScrollPosition.Y), customer.Name, CustomersPanel);
        _customersLabels.Add(label);
    }

    private Label CreaterLabel(Size size, Point point, string text, Control control)
    {
        Label label = new Label();
        label.Text = text;
        label.Location = point;
        label.Size = size;
        label.Font = new Font("Times New Roman", 20, FontStyle.Regular);
        label.TextAlign = ContentAlignment.MiddleCenter;
        control.Controls.Add(label);
        return label;
    }

    public void RemoveCustomer(Customer customer)
    {
        int index = _customersList.IndexOf(customer);
        _customersList.Remove(customer);

        Label label = _customersLabels[index];
        _customersPanel.Controls.Remove(label);
        _customersLabels.Remove(label);
        label.Dispose();

        RefreshLocation();
    }

    private void RefreshLocation()
    {
        foreach (Label label in _customersLabels)
        {
            label.Location = new Point(label.Location.X, label.Location.Y - label.Height);
        }
    }
}
