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
    using trizbd;
    using System.Windows;

    public partial class EditDepartmentWindow : Window
    {
        private Departments _currentDepartment;

        public EditDepartmentWindow(Departments selectedDepartment)
        {
            InitializeComponent();
            _currentDepartment = selectedDepartment;
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
                kadriEntities.getContext().SaveChanges();
                MessageBox.Show("отдел обновлен!");
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
