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

    public partial class AddPositionWindow : Window
    {
        private Positions _currentPosition = new Positions();

        public AddPositionWindow()
        {
            InitializeComponent();
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
                kadriEntities.getContext().Positions.Add(_currentPosition);
                kadriEntities.getContext().SaveChanges();
                MessageBox.Show("должность добавлена!");
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
