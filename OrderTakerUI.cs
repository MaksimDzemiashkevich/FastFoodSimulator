using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;

public class OrderTakerUI : OrderTaker
{
    private Panel _panel;
    private Label _labelTitle;
    private List<Label> _label = new List<Label>();
    private List<int> _timeNextIssuingOrder = new List<int>();

    public Panel Panel
    {
        get { return _panel; }
        set
        {
            if (value != null)
            {
                _panel = value;
            }
        }
    }

    public Label LabelTitle
    {
        get { return _labelTitle; }
        set
        {
            if (value != null)
            {
                _labelTitle = value;
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

    public OrderTakerUI() : base()
    {
        
    }

    public OrderTakerUI(Panel panel) : base()
    {
        _panel = panel;
    }

    public void MakingOrder(Customer customer)
    {
        Customer.Add(customer);
        _label.Add(CreaterLabel(new Size(_panel.Size.Width - 30, 70), new Point(0, (_label.Count) * 70 + _panel.AutoScrollPosition.Y), customer.ToString(), _panel));
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
        if (_label[i] != null)
        {
            _panel.Controls.Remove(_label[i]);
            _label[i].Dispose();
            _label[i] = null;
            _label.RemoveAt(i);
            _timeNextIssuingOrder.RemoveAt(i);
            Customer.RemoveAt(i);

            RefreshLocation(_label, i);
        }
        else
        {
            return;
        }
    }

    public void RefreshLocation(List<Label> list, int dot)
    {
        for (int i = dot; i < list.Count; i++)
        {
            list[i].Location = new Point(list[i].Location.X, list[i].Location.Y - list[i].Height);
        }
    }

    public bool IsExistedLabel()
    {
        if (_label == null)
        {
            return false;
        }
        else
        {
            return true;
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
