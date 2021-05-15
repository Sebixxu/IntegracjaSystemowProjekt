using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using IntegracjaSystemowProjekt.Models;
using IntegracjaSystemowProjekt.WPF.Extensions;
using IntegracjaSystemowProjekt.WPF.Helpers;
using IntegracjaSystemowProjekt.WPF.Models;
using IntegracjaSystemowProjekt.WPF.Views;
using ISP.DataAccess;
using ISP.DatabaseAccess;
using ISP.DatabaseAccess.DataAccess;
using Microsoft.Win32;
using Screen = Caliburn.Micro.Screen;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace IntegracjaSystemowProjekt.WPF.ViewModels
{
    public class ShellViewModel : Screen
    {
        public static bool IsInLoadingState = true;
        public static LaptopsDataAccess LaptopsDataAccess = new LaptopsDataAccess();

        public static Dictionary<RecordModel, int> Errors = new Dictionary<RecordModel, int>();
        private BindableCollection<RecordModel> _records = new BindableCollection<RecordModel>();

        private string numberOfDuplicatesFieldText = "Liczba duplikatów to: {0}";
        private string numberOfNewRowsFieldText = "Liczba nowych wierszy to: {0}";

        private string numberOfDuplicatesField = "Liczba duplikatów to: {0}";

        public string NumberOfDuplicatesField
        {
            get { return numberOfDuplicatesField; }
            set
            {
                if (value != numberOfDuplicatesField)
                {
                    numberOfDuplicatesField = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("NumberOfDuplicatesField"));
                }
            }
        }

        private string numberOfNewRowsField = "Liczba nowych wierszy to: {0}";

        public string NumberOfNewRowsField
        {
            get { return numberOfNewRowsField; }
            set
            {
                if (value != numberOfNewRowsField)
                {
                    numberOfNewRowsField = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("NumberOfNewRowsField"));
                }
            }
        }

        public BindableCollection<RecordModel> Records
        {
            get { return _records; }
            set { _records = value; }
        }

        public ShellViewModel()
        {
            var loadDefaultData = LoadDefaultData().ToList();

            var duplicates = GetDuplicates(loadDefaultData);
            var validatedRecordModels = GetValidatedData(loadDefaultData, duplicates);
            SetDataWithDuplicatedInfo(validatedRecordModels);

            NotifyOfPropertyChange(() => Records);

            IsInLoadingState = false;
        }

        public void OpenTxtFileDialog()
        {
            IsInLoadingState = true;

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

                    var records = DataAccess.GetTxtFileDataByPath(path);
                    var recordsList = records as Record[] ?? records.ToArray();

                    var duplicates = GetDuplicates(recordsList);
                    var validatedRecordModels = GetValidatedData(recordsList, duplicates);
                    SetDataWithDuplicatedInfo(validatedRecordModels);

                    MessageBox.Show("Wskazany plik został wczytany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            IsInLoadingState = false;
        }

        private List<RecordModel> GetValidatedData(IEnumerable<Record> laptopsList, List<KeyValuePair<Record, int>> duplicated)
        {
            List<RecordModel> recordModels = new List<RecordModel>();

            foreach (var record in laptopsList)
            {
                var isDuplicate = duplicated.FirstOrDefault(x => Equals(x.Key, record)).Value > 1;

                var currentRecordModel = Mapper.MapValue(record);

                currentRecordModel.RecordState = isDuplicate ? RecordState.Duplicate : RecordState.Normal;
                currentRecordModel.RecordColor = AssignBackgroundColor(currentRecordModel.RecordState);

                recordModels.Add(currentRecordModel);
            }

            return recordModels;
        }

        private void SetDataWithDuplicatedInfo(List<RecordModel> validatedRecordModels)
        {
            var newRowsCount = validatedRecordModels.Count(x => x.RecordState == RecordState.Normal);
            var duplicateRowsCount = validatedRecordModels.Count(x => x.RecordState == RecordState.Duplicate);

            NumberOfNewRowsField = string.Format(numberOfNewRowsFieldText, newRowsCount);
            NumberOfDuplicatesField = string.Format(numberOfDuplicatesFieldText, duplicateRowsCount);

            Records.Clear();
            Records.AddRange(validatedRecordModels);
        }

        private List<KeyValuePair<T, int>> GetDuplicates<T>(IEnumerable<T> records) where T : class
        {
            Dictionary<T, int> recordDictionary = new Dictionary<T, int>();

            var recordsList = records.ToList();
            foreach (var record in recordsList)
            {
                if (!recordDictionary.TryGetValue(record, out _))
                {
                    recordDictionary.Add(record, 1);
                }
                else
                {
                    recordDictionary[record]++;
                }
            }

            var duplicated = recordDictionary.Where(x => x.Value > 1).ToList();
            return duplicated;
        }

        private string AssignBackgroundColor(RecordState recordState)
        {
            if (recordState == RecordState.Duplicate)
                return ColorTemplates.DuplicatedRow;

            if (recordState == RecordState.Modified)
                return ColorTemplates.ModifiedRow;

            return ColorTemplates.NormalRow;
        }

        public void SaveDataToTxtFile()
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

        public void OpenXmlFileDialog()
        {
            IsInLoadingState = true;

            MessageBox.Show("Na następnym oknie należy wybrać plik z odpowiednim formatem danych.", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Wybierz plik do odczytania",
                Filter = "Text Document (*.xml) | *.xml"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckFileExists)
                {
                    var path = openFileDialog.FileName;

                    var records = DataAccess.GetXmlFileDataByPath(path);

                    var duplicates = GetDuplicates(records.LaptopsCollection);
                    var validatedRecordModels = GetValidatedData(records.LaptopsCollection, duplicates);
                    SetDataWithDuplicatedInfo(validatedRecordModels);

                    MessageBox.Show("Wskazany plik został wczytany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            IsInLoadingState = false;
        }

        public void SaveDataToXmlFile()
        {
            if (Errors.Any(x => x.Value > 0))
            {
                MessageBox.Show("Znaleziono błędne dane, popraw je przed zapisem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var laptops = Mapper.MapRecordToLaptops(Records);

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Zapisz plik",
                    Filter = "Text Files(*.xml)|*.xml|All(*.*)|*"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    DataAccess.SaveXmlFile(laptops, saveFileDialog.FileName);

                    MessageBox.Show("Pomyślnie zapisano plik.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private List<RecordModel> GetValidatedData(IEnumerable<Laptop> laptopsList, List<KeyValuePair<Laptop, int>> duplicated)
        {
            List<RecordModel> recordModels = new List<RecordModel>();

            foreach (var record in laptopsList)
            {
                var isDuplicate = duplicated.FirstOrDefault(x => Equals(x.Key, record)).Value > 1;

                var currentRecordModel = Mapper.MapValue(record);

                currentRecordModel.RecordState = isDuplicate ? RecordState.Duplicate : RecordState.Normal;
                currentRecordModel.RecordColor = AssignBackgroundColor(currentRecordModel.RecordState);

                recordModels.Add(currentRecordModel);
            }

            return recordModels;
        }

        public void LoadDataFromDatabase()
        {
            var laptopsDtos = LaptopsDataAccess.GetLaptops().ToList();

            var duplicates = GetDuplicates(laptopsDtos);
            var validatedRecordModels = GetValidatedData(laptopsDtos, duplicates);
            SetDataWithDuplicatedInfo(validatedRecordModels);
        }

        public void SaveDataToDatabase()
        {
            if (Errors.Any(x => x.Value > 0))
            {
                MessageBox.Show("Znaleziono błędne dane, popraw je przed zapisem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var laptopsDtos = Mapper.MapRecordsToLaptopsDto(Records);

                LaptopsDataAccess.AddLaptops(laptopsDtos);

                MessageBox.Show("Pomyślnie zapisano dane.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private List<RecordModel> GetValidatedData(IEnumerable<LaptopsDto> laptopsList, List<KeyValuePair<LaptopsDto, int>> duplicated)
        {
            List<RecordModel> recordModels = new List<RecordModel>();

            foreach (var record in laptopsList)
            {
                var isDuplicate = duplicated.FirstOrDefault(x => Equals(x.Key, record)).Value > 1;

                var currentRecordModel = Mapper.MapLaptopDtoToRecord(record);

                currentRecordModel.RecordState = isDuplicate ? RecordState.Duplicate : RecordState.Normal;
                currentRecordModel.RecordColor = AssignBackgroundColor(currentRecordModel.RecordState);

                recordModels.Add(currentRecordModel);
            }

            return recordModels;
        }

        private IEnumerable<Record> LoadDefaultData()
        {
            var records = DataAccess.GetDefaultTxtFileData();

            return records;
        }
    }
}