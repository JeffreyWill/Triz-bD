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

    public partial class AddVacationWindow : Window
    {
        private Vacations _currentVacation = new Vacations();

        public AddVacationWindow()
        {
            InitializeComponent();
            DataContext = _currentVacation;
            comboBoxEmployee_ID.ItemsSource = kadriEntities.getContext().Employees.ToList();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxEmployee_ID.SelectedValue == null || string.IsNullOrWhiteSpace(textBoxStart_Date.Text) ||
                string.IsNullOrWhiteSpace(textBoxEnd_Date.Text) || string.IsNullOrWhiteSpace(textBoxVacation_Type.Text))
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

            _currentVacation.Employee_ID = (int)comboBoxEmployee_ID.SelectedValue;
            _currentVacation.Start_Date = startDate;
            _currentVacation.End_Date = endDate;
            _currentVacation.Vacation_Type = textBoxVacation_Type.Text;

            try
            {
                kadriEntities.getContext().Vacations.Add(_currentVacation);
                kadriEntities.getContext().SaveChanges();
                MessageBox.Show("отпуск добавлен!");
                new MainWindow().Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка при добавлении: " + ex.Message);
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
