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
    private Label _label;

    public ServerUI(Panel panel, Panel panelReadyTickets, Label labelTitle, Label labelTitleReadyTickets)
    {
        _panel = panel;
        _panelReadyTickets = panelReadyTickets;
        _labelTitle = labelTitle;
        _labelTitleReadyTickets = labelTitleReadyTickets;
    }

    public void IssuingOrder(Customer customer)
    {
        _label = CreaterLabel(new Size(_panel.Width - 30, 70), new Point(0, 0), customer.ToString(), _panel);
        RemoveTicket(customer);
        RefreshLocation();
    }

    public void RefreshLocation()
    {
        foreach (Label label in _readyTicketsLabelList)
        {
            label.Location = new Point(label.Location.X, label.Location.Y - label.Height);
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

    public void ClearPanel()
    {
        if (_label != null)
        {
            _panel.Controls.Remove(_label);
            _label.Dispose();
            _label = null;
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
}
