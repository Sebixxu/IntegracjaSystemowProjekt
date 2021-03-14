using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using IntegracjaSystemowProjekt.Models;
using IntegracjaSystemowProjekt.WPF.Helpers;
using IntegracjaSystemowProjekt.WPF.Models;
using ISP.DataAccess;
using Microsoft.Win32;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace IntegracjaSystemowProjekt.WPF.ViewModels
{
    public class ShellViewModel : Screen
    {
        public static Dictionary<RecordModel, int> Errors = new Dictionary<RecordModel, int>();
        private BindableCollection<RecordModel> _records = new BindableCollection<RecordModel>();

        public BindableCollection<RecordModel> Records
        {
            get { return _records; }
            set { _records = value; }
        }

        public ShellViewModel()
        {
            var loadDefaultData = LoadDefaultData().ToList();

            Records.AddRange(loadDefaultData);
        }

        public void OpenFileDialog()
        {
            MessageBox.Show("Na następnym oknie należy wybrać plik z odpowiednim formatem danych.", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Wybierz plik do odczytania",
                Filter = "Text Document (*.txt) | *.txt"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckFileExists)
                {
                    var path = openFileDialog.FileName;

                    var records = DataAccess.GetFileDataByPath(path);

                    Records.Clear();
                    Records.AddRange(ValueHelper.MapValues(records));

                    MessageBox.Show("Wskazany plik został wczytany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public void SaveDataToFile()
        {
            if (Errors.Any(x => x.Value > 0))
            {
                MessageBox.Show("Znaleziono błędne dane, popraw je przed zapisem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var fileContent = FileHelper.BuildFileContent(Records);

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Zapisz plik",
                    Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, fileContent);

                    MessageBox.Show("Pomyślnie zapisano plik.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private IEnumerable<RecordModel> LoadDefaultData()
        {
            var records = DataAccess.GetDefaultFileData();

            var recordModels = ValueHelper.MapValues(records);

            return recordModels;
        }



        private bool ParseBoolValue(string value)
        {
            if (bool.TryParse(value, out var result))
                return result;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            if (value == "1" || value.ToLower() == "tak" || value.ToLower() == "yes")
                return true;

            return false;
        }

        private int? ParseIntValue(string value)
        {
            if (int.TryParse(value, out var result))
                return result;

            return null;
        }
    }
}