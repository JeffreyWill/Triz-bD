using System;
using System.Linq;
using System.Windows;
using trizbd;
namespace trizbd
{

    public partial class EditEmployeeWindow : Window
    {
        private Employees _currentEmployee; 

        public EditEmployeeWindow(Employees selectedEmployee)
        {
            InitializeComponent();
            _currentEmployee = selectedEmployee;
            DataContext = _currentEmployee;
            comboBoxPosition.ItemsSource = kadriEntities.getContext().Positions.ToList();
            comboBoxDepartment.ItemsSource = kadriEntities.getContext().Departments.ToList();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFullName.Text) || string.IsNullOrEmpty(textBoxBirthDate.Text) ||
                comboBoxGender.SelectedItem == null || string.IsNullOrEmpty(textBoxAddress.Text) ||
                string.IsNullOrEmpty(textBoxPhone.Text) || comboBoxPosition.SelectedItem == null ||
                comboBoxDepartment.SelectedItem == null)
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
            _currentEmployee.Birth_Date = birthDate;
            _currentEmployee.Position_ID = (comboBoxPosition.SelectedItem as Positions).Position_ID;
            _currentEmployee.Department_ID = (comboBoxDepartment.SelectedItem as Departments).Department_ID;

            try
            {
                kadriEntities.getContext().SaveChanges();
                MessageBox.Show("данные сотрудника обновлены!");
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
