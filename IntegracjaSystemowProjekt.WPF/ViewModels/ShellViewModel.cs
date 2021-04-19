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

            AssignRecordsToGridFromTxt(loadDefaultData);

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
                    CheckDuplicates(records);
                    //AssignRecordsToGridFromTxt(records);

                    MessageBox.Show("Wskazany plik został wczytany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            IsInLoadingState = false;
        }

        private void CheckDuplicates(IEnumerable<Record> records)
        {
            Dictionary<Record, int> recordDictionary = new Dictionary<Record, int>();

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

            List<RecordModel> recordModels = new List<RecordModel>();

            foreach (var record in recordsList)
            {
                var isDuplicate = duplicated.FirstOrDefault(x => Equals(x.Key, record)).Value > 1; 

                var currentRecordModel = ValueHelper.MapValue(record);

                currentRecordModel.RecordState = isDuplicate ? RecordState.Duplicate : RecordState.Normal;
                currentRecordModel.RecordColor = AssignBackgroundColor(currentRecordModel.RecordState);

                recordModels.Add(currentRecordModel);
            }

            var newRowsCount = recordModels.Count(x => x.RecordState == RecordState.Normal);
            var duplicateRowsCount = recordModels.Count(x => x.RecordState == RecordState.Duplicate);

            NumberOfNewRowsField = string.Format(numberOfNewRowsFieldText, newRowsCount);
            NumberOfDuplicatesField = string.Format(numberOfDuplicatesFieldText, duplicateRowsCount);

            Records.Clear();
            Records.AddRange(recordModels);
        }

        private void AssignRecordsToGridFromTxt(IEnumerable<Record> records)
        {
            List<RecordModel> recordModels = new List<RecordModel>();

            foreach (var record in records)
            {
                var isDuplicatesFromTxtFile = CheckIsDuplicatesFromTxtFile(record);

                var currentRecordModel = ValueHelper.MapValue(record);

                currentRecordModel.RecordState = isDuplicatesFromTxtFile ? RecordState.Duplicate : RecordState.Normal;
                currentRecordModel.RecordColor = AssignBackgroundColor(currentRecordModel.RecordState);

                recordModels.Add(currentRecordModel);
            }

            var newRowsCount = recordModels.Count(x => x.RecordState == RecordState.Normal);
            var duplicateRowsCount = recordModels.Count(x => x.RecordState == RecordState.Duplicate);

            NumberOfNewRowsField = string.Format(numberOfNewRowsFieldText, newRowsCount);
            NumberOfDuplicatesField = string.Format(numberOfDuplicatesFieldText, duplicateRowsCount);

            Records.Clear();
            Records.AddRange(recordModels);
        }

        private string AssignBackgroundColor(RecordState recordState)
        {
            if (recordState == RecordState.Duplicate)
                return ColorTemplates.DuplicatedRow;

            if (recordState == RecordState.Modified)
                return ColorTemplates.ModifiedRow;

            return ColorTemplates.NormalRow;
        }

        private bool CheckIsDuplicatesFromTxtFile(Record record)
        {
            var laptopsDto = new LaptopsDto
            {
                ScreenDiagonal = record.ScreenDiagonal,
                Resolution = record.Resolution,
                DiskSize = record.DiskSize,
                ManufacturerName = record.ManufacturerName,
                Ram = record.Ram,
                Os = record.Os,
                IsTouchable = record.IsTouchable.ParseBoolValue(),
                Frequency = record.Frequency.ParseIntValue(),
                DiskType = string.IsNullOrEmpty(record.DiskType) ? null : record.DiskType,
                NumberOfPhysicalCores = record.NumberOfPhysicalCores.ParseIntValue(),
                Gpu = record.Gpu,
                Drive = record.Drive,
                ScreenSurfaceType = record.ScreenSurfaceType,
                Vram = record.Vram,
                ProcessorName = record.ProcessorName
            };

            var isAlreadyExisting = LaptopsDataAccess.IsAlreadyExisting(laptopsDto);

            return isAlreadyExisting;
        }

        private bool CheckIsDuplicate(RecordModel recordModel)
        {
            var isAlreadyExisting = LaptopsDataAccess.IsAlreadyExisting(new LaptopsDto
            {
                ScreenDiagonal = recordModel.ScreenDiagonal,
                Resolution = recordModel.Resolution,
                DiskSize = recordModel.DiskSize,
                ManufacturerName = recordModel.ManufacturerName,
                Ram = recordModel.Ram,
                Os = recordModel.Os,
                IsTouchable = recordModel.IsTouchable,
                Frequency = recordModel.Frequency,
                DiskType = recordModel.DiskType,
                NumberOfPhysicalCores = recordModel.NumberOfPhysicalCores,
                Gpu = recordModel.Gpu,
                Drive = recordModel.Drive,
                ScreenSurfaceType = recordModel.ScreenSurfaceType,
                Vram = recordModel.Vram,
                ProcessorName = recordModel.ProcessorName
            });

            return isAlreadyExisting;
        }

        private bool CheckIsDuplicatesFromXmlFile(Laptop laptop)
        {
            var laptopsDto = new LaptopsDto
            {
                ScreenDiagonal = laptop.Screen.Size,
                Resolution = laptop.Screen.Resolution,
                DiskSize = laptop.Disc.Storage,
                ManufacturerName = laptop.Manufacturer,
                Ram = laptop.Ram,
                Os = laptop.Os,
                IsTouchable = laptop.Screen.IsTouchable.ParseBoolValue(),
                Frequency = laptop.Processor.ClockSpeedAsText.ParseIntValue(),
                DiskType = string.IsNullOrEmpty(laptop.Disc.Type) ? null : laptop.Disc.Type,
                NumberOfPhysicalCores = laptop.Processor.PhysicalCoresAsText.ParseIntValue(),
                Gpu = laptop.GraphicCard.Name,
                Drive = laptop.DiscReader,
                ScreenSurfaceType = laptop.Screen.Type,
                Vram = laptop.GraphicCard.Memory,
                ProcessorName = laptop.Processor.Name
            };
            var isAlreadyExisting = LaptopsDataAccess.IsAlreadyExisting(laptopsDto);

            return isAlreadyExisting;
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
                    AssignRecordsToGridFromXml(records);
                    
                    MessageBox.Show("Wskazany plik został wczytany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            IsInLoadingState = false;
        }

        private void AssignRecordsToGridFromXml(Laptops laptops)
        {
            List<RecordModel> recordModels = new List<RecordModel>();

            foreach (var laptop in laptops.LaptopsCollection)
            {
                var isDuplicatesFromTxtFile = CheckIsDuplicatesFromXmlFile(laptop);

                var currentRecordModel = ValueHelper.MapValue(laptop);

                currentRecordModel.RecordState = isDuplicatesFromTxtFile ? RecordState.Duplicate : RecordState.Normal;
                currentRecordModel.RecordColor = AssignBackgroundColor(currentRecordModel.RecordState);

                recordModels.Add(currentRecordModel);
            }

            var newRowsCount = recordModels.Count(x => x.RecordState == RecordState.Normal);
            var duplicateRowsCount = recordModels.Count(x => x.RecordState == RecordState.Duplicate);

            NumberOfNewRowsField = string.Format(numberOfNewRowsFieldText, newRowsCount);
            NumberOfDuplicatesField = string.Format(numberOfDuplicatesFieldText, duplicateRowsCount);

            Records.Clear();
            Records.AddRange(recordModels);
        }

        public void SaveDataToXmlFile()
        {
            if (Errors.Any(x => x.Value > 0))
            {
                MessageBox.Show("Znaleziono błędne dane, popraw je przed zapisem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var laptops = MapRecordToLaptops(Records);

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

        public void LoadDataFromDatabase()
        {
            var laptopsDtos = LaptopsDataAccess.GetLaptops();

            NumberOfNewRowsField = "Aktualnie prezentowane dane z bazy";
            NumberOfDuplicatesField = "Aktualnie prezentowane dane z bazy";

            Records.Clear();
            Records.AddRange(MapLaptopsDtoToRecords(laptopsDtos));
        }

        public void SaveDataToDatabase()
        {
            if (Errors.Any(x => x.Value > 0))
            {
                MessageBox.Show("Znaleziono błędne dane, popraw je przed zapisem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                foreach (var recordModel in Records)
                {
                    var isDuplicatesFromTxtFile = CheckIsDuplicate(recordModel);
                    recordModel.RecordState = isDuplicatesFromTxtFile ? RecordState.Duplicate : RecordState.Normal;
                }

                var recordsWithoutDuplicates = Records.Where(x => x.RecordState != RecordState.Duplicate);
                var laptopsDtos = MapRecordsToLaptopsDto(recordsWithoutDuplicates);

                LaptopsDataAccess.AddLaptops(laptopsDtos);

                MessageBox.Show("Pomyślnie zapisano dane - jeśli istniały nowe unikalne rekody.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private IEnumerable<RecordModel> MapLaptopsDtoToRecords(IEnumerable<LaptopsDto> laptopsDtos)
        {
            var record = new List<RecordModel>();

            foreach (var laptopsDto in laptopsDtos)
            {
                record.Add(new RecordModel
                {
                    ScreenDiagonal = laptopsDto.ScreenDiagonal,
                    Resolution = laptopsDto.Resolution,
                    ManufacturerName = laptopsDto.ManufacturerName,
                    DiskType = laptopsDto.DiskType,
                    DiskSize = laptopsDto.DiskSize,
                    Vram = laptopsDto.Vram,
                    Ram = laptopsDto.Ram,
                    Drive = laptopsDto.Drive,
                    Gpu = laptopsDto.Gpu,
                    Os = laptopsDto.Os,
                    ProcessorName = laptopsDto.ProcessorName,
                    ScreenSurfaceType = laptopsDto.ScreenSurfaceType,
                    IsTouchable = laptopsDto.IsTouchable,
                    Frequency = laptopsDto.Frequency,
                    NumberOfPhysicalCores = laptopsDto.NumberOfPhysicalCores
                });
            }

            return record;
        }

        private IEnumerable<LaptopsDto> MapRecordsToLaptopsDto(IEnumerable<RecordModel> recordModels)
        {
            var laptopsDtos = new List<LaptopsDto>();

            foreach (var recordModel in recordModels)
            {
                laptopsDtos.Add(new LaptopsDto
                {
                    ScreenDiagonal = recordModel.ScreenDiagonal,
                    Resolution = recordModel.Resolution,
                    ManufacturerName = recordModel.ManufacturerName,
                    DiskType = recordModel.DiskType,
                    DiskSize = recordModel.DiskSize,
                    Vram = recordModel.Vram,
                    Ram = recordModel.Ram,
                    Drive = recordModel.Drive,
                    Gpu = recordModel.Gpu,
                    Os = recordModel.Os,
                    ProcessorName = recordModel.ProcessorName,
                    ScreenSurfaceType = recordModel.ScreenSurfaceType,
                    IsTouchable = recordModel.IsTouchable,
                    Frequency = recordModel.Frequency,
                    NumberOfPhysicalCores = recordModel.NumberOfPhysicalCores
                });
            }

            return laptopsDtos;
        }

        private Laptops MapRecordToLaptops(BindableCollection<RecordModel> recordModels)
        {
            var laptops = new Laptops { ModDate = DateTime.Now.ToString(), LaptopsCollection = new List<Laptop>() };

            foreach (var recordModel in recordModels.Select((value, i) => new { i, value }))
            {
                laptops.LaptopsCollection.Add(new Laptop
                {
                    Disc = new Disc
                    {
                        Storage = recordModel.value.DiskSize,
                        Type = recordModel.value.DiskType
                    },
                    DiscReader = recordModel.value.Drive,
                    GraphicCard = new GraphicCard
                    {
                        Memory = recordModel.value.Vram,
                        Name = recordModel.value.Gpu
                    },
                    Id = recordModel.i + 1,
                    Manufacturer = recordModel.value.ManufacturerName,
                    Os = recordModel.value.Os,
                    Processor = new Processor
                    {
                        PhysicalCores = recordModel.value.NumberOfPhysicalCores,
                        PhysicalCoresAsText = recordModel.value.NumberOfPhysicalCores?.ToString(),
                        ClockSpeed = recordModel.value.Frequency,
                        ClockSpeedAsText = recordModel.value.Frequency?.ToString(),
                        Name = recordModel.value.ProcessorName
                    },
                    Ram = recordModel.value.Ram,
                    Screen = new IntegracjaSystemowProjekt.Models.Screen
                    {
                        IsTouchable = ParseBoolToXmlString(recordModel.value.IsTouchable),
                        Resolution = recordModel.value.Resolution,
                        Size = recordModel.value.ScreenDiagonal,
                        Type = recordModel.value.ScreenSurfaceType
                    }
                });
            }

            return laptops;
        }

        private IEnumerable<Record> LoadDefaultData()
        {
            var records = DataAccess.GetDefaultTxtFileData();

            return records;
        }

        private string ParseBoolToXmlString(bool value)
        {
            return value ? "yes" : "false";
        }
    }
}