using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;
public class PresentationSimulation
{
    private Control _window;

    private int _countCooks;
    private int _countServers;
    private int _countOrderTakers;
    private int _intervalTimeArriveCustomers;
    private int _intervalTimeCook;

    private CustomerListUI _customerListUI;
    private OrderTakerUI _orderTakerUI;
    private Panel _cooksPanel;
    private Panel _serversPanel;
    private Panel _waitingCustomersPanel;

    private int _countSeconds = 0;
    private int _intervalTimeArriveCustomersLocal = 0;
    private int _timeForNextMakingOrder = 0;

    private int _timeforMakingOrder = 4;
    private int _rest = 1;

    int ff = 0;

    public PresentationSimulation(int countCooks, int countServers, int countOrderTakers, int intervalTimeArriveCustomers, int intervalTimeCook, Control window)
    {
        CountCooks = countCooks;
        CountServers = countServers;
        CountOrderTakers = countOrderTakers;
        IntervalTimeArriveCustomers = intervalTimeArriveCustomers;
        IntervalTimeCook = intervalTimeCook;
        _window = window;

        _intervalTimeArriveCustomersLocal = intervalTimeArriveCustomers;
    }

    public int CountCooks
    {
        get { return _countCooks; }
        set
        {
            if (value != null)
            {
                _countCooks = value;
            }
        }
    }

    public int CountServers
    {
        get { return _countServers; }
        set
        {
            if (value != null)
            {
                _countServers = value;
            }
        }
    }

    public int CountOrderTakers
    {
        get { return _countOrderTakers; }
        set
        {
            if (value != null)
            {
                _countOrderTakers = value;
            }
        }
    }

    public int IntervalTimeArriveCustomers
    {
        get { return _intervalTimeArriveCustomers; }
        set
        {
            if (value != null)
            {
                _intervalTimeArriveCustomers = value;
            }
        }
    }

    public int IntervalTimeCook
    {
        get { return _intervalTimeCook; }
        set
        {
            if (value != null)
            {
                _intervalTimeCook = value;
            }
        }
    }

    public OrderTakerUI OrderTakerUI
    {
        get { return _orderTakerUI; }
        set
        {
            if (value != null)
            {
                _orderTakerUI = value;
            }
        }
    }

    public Panel CooksPanel
    {
        get { return _cooksPanel; }
        set
        {
            if (value != null)
            {
                _cooksPanel = value;
            }
        }
    }

    public Panel ServersPanel
    {
        get { return _serversPanel; }
        set
        {
            if (value != null)
            {
                _serversPanel = value;
            }
        }
    }

    public Panel WaitingCustomersPanel
    {
        get { return _waitingCustomersPanel; }
        set
        {
            if (value != null)
            {
                _waitingCustomersPanel = value;
            }
        }
    }

    public void Main()
    {
        OrderTakerUI = new OrderTakerUI(CreaterPanel(new Size(300, 120), new Point(50, 130), _window));
        OrderTakerUI.LabelTitle = CreaterLabelTitle(new Size(300, 80), new Point(50, 50), "Order taker", _window);

        _customerListUI = new CustomerListUI(CreaterPanel(new Size(300, 620), new Point(50, 480), _window));
        _customerListUI.Label = CreaterLabelTitle(new Size(300, 80), new Point(50, 400), "Customers", _window);

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        timer.Interval = 1000;
        timer.Tick += TimerTick;
        timer.Start();
    }

    private void TimerTick(object sender, EventArgs e)
    {
        _countSeconds++;
        if (_timeForNextMakingOrder <= _countSeconds && _orderTakerUI.IsExistedLabel())
        {
            _orderTakerUI.ClearPanel();
        }

        if (_countSeconds >= _intervalTimeArriveCustomersLocal)
        {
            _customerListUI.AddNewCustomer();
            _intervalTimeArriveCustomersLocal += _intervalTimeArriveCustomers;
            ff++;
            if (ff == 2)
            {
                ff++;
            }
        }

        if (_customerListUI.CustomersList.Count > 0 && _countSeconds >= _timeForNextMakingOrder + _rest)
        {
            _orderTakerUI.MakingOrder(_customerListUI.CustomersList.First());
            _customerListUI.RemoveCustomer(_customerListUI.CustomersList.First());
            _timeForNextMakingOrder = _countSeconds + _timeforMakingOrder;
        }
    }

    private Label CreaterLabel(Size size, Point point, string text, Control control)
    {
        Label label = new Label();
        label.Text = text;
        label.Location = point;
        label.Size = size;
        label.Font = new Font("Times New Roman", 20, FontStyle.Bold);
        control.Controls.Add(label);
        return label;
    }

    private Label CreaterLabelTitle(Size size, Point point, string text, Control control)
    {
        Label label = new Label();
        label.Text = text;
        label.Location = point;
        label.Size = size;
        label.Font = new Font("Times New Roman", 20, FontStyle.Bold);
        label.TextAlign = ContentAlignment.MiddleCenter;
        label.BackColor = Color.Aqua;
        control.Controls.Add(label);
        return label;
    }

    private Panel CreaterPanel(Size size, Point point, Control control)
    {
        Panel panel = new Panel();
        panel.Size = size;
        panel.Location = point;
        panel.AutoScroll = true;
        panel.BackColor = Color.Yellow;
        control.Controls.Add(panel);
        return panel;
    }
}
