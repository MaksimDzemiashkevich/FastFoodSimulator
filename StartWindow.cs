using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1;
public class StartWindow
{
    private Control _window;

    private Panel _panel;
    private TextBox _countCooksTextBox;
    private TextBox _countServersTextBox;
    private TextBox _countOrderTakersTextBox;
    private TextBox _intervalTimeArriveCustomersTextBox;
    private TextBox _intervalTimeCookTextBox;
    private Button _startButton;

    public StartWindow(Control window)
    {
        _window = window;
    }

    public Button StartButton
    {
        get { return _startButton; }
        set
        {
            if (value != null)
            {
                _startButton = value;
            }
        }
    }

    public Panel Panel
    {
        get {  return _panel; }
        set
        {
            if (value != null)
            {
                _panel = value;
            }
        }
    }

    public TextBox CountCooksTextBox
    {
        get { return _countCooksTextBox; }
        set
        {
            if (value != null)
            {
                _countCooksTextBox = value;
            }
        }
    }

    public TextBox CountServersTextBox
    {
        get { return _countServersTextBox; }
        set
        {
            if (value != null)
            {
                _countServersTextBox = value;
            }
        }
    }

    public TextBox CountOrderTakersTextBox
    {
        get { return _countOrderTakersTextBox; }
        set
        {
            if (value != null)
            {
                _countOrderTakersTextBox = value;
            }
        }
    }

    public TextBox IntervalTimeArriveCustomersTextBox
    {
        get { return _intervalTimeArriveCustomersTextBox; }
        set
        {
            if (value != null)
            {
                _intervalTimeArriveCustomersTextBox = value;
            }
        }
    }

    public TextBox IntervalTimeCookTextBox
    {
        get { return _intervalTimeCookTextBox; }
        set
        {
            if (value != null)
            {
                _intervalTimeCookTextBox = value;
            }
        }
    }

    public void Main()
    {
        Panel = CreaterPanel(new Size(_window.Width - 20, _window.Height - 20), new Point(10, 10), _window);
        CountCooksTextBox = CreaterTextBox(new Size(400, 50), new Point(0, 0), "Input count cooks", Panel);
        CountServersTextBox = CreaterTextBox(new Size(400, 50), new Point(0, CountCooksTextBox.Height + CountCooksTextBox.Location.Y + 10),
            "Input count servers", Panel);
        CountOrderTakersTextBox = CreaterTextBox(new Size(400, 50), new Point(0, CountServersTextBox.Location.Y + CountServersTextBox.Height + 10),
            "Input count order taker", Panel);
        IntervalTimeArriveCustomersTextBox = CreaterTextBox(new Size(400, 50), new Point(0, CountOrderTakersTextBox.Location.Y + 
            CountOrderTakersTextBox.Height + 10), "Input interval time arrive customers", Panel);
        IntervalTimeCookTextBox = CreaterTextBox(new Size(400, 50), new Point(0, IntervalTimeArriveCustomersTextBox.Location.Y +
            IntervalTimeArriveCustomersTextBox.Height + 10), "Input interval time cook tickets", Panel);
        StartButton = CreaterButton(new Size(200, 70), new Point(400 + 50, 50), "Start", Panel);
    }

    private Panel CreaterPanel(Size size, Point point, Control control)
    {
        Panel panel = new Panel();
        panel.Size = size;
        panel.Location = point;
        control.Controls.Add(panel);
        return panel;
    }

    private TextBox CreaterTextBox(Size size, Point point, string placeHolder, Control control)
    {
        TextBox textBox = new TextBox();
        textBox.Size = size;
        textBox.Location = point;
        textBox.PlaceholderText = placeHolder;
        textBox.TabStop = false;
        textBox.KeyPress += KeyPressTextBox;
        control.Controls.Add(textBox);
        return textBox;
    }

    private Button CreaterButton(Size size, Point point, string text, Control control)
    {
        Button button = new Button();
        button.Size = size;
        button.Location = point;
        button.Text = text;
        button.Click += PressButton;
        control.Controls.Add(button);
        return button;
    }

    private void KeyPressTextBox(object sender, KeyPressEventArgs e) 
    {
        if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void PressButton(object sender, EventArgs e)
    {
        CountCooksTextBox.Visible = false;
        CountServersTextBox.Visible = false;
        CountOrderTakersTextBox.Visible = false;
        IntervalTimeArriveCustomersTextBox.Visible = false;
        IntervalTimeCookTextBox.Visible = false;
        StartButton.Visible = false;

        PresentationSimulation presentationSimulation = new PresentationSimulation(int.Parse(CountCooksTextBox.Text), int.Parse(CountServersTextBox.Text),
            int.Parse(CountOrderTakersTextBox.Text), int.Parse(IntervalTimeArriveCustomersTextBox.Text), int.Parse(IntervalTimeCookTextBox.Text), _window);
        presentationSimulation.Main();
    }
}