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
    private Label _label;

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

    public OrderTakerUI() : base()
    {
        
    }

    public OrderTakerUI(Panel panel) : base()
    {
        _panel = panel;
    }

    public void MakingOrder(Customer customer)
    {
        Customer = customer;
        _label = CreaterLabel(new Size(_panel.Size.Width, _panel.Size.Height), new Point(0, 0), customer.ToString(), _panel);
    }

    private Label CreaterLabel(Size size, Point point, string text, Control control)
    {
        Label label = new Label();
        label.Text = text;
        label.Location = point;
        label.Size = size;
        label.TextAlign = ContentAlignment.MiddleLeft;
        control.Controls.Add(label);
        return label;
    }

    public void ClearPanel()
    {
        _panel.Controls.Remove(_label);
        _label.Dispose();
        _label = null;
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
}
