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
using Screen = Caliburn.Micro.Screen;
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

        public void OpenTxtFileDialog()
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

                    var records = DataAccess.GetTxtFileDataByPath(path);

                    Records.Clear();
                    Records.AddRange(ValueHelper.MapValues(records));

                    MessageBox.Show("Wskazany plik został wczytany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
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

                    Records.Clear();
                    Records.AddRange(ValueHelper.MapValues(records));

                    MessageBox.Show("Wskazany plik został wczytany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
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

        private IEnumerable<RecordModel> LoadDefaultData()
        {
            var records = DataAccess.GetDefaultTxtFileData();

            var recordModels = ValueHelper.MapValues(records);

            return recordModels;
        }

        private string ParseBoolToXmlString(bool value)
        {
            return value ? "yes" : "false";
        }
    }
}