using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class WaitingCustomersUI
    {
        private Panel _waitingCustomersPanel;
        private Label _label;
        private List<Customer> _customersList = new List<Customer>();
        private List<Label> _customersLabels = new List<Label>();

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

        public WaitingCustomersUI(Panel panel)
        {
            _waitingCustomersPanel = panel;
        }

        public void AddNewCustomer(Customer customer)
        {
            _customersList.Add(customer);
            Label label = CreaterLabel(new Size(_waitingCustomersPanel.Width - 30, 70),
            new Point(5, 5 + (_customersList.Count - 1) * 70 + _waitingCustomersPanel.AutoScrollPosition.Y), customer.Name, _waitingCustomersPanel);
            _customersLabels.Add(label);
        }

        private Label CreaterLabel(Size size, Point point, string text, Control control)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = point;
            label.Size = size;
            label.Font = new Font("Times New Roman", 20, FontStyle.Regular);
            label.TextAlign = ContentAlignment.MiddleCenter;
            control.Controls.Add(label);
            return label;
        }

        public bool IsAnyWaitingCustomer()
        {
            return (_customersList.Count > 0) ? true : false;
        }

        public Customer FindCustomer(int id)
        {
            Customer customer = _customersList.Find(MatchById(id));
            if (customer != null)
            {
                return customer;
            }
            else
            {
                return new Customer();
            }
        }

        private Predicate<Customer> MatchById(int id)
        {
            return delegate (Customer customer) { return customer.Id == id; };
        }

        public void RemoveWaitingCustomer(Customer customer)
        {
            int index = _customersList.IndexOf(customer);
            _customersList.Remove(customer);

            Label label = _customersLabels[index];
            _waitingCustomersPanel.Controls.Remove(label);
            _customersLabels.Remove(label);
            label.Dispose();

            RefreshLocation();
        }

        private void RefreshLocation()
        {
            foreach (Label label in _customersLabels)
            {
                label.Location = new Point(label.Location.X, label.Location.Y - label.Height);
            }
        }
    }
}
