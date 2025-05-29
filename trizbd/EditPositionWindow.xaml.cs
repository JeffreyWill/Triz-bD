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
    public partial class EditPositionWindow : Window
    {
        private Positions _currentPosition;

        public EditPositionWindow(Positions selectedPosition)
        {
            InitializeComponent();
            _currentPosition = selectedPosition;
            DataContext = _currentPosition;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPosition_Name.Text) || string.IsNullOrWhiteSpace(textBoxDescription.Text))
            {
                MessageBox.Show("заполните все поля!");
                return;
            }

            _currentPosition.Position_Name = textBoxPosition_Name.Text;
            _currentPosition.Description = textBoxDescription.Text;

            try
            {
                kadriEntities.getContext().SaveChanges();
                MessageBox.Show("должность обновлена!");
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
