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
using trizbd;
namespace trizbd
{
    
    public partial class AddEmployeeWindow : Window
    {
        private Employees _currentEmployee = new Employees();

        public AddEmployeeWindow()
        {
            InitializeComponent();
            DataContext = _currentEmployee;
            comboBoxPosition_ID.ItemsSource = kadriEntities.getContext().Positions.ToList();
            comboBoxDepartment_ID.ItemsSource = kadriEntities.getContext().Departments.ToList();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFullName.Text) || string.IsNullOrWhiteSpace(textBoxBirthDate.Text) ||
                comboBoxGender.SelectedItem == null || string.IsNullOrWhiteSpace(textBoxAddress.Text) ||
                string.IsNullOrWhiteSpace(textBoxPhone.Text) || comboBoxPosition_ID.SelectedValue == null ||
                comboBoxDepartment_ID.SelectedValue == null)
            {
                MessageBox.Show("заполните все поля!");
                return;
            }

            DateTime birthDate;
            if (!DateTime.TryParse(textBoxBirthDate.Text, out birthDate))
            {
                MessageBox.Show("введите корректную дату рождения (например, 01.01.1990)");
                return;
            }

            _currentEmployee.Full_Name = textBoxFullName.Text;
            _currentEmployee.Birth_Date = birthDate;
            _currentEmployee.Gender = (comboBoxGender.SelectedItem as ComboBoxItem)?.Content.ToString();
            _currentEmployee.Address = textBoxAddress.Text;
            _currentEmployee.Phone = textBoxPhone.Text;
            _currentEmployee.Position_ID = (int)comboBoxPosition_ID.SelectedValue;
            _currentEmployee.Department_ID = (int)comboBoxDepartment_ID.SelectedValue;

            try
            {
                kadriEntities.getContext().Employees.Add(_currentEmployee);
                kadriEntities.getContext().SaveChanges();
                MessageBox.Show("сотрудник добавлен!");
                new MainWindow().Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка при добавлении: " + ex.Message + (ex.InnerException != null ? "\nвнутренняя ошибка: " + ex.InnerException.Message : ""));
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void comboBoxPosition_ID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBoxGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
