using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;

public class CookUI : Cook
{
    private Panel _panel;
    private Panel _panelQueue;
    private Label _labelTitle;
    private Label _labelCook;
    private Label _labelQueue;
    private List<Label> _labelsTickets = new List<Label>();
    private List<Customer> _listCustomers = new List<Customer>();
 
    public Label LabelCook
    {
        get { return _labelCook; }
        set
        {
            if (value != null)
            {
                _labelCook = value;
            }
        }
    }

    public CookUI(Panel panel, Panel panelQueue, Label labelTitle, Label labelQueue) : base()
    {
        _panel = panel;
        _panelQueue = panelQueue;
        _labelTitle = labelTitle;
        _labelQueue = labelQueue;
    }

    public void Cooking()
    {
        Customer = _listCustomers.First();
        _labelCook = CreaterLabel(new Size(_panel.Size.Width, _panel.Size.Height), new Point(0, 0), Customer.ToString(), _panel);

        int index = _listCustomers.IndexOf(Customer);
        _listCustomers.RemoveAt(index);

        Label label = _labelsTickets[index];
        _panelQueue.Controls.Remove(label);
        _labelsTickets.Remove(label);
        label.Dispose();

        RefreshLocation();
    }

    private void RefreshLocation()
    {
        foreach (Label label in _labelsTickets)
        {
            label.Location = new Point(label.Location.X, label.Location.Y - label.Height);
        }
    }

    public void AddCustomerInQueue(Customer customer)
    {
        _listCustomers.Add(customer);
        AddCustomerLabelInQueue(customer);
    }

    private void AddCustomerLabelInQueue(Customer customer)
    {
        Label label = CreaterLabel(new Size(_panelQueue.Width - 30, 70), new Point(0, (_listCustomers.Count - 1) * 70 + _panelQueue.AutoScrollPosition.Y), customer.ToString(), _panelQueue);
        _labelsTickets.Add(label);
    }

    private Label CreaterLabel(Size size, Point point, string text, Control control)
    {
        Label label = new Label();
        label.Text = text;
        label.Location = point;
        label.Size = size;
        control.Controls.Add(label);
        return label;
    }

    public void ClearPanel()
    {
        _panel.Controls.Remove(_labelCook);
        _labelCook.Dispose();
        _labelCook = null;
    }

    public bool IsExistedLabel()
    {
        if (_labelCook == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsTicketsInOrder()
    {
        return (_labelsTickets.Count > 0) ? true : false;
    }
}
