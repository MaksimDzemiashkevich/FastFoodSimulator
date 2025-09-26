using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;

public class OrderTakerUI
{
    private OrderTaker _orderTaker;
    private Panel _panel;
    private Label _label;

    public OrderTaker OrderTaker
    {
        get { return _orderTaker; }
        set
        {
            if (value != null)
            {
                _orderTaker = value;
            }
        }
    }

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

    public OrderTakerUI(OrderTaker orderTaker)
    {
        _orderTaker = orderTaker;
    }

    public OrderTakerUI()
    {
        
    }

    public OrderTakerUI(Panel panel)
    {
        _panel = panel;
    }
}
