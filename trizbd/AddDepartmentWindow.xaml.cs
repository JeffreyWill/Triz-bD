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
public partial class AddDepartmentWindow : Window
    {
        private Departments _currentDepartment = new Departments();

        public AddDepartmentWindow()
        {
            InitializeComponent();
            DataContext = _currentDepartment;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxDepartment_Name.Text))
            {
                MessageBox.Show("заполните поле названия!");
                return;
            }

            _currentDepartment.Department_Name = textBoxDepartment_Name.Text;

            try
            {
                kadriEntities.getContext().Departments.Add(_currentDepartment);
                kadriEntities.getContext().SaveChanges();
                MessageBox.Show("отдел добавлен!");
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
