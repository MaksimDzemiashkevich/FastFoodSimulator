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
    private List<Label> _labelCook = new List<Label>();
    private Label _labelQueue;
    private List<Label> _labelsTickets = new List<Label>();
    private List<Customer> _listCustomers = new List<Customer>();
    private List<int> _timeNextIssuingOrder = new List<int>();

    public List<Label> LabelCook
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

    public List<int> TimeNextIssuingOrder
    {
        get { return _timeNextIssuingOrder; }
        set
        {
            if (value != null)
            {
                _timeNextIssuingOrder = value;
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
        Customer.Add(_listCustomers.First());
        _labelCook[_labelCook.Count - 1] = CreaterLabel(new Size(_panel.Size.Width - 30, 70), new Point(0, (_labelCook.Count - 1) * 70 + _panel.AutoScrollPosition.Y), Customer.Last().ToString(), _panel);

        int index = _listCustomers.IndexOf(_listCustomers.First());
        _listCustomers.RemoveAt(index);

        Label label = _labelsTickets[index];
        _panelQueue.Controls.Remove(label);
        _labelsTickets.Remove(label);
        label.Dispose();

        RefreshLocation(_labelsTickets, index);
    }

    public void RefreshLocation(List<Label> list, int dot)
    {
        for (int i = dot; i < list.Count; i++)
        {
            list[i].Location = new Point(list[i].Location.X, list[i].Location.Y - list[i].Height);
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

    public void ClearPanel(int i)
    {
        if (_labelCook[i] != null)
        {
            _panel.Controls.Remove(_labelCook[i]);
            _labelCook[i].Dispose();
            _labelCook[i] = null;
            _labelCook.RemoveAt(i);
            _timeNextIssuingOrder.RemoveAt(i);
            Customer.RemoveAt(i);

            RefreshLocation(_labelCook, i);
        }
        else
        {
            return;
        }
    }

    public bool IsExistedLabels()
    {
        if (_labelCook == null)
        {
            return false;
        }
        else if (_labelCook.Count < 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsExistedLabel(int i)
    {
        return (_labelCook.Count > i) ? true : false;
    }

    public bool IsTicketsInOrder()
    {
        return (_labelsTickets.Count > 0) ? true : false;
    }

    public bool CheckTimer(int i, int _countSeconds)
    {
        if (i >= _timeNextIssuingOrder.Count)
        {
            return false;
        }
        else if (TimeNextIssuingOrder[i] <= _countSeconds)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckTime(int i, int _countSeconds)
    {
        if (i >= _timeNextIssuingOrder.Count)
        {
            return true;
        }
        else if (TimeNextIssuingOrder[i] <= _countSeconds)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}