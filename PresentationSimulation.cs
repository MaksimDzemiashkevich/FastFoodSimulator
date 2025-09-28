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
    private CookUI _cookUI;
    private ServerUI _serverUI;
    private WaitingCustomersUI _waitingCustomersUI;
    private Button _stopButton;
    private System.Windows.Forms.Timer _timer;
    private bool _isRunning = true;

    private int _countSeconds = 0;
    private int _intervalTimeArriveCustomersLocal = 0;
    private int _timeForNextMakingOrder = 0;
    private int _timeForNextCooking = 0;
    private int _timeNextIssuingOrder = 0;

    private int _timeforMakingOrder = 2;
    private int _timeForIssuingOrder = 4;
    private int _rest = 1;

    private Customer customer;

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

    public CookUI CookUI
    {
        get { return _cookUI; }
        set
        {
            if (value != null)
            {
                _cookUI = value;
            }
        }
    }

    public ServerUI ServerUI
    {
        get { return _serverUI; }
        set
        {
            if (value != null)
            {
                _serverUI = value;
            }
        }
    }

    public void Main()
    {
        OrderTakerUI = new OrderTakerUI(CreaterPanel(new Size(300, 120), new Point(50, 130), _window));
        OrderTakerUI.LabelTitle = CreaterLabelTitle(new Size(300, 80), new Point(50, 50), "Order taker", _window);

        _customerListUI = new CustomerListUI(CreaterPanel(new Size(300, 620), new Point(50, 580), _window));
        _customerListUI.Label = CreaterLabelTitle(new Size(300, 80), new Point(50, 500), "Customers", _window);

        _waitingCustomersUI = new WaitingCustomersUI(CreaterPanel(new Size(300, 620), new Point(800, 580), _window));
        _waitingCustomersUI.Label = CreaterLabelTitle(new Size(300, 80), new Point(800, 500), "Waiting customers", _window);

        _cookUI = new CookUI(CreaterPanel(new Size(300, 120), new Point(400, 130), _window), CreaterPanel(new Size(300, 120), 
            new Point(400, 330), _window), CreaterLabelTitle(new Size(300, 80), new Point(400, 50), "Cook", _window), 
            CreaterLabelTitle(new Size(300, 80), new Point(400, 250), "Queue tickets", _window));

        _serverUI = new ServerUI(CreaterPanel(new Size(300, 120), new Point(800, 130), _window), CreaterPanel(new Size(300, 120),
            new Point(800, 330), _window), CreaterLabelTitle(new Size(300, 80), new Point(800, 50), "Server", _window),
            CreaterLabelTitle(new Size(300, 80), new Point(800, 250), "Ready tickets", _window));

        _stopButton = CreaterButton(new Size(200, 70), new Point(450, 700), "Stop", _window);
        _stopButton.Click += StopAndContinue;

        _timer = new System.Windows.Forms.Timer();
        _timer.Interval = 1000;
        _timer.Tick += TimerTick;
        _timer.Start();
    }

    private void TimerTick(object sender, EventArgs e)
    {
        _countSeconds++;

        //Arrive new customer
        if (_countSeconds >= _intervalTimeArriveCustomersLocal)
        {
            _customerListUI.AddNewCustomer();
            _intervalTimeArriveCustomersLocal += _intervalTimeArriveCustomers;
        }
        //make order move to cook queue, customer move from order taker to waiting queue and order taker area clear
        if (_timeForNextMakingOrder <= _countSeconds && _orderTakerUI.IsExistedLabel())
        {
            _cookUI.AddCustomerInQueue(_orderTakerUI.Customer);
            _waitingCustomersUI.AddNewCustomer(_orderTakerUI.Customer);
            _orderTakerUI.ClearPanel();
        }
        //Order taker make new order and first customer leave queue
        if (_customerListUI.CustomersList.Count > 0 && _countSeconds >= _timeForNextMakingOrder + _rest)
        {
            _orderTakerUI.MakingOrder(_customerListUI.CustomersList.First());
            _customerListUI.RemoveCustomer(_customerListUI.CustomersList.First());
            _timeForNextMakingOrder = _countSeconds + _timeforMakingOrder;
        }
        //
        if (_timeForNextCooking <= _countSeconds && _cookUI.IsExistedLabel())
        {
            _serverUI.AddReadyTicket(_cookUI.Customer);
            _cookUI.ClearPanel();
        }
        //Take ticket from queue and cook
        if (_timeForNextCooking + _rest <= _countSeconds && _cookUI.IsTicketsInOrder())
        {
            _cookUI.Cooking();
            _timeForNextCooking = _countSeconds + _intervalTimeCook;
        }
        //Server waiting customers
        if (_timeNextIssuingOrder < _countSeconds && _serverUI.IsAnyReadyTicket() && _waitingCustomersUI.IsAnyWaitingCustomer())
        {
            _serverUI.ClearPanel();
            ServerWaitingCustomer();
            _timeNextIssuingOrder = _countSeconds + _timeForIssuingOrder;
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

    private void ServerWaitingCustomer()
    {
        Customer customer = _waitingCustomersUI.FindCustomer(_serverUI.GetFirstCustomer().Id);
        if (customer.Name != "")
        {
            _serverUI.IssuingOrder(customer);
            _waitingCustomersUI.RemoveWaitingCustomer(customer);
        }
    }

    private Button CreaterButton(Size size, Point point, string text, Control control)
    {
        Button button = new Button();
        button.Size = size;
        button.Text = text;
        button.Location = point;
        control.Controls.Add(button);
        return button;
    }

    private void StopAndContinue(object sender, EventArgs e)
    {
        if (_isRunning)
        {
            _timer.Stop();
            _stopButton.Text = "Continue";
            _isRunning = !_isRunning;
        }
        else
        {
            _timer.Start();
            _stopButton.Text = "Stop";
            _isRunning = !_isRunning;
        }
    }
}
