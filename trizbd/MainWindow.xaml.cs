using System.Windows;
using System.Linq;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using trizbd;

namespace trizbd 
{

    public partial class MainWindow : Window
    {
        private string currentTable = "Employees";

        public MainWindow()
        {
            InitializeComponent();
            comboBoxTables.SelectedIndex = 0; 
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var context = kadriEntities.getContext();
                dataGrid.Columns.Clear(); 

                switch (currentTable)
                {
                    case "Employees":
                        var employees = context.Employees
                            .Include("Positions")
                            .Include("Departments")
                            .OrderBy(e => e.Employee_ID)
                            .ToList();
                        dataGrid.ItemsSource = employees;
                        GenerateColumnsForEmployees();
                        break;
                    case "Positions":
                        var positions = context.Positions.OrderBy(p => p.Position_ID).ToList();
                        dataGrid.ItemsSource = positions;
                        GenerateColumnsForPositions();
                        break;
                    case "Departments":
                        var departments = context.Departments.OrderBy(d => d.Department_ID).ToList();
                        dataGrid.ItemsSource = departments;
                        GenerateColumnsForDepartments();
                        break;
                    case "Employment_Contracts":
                        var contracts = context.Employment_Contracts.OrderBy(ec => ec.Contract_ID).ToList();
                        dataGrid.ItemsSource = contracts;
                        GenerateColumnsForEmploymentContracts();
                        break;
                    case "Salaries":
                        var salaries = context.Salaries.OrderBy(s => s.Salary_ID).ToList();
                        dataGrid.ItemsSource = salaries;
                        GenerateColumnsForSalaries();
                        break;
                    case "Vacations":
                        var vacations = context.Vacations.OrderBy(v => v.Vacation_ID).ToList();
                        dataGrid.ItemsSource = vacations;
                        GenerateColumnsForVacations();
                        break;
                }

                AddActionColumns();

                if ((dataGrid.ItemsSource as System.Collections.IList)?.Count == 0)
                {
                    MessageBox.Show($"в таблице {currentTable} нет записей!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ошибка загрузки данных: {ex.Message}");
            }
        }

        private void GenerateColumnsForEmployees()
        {
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Employee_ID", Binding = new Binding("Employee_ID"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "FullName", Binding = new Binding("Full_Name"), Width = 120 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "BirthDate", Binding = new Binding("Birth_Date") { StringFormat = "dd.MM.yyyy" }, Width = 100 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Gender", Binding = new Binding("Gender"), Width = 50 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Address", Binding = new Binding("Address"), Width = 150 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Phone", Binding = new Binding("Phone"), Width = 100 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Position_ID", Binding = new Binding("Position_ID"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Department_ID", Binding = new Binding("Department_ID"), Width = 80 });
        }

        private void GenerateColumnsForPositions()
        {
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Position_ID", Binding = new Binding("Position_ID"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Position_Name", Binding = new Binding("Position_Name"), Width = 150 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Description", Binding = new Binding("Description"), Width = 200 });
        }

        private void GenerateColumnsForDepartments()
        {
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Department_ID", Binding = new Binding("Department_ID"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Department_Name", Binding = new Binding("Department_Name"), Width = 150 });
        }

        private void GenerateColumnsForEmploymentContracts()
        {
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Contract_ID", Binding = new Binding("Contract_ID"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Employee_ID", Binding = new Binding("Employee_ID"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Start_Date", Binding = new Binding("Start_Date") { StringFormat = "dd.MM.yyyy" }, Width = 100 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "End_Date", Binding = new Binding("End_Date") { StringFormat = "dd.MM.yyyy" }, Width = 100 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Salary", Binding = new Binding("Salary"), Width = 80 });
        }

        private void GenerateColumnsForSalaries()
        {
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Salary_ID", Binding = new Binding("Salary_ID"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Employee_ID", Binding = new Binding("Employee_ID"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Amount", Binding = new Binding("Amount"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Payment_Date", Binding = new Binding("Payment_Date") { StringFormat = "dd.MM.yyyy" }, Width = 100 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Payment_Type", Binding = new Binding("Payment_Type"), Width = 150 });
        }

        private void GenerateColumnsForVacations()
        {
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Vacation_ID", Binding = new Binding("Vacation_ID"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Employee_ID", Binding = new Binding("Employee_ID"), Width = 80 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Start_Date", Binding = new Binding("Start_Date") { StringFormat = "dd.MM.yyyy" }, Width = 100 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "End_Date", Binding = new Binding("End_Date") { StringFormat = "dd.MM.yyyy" }, Width = 100 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Vacation_Type", Binding = new Binding("Vacation_Type"), Width = 150 });
        }

        private void AddActionColumns()
        {
            var editColumn = new DataGridTemplateColumn { Width = 80 };
            editColumn.CellTemplate = new DataTemplate();
            var editButton = new FrameworkElementFactory(typeof(Button));
            editButton.SetValue(Button.ContentProperty, "изменить");
            editButton.SetValue(Button.BackgroundProperty, System.Windows.Media.Brushes.Transparent);
            editButton.SetValue(Button.ForegroundProperty, System.Windows.Media.Brushes.White);
            editButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(buttonEditGrid_Click));
            editColumn.CellTemplate.VisualTree = editButton;
            dataGrid.Columns.Add(editColumn);

            var deleteColumn = new DataGridTemplateColumn { Width = 80 };
            deleteColumn.CellTemplate = new DataTemplate();
            var deleteButton = new FrameworkElementFactory(typeof(Button));
            deleteButton.SetValue(Button.ContentProperty, "удалить");
            deleteButton.SetValue(Button.BackgroundProperty, System.Windows.Media.Brushes.Transparent);
            deleteButton.SetValue(Button.ForegroundProperty, System.Windows.Media.Brushes.White);
            deleteButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(buttonDeleteGrid_Click));
            deleteColumn.CellTemplate.VisualTree = deleteButton;
            dataGrid.Columns.Add(deleteColumn);
        }

        private void comboBoxTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxTables.SelectedItem is ComboBoxItem selectedItem)
            {
                currentTable = selectedItem.Content.ToString();
                LoadData();
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (currentTable)
            {
                case "Employees":
                    new AddEmployeeWindow().Show();
                    break;
                case "Positions":
                    new AddPositionWindow().Show();
                    break;
                case "Departments":
                    new AddDepartmentWindow().Show();
                    break;
                case "Employment_Contracts":
                    new AddEmploymentContractWindow().Show();
                    break;
                case "Salaries":
                    new AddSalaryWindow().Show();
                    break;
                case "Vacations":
                    new AddVacationWindow().Show();
                    break;
            }
            Close();
        }

        private void buttonEditGrid_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItem;
            if (selectedItem != null)
            {
                switch (currentTable)
                {
                    case "Employees":
                        new EditEmployeeWindow(selectedItem as Employees).Show();
                        break;
                    case "Positions":
                        new EditPositionWindow(selectedItem as Positions).Show();
                        break;
                    case "Departments":
                        new EditDepartmentWindow(selectedItem as Departments).Show();
                        break;
                    case "Employment_Contracts":
                        new EditEmploymentContractWindow(selectedItem as Employment_Contracts).Show();
                        break;
                    case "Salaries":
                        new EditSalaryWindow(selectedItem as Salaries).Show();
                        break;
                    case "Vacations":
                        new EditVacationWindow(selectedItem as Vacations).Show();
                        break;
                }
                Close();
            }
        }

        private void buttonDeleteGrid_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("выберите хотя бы одну запись для удаления!");
                return;
            }

            var itemsToRemove = dataGrid.SelectedItems.Cast<object>().ToList();
            if (itemsToRemove.Any())
            {
                if (MessageBox.Show($"вы уверены, что хотите удалить выбранные записи из таблицы {currentTable}?", "подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var context = kadriEntities.getContext();
                    switch (currentTable)
                    {
                        case "Employees":
                            context.Employees.RemoveRange(itemsToRemove.Cast<Employees>());
                            break;
                        case "Positions":
                            context.Positions.RemoveRange(itemsToRemove.Cast<Positions>());
                            break;
                        case "Departments":
                            context.Departments.RemoveRange(itemsToRemove.Cast<Departments>());
                            break;
                        case "Employment_Contracts":
                            context.Employment_Contracts.RemoveRange(itemsToRemove.Cast<Employment_Contracts>());
                            break;
                        case "Salaries":
                            context.Salaries.RemoveRange(itemsToRemove.Cast<Salaries>());
                            break;
                        case "Vacations":
                            context.Vacations.RemoveRange(itemsToRemove.Cast<Vacations>());
                            break;
                    }
                    context.SaveChanges();
                    LoadData();
                }
            }
        }
    }

}
