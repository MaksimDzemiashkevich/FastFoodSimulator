using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;

public class ServerUI
{
    private Panel _panel;
    private Panel _panelReadyTickets;
    private List<Customer> _readyTicketsList = new List<Customer>();
    private List<Label> _readyTicketsLabelList = new List<Label>();
    private Label _labelTitle;
    private Label _labelTitleReadyTickets;
    private List<Label> _label = new List<Label>();
    private List<int> _timeNextIssuingOrder = new List<int>();

    public List<Label> Label
    {
        get { return _label; }
        set
        {
            if (value != null)
            {
                Label = value;
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

    public ServerUI(Panel panel, Panel panelReadyTickets, Label labelTitle, Label labelTitleReadyTickets)
    {
        _panel = panel;
        _panelReadyTickets = panelReadyTickets;
        _labelTitle = labelTitle;
        _labelTitleReadyTickets = labelTitleReadyTickets;
    }

    public void IssuingOrder(Customer customer, int i)
    {
        _label[_label.Count - 1] = CreaterLabel(new Size(_panel.Width - 30, 70), new Point(0, (_label.Count - 1) * 70 + _panel.AutoScrollPosition.Y), customer.ToString(), _panel);
        RemoveTicket(customer);
        RefreshLocation(_readyTicketsLabelList);
    }

    public void RefreshLocation(List<Label> list)
    {
        foreach (Label label in list)
        {
            label.Location = new Point(label.Location.X, label.Location.Y - label.Height);
        }
    }

    public void RefreshLocation(List<Label> list, int dot)
    {
        for (int i = dot; i< list.Count; i++)
        {
            list[i].Location = new Point(list[i].Location.X, list[i].Location.Y - list[i].Height);
        }
    }

    public void AddReadyTicket(Customer customer)
    {
        _readyTicketsList.Add(customer);
        Label label = CreaterLabel(new Size(_panel.Width - 30, 70), new Point(0, (_readyTicketsList.Count - 1) * 70 + _panelReadyTickets.AutoScrollPosition.Y), customer.ToString(), _panelReadyTickets);
        _readyTicketsLabelList.Add(label);
    }

    public void RemoveTicket(Customer customer)
    {
        int index = _readyTicketsList.IndexOf(customer);
        _readyTicketsList.Remove(customer);

        Label label = _readyTicketsLabelList[index];
        _panelReadyTickets.Controls.Remove(label);
        _readyTicketsLabelList.Remove(label);
        label.Dispose();
    }

    public bool IsAnyReadyTicket()
    {
        return (_readyTicketsList.Count > 0) ? true : false;
    }

    public Customer GetFirstCustomer()
    {
        return _readyTicketsList.First();
    }

    public void ClearPanel(int i)
    {
        if (_label[i] != null)
        {
            _panel.Controls.Remove(_label[i]);
            _label[i].Dispose();
            _label[i] = null;
            _label.RemoveAt(i);
            _timeNextIssuingOrder.RemoveAt(i);

            RefreshLocation(_label, i);
        }
        else
        {
            return;
        }
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
}
