using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Globalization;
using Microsoft.VisualBasic.FileIO;

namespace BankApplication
{
    public partial class MainWindow : Window
    {
         ObservableCollection<LocalData> Datas { get; set; }

        private string _currentUser;
        private string filePath;

        public MainWindow(string CurrentUser)
        {
            InitializeComponent();
            _currentUser = CurrentUser;
            AccessView.Text = _currentUser;
            DirectoryAndFile_preference();
            Datas = LoadDataFromCsv(filePath);
            DataGridClientData.ItemsSource = Datas;

            DataGrid_Preference();
        }

        private void DirectoryAndFile_preference()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "DataBase";
            string fileName = "DataBase.csv";
            string folderPath = Path.Combine(baseDirectory, folderName);


            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
 
            filePath = Path.Combine(folderPath, fileName);

            if (!File.Exists(filePath))
            {
                using (var fileStream = File.Create(filePath)) { };     
            }
        }
        private ObservableCollection<LocalData> LoadDataFromCsv(string filePath)
        {
            ObservableCollection<LocalData> data = new ObservableCollection<LocalData>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                bool isFirstRow = true;

                while (!parser.EndOfData)
                {
                    // Пропускаем первую строку с заголовками
                    string[] fields = parser.ReadFields();
                    if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue;
                    }

                    var localData = new LocalData
                    {
                        FullName = fields[0],
                        NumberPhone = fields[1],
                        NumberPassport = fields[2]
                    };

                    data.Add(localData);
                }
            }

            return data;
        }

        private void DataGridClientData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            authorizationWindow authorizationWindow = new authorizationWindow();
            authorizationWindow.Show();
            this.Close();

        }

        private void DataGrid_Preference()
        {

            if(_currentUser == "manager")
            {

            DataGridClientData.CanUserDeleteRows = AuthenticationService.PermissionsAccess;
            DataGridClientData.CanUserAddRows = AuthenticationService.PermissionsAccess;
                for (int i = 0; i < DataGridClientData.Columns.Count; i++)
                {
                    // Если текущий индекс равен 3 или 4, пропускаем его
                    if (i == 1 || i == 2)
                    {
                        continue; // Переходим к следующей итерации цикла
                    }

                    DataGridClientData.Columns[i].IsReadOnly = true; // Пример установки остальных столбцов как только для чтения
                }

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveDataToCsv(filePath, Datas);
            MessageBox.Show("Данные успешно сохранены.");
        }

        private void SaveDataToCsv(string filePath, ObservableCollection<LocalData> data)
        {
            // Создаем или перезаписываем файл с данными
            using (StreamWriter file = new StreamWriter(filePath, false))
            {
                // Записываем заголовок
                file.WriteLine("FullName,NumberPhone,NumberPassport");

                // Записываем данные
                foreach (var item in data)
                {
                    string line = $"{item.FullName},{item.NumberPhone},{item.NumberPassport}";
                    file.WriteLine(line);
                }
            }
        }

    }
}