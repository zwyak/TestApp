using System.Windows;
using System.Windows.Controls;

namespace TestApp
{
    public partial class PhoneWindow : Window
    {
        public Phone Phone { get; private set; }

        public PhoneWindow(Phone p)
        {
            InitializeComponent();
            Phone = p;
            this.DataContext = Phone;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            ErrorBlock.Text = "";

            FluentValidation.Results.ValidationResult results = new PhoneValidator().Validate(Phone);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    ErrorBlock.Text += "Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage + "\r\n";
                }
            }
            else
            {
                DialogResult = true;
            }
        }
    }
}
