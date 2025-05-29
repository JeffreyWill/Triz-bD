using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace trizbd
{
    public partial class EditSalaryWindow : Window
    {
        private Salaries _currentSalary;

        public EditSalaryWindow(Salaries selectedSalary)
        {
            InitializeComponent();
            _currentSalary = selectedSalary;
            DataContext = _currentSalary;
            comboBoxEmployee_ID.ItemsSource = kadriEntities.getContext().Employees.ToList();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxEmployee_ID.SelectedValue == null || string.IsNullOrWhiteSpace(textBoxAmount.Text) ||
                string.IsNullOrWhiteSpace(textBoxPayment_Date.Text) || string.IsNullOrWhiteSpace(textBoxPayment_Type.Text))
            {
                MessageBox.Show("заполните все поля!");
                return;
            }

            DateTime paymentDate;
            if (!DateTime.TryParse(textBoxPayment_Date.Text, out paymentDate))
            {
                MessageBox.Show("введите корректную дату оплаты (например, 01.01.2020)");
                return;
            }

            decimal amount;
            if (!decimal.TryParse(textBoxAmount.Text, out amount))
            {
                MessageBox.Show("введите корректную сумму (число)");
                return;
            }

            _currentSalary.Employee_ID = (int)comboBoxEmployee_ID.SelectedValue;
            _currentSalary.Amount = amount;
            _currentSalary.Payment_Date = paymentDate;
            _currentSalary.Payment_Type = textBoxPayment_Type.Text;

            try
            {
                kadriEntities.getContext().SaveChanges();
                MessageBox.Show("зарплата обновлена!");
                new MainWindow().Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка при сохранении: " + ex.Message);
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
