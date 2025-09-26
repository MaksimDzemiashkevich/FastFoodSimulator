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

    private Panel _customersPanel;
    private OrderTakerUI _orderTakerUI;
    private Panel _cooksPanel;
    private Panel _serversPanel;
    private Panel _waitingCustomersPanel;

    private List<Customer> _customersList = new List<Customer>();
    private Random _random = new Random();
    private string[] _names = Enum.GetNames(typeof(NamesCustomers));

    private List<Label> _customersLabels = new List<Label>();

    public PresentationSimulation(int countCooks, int countServers, int countOrderTakers, int intervalTimeArriveCustomers, int intervalTimeCook, Control window)
    {
        CountCooks = countCooks;
        CountServers = countServers;
        CountOrderTakers = countOrderTakers;
        IntervalTimeArriveCustomers = intervalTimeArriveCustomers;
        IntervalTimeCook = intervalTimeCook;
        _window = window;
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
        OrderTakerUI = new OrderTakerUI(CreaterPanel(new Size(300, 200), new Point(50, 50), _window));
        OrderTakerUI.Label = CreaterLabelTitle(new Size(300, 80), new Point(0, 0), "Order taker", OrderTakerUI.Panel);

        CustomersPanel = CreaterPanel(new Size(300, 700), new Point(50, 400), _window);

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        timer.Interval = IntervalTimeArriveCustomers * 1000;
        timer.Tick += TimerTick;
        timer.Start();

    }

    private void TimerTick(object sender, EventArgs e)
    {
        string randomName = _names[_random.Next(_names.Length)];
        Customer customer = new Customer(randomName);
        _customersList.Add(customer);

        Label label = CreaterLabel(new Size(CustomersPanel.Width - 10, 70), new Point(5, 5 + (_customersList.Count -1) * 75), customer.Name, CustomersPanel);
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

    private void UpdateUIElement()
    {

    }
}
