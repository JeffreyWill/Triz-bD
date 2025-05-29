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
    public partial class EditEmploymentContractWindow : Window
    {
        private Employment_Contracts _currentContract;

        public EditEmploymentContractWindow(Employment_Contracts selectedContract)
        {
            InitializeComponent();
            _currentContract = selectedContract;
            DataContext = _currentContract;
            comboBoxEmployee_ID.ItemsSource = kadriEntities.getContext().Employees.ToList();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxEmployee_ID.SelectedValue == null || string.IsNullOrWhiteSpace(textBoxStart_Date.Text) ||
                string.IsNullOrWhiteSpace(textBoxEnd_Date.Text) || string.IsNullOrWhiteSpace(textBoxSalary.Text))
            {
                MessageBox.Show("заполните все поля!");
                return;
            }

            DateTime startDate;
            if (!DateTime.TryParse(textBoxStart_Date.Text, out startDate))
            {
                MessageBox.Show("введите корректную дату начала (например, 01.01.2020)");
                return;
            }

            DateTime endDate;
            if (!DateTime.TryParse(textBoxEnd_Date.Text, out endDate))
            {
                MessageBox.Show("введите корректную дату окончания (например, 01.01.2021)");
                return;
            }

            decimal salary;
            if (!decimal.TryParse(textBoxSalary.Text, out salary))
            {
                MessageBox.Show("введите корректную зарплату (число)");
                return;
            }

            _currentContract.Employee_ID = (int)comboBoxEmployee_ID.SelectedValue;
            _currentContract.Start_Date = startDate;
            _currentContract.End_Date = endDate;
            _currentContract.Salary = salary;

            try
            {
                kadriEntities.getContext().SaveChanges();
                MessageBox.Show("контракт обновлен!");
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
