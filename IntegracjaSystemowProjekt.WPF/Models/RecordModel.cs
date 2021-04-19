using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using IntegracjaSystemowProjekt.WPF.Annotations;
using IntegracjaSystemowProjekt.WPF.ViewModels;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace IntegracjaSystemowProjekt.WPF.Models
{
    public class RecordModel : INotifyPropertyChanged
    {
        private string _recordColor = "Gray";
        public string RecordColor
        {
            get => _recordColor;
            set => _recordColor = value;
        }

        public RecordState RecordState { get; set; }

        private string _manufacturerName;
        private string _screenDiagonal;
        private string _resolution;
        private string _screenSurfaceType;
        private bool _isTouchable;
        private string _processorName;
        private int? _numberOfPhysicalCores;
        private int? _frequency;
        private string _ram;
        private string _diskSize;
        private string _diskType;
        private string _gpu;
        private string _vram;
        private string _os;
        private string _drive;
        

        public string ManufacturerName
        {
            get => _manufacturerName;
            set
            {
                _manufacturerName = value;
                ProcessEdit();
            }
        }

        public string ScreenDiagonal
        {
            get => _screenDiagonal;
            set
            {
                _screenDiagonal = value;
                ProcessEdit();
            }
        }

        public string Resolution
        {
            get => _resolution;
            set
            {
                _resolution = value;
                ProcessEdit();
            }
        }

        public string ScreenSurfaceType
        {
            get => _screenSurfaceType;
            set
            {
                _screenSurfaceType = value;
                ProcessEdit();
            }
        }

        public bool IsTouchable
        {
            get => _isTouchable;
            set
            {
                _isTouchable = value;
                ProcessEdit();
            }
        }

        public string ProcessorName
        {
            get => _processorName;
            set
            {
                _processorName = value;
                ProcessEdit();
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Podaj liczbę większą od zera.")]
        public int? NumberOfPhysicalCores
        {
            get => _numberOfPhysicalCores;
            set
            {
                ValidateProperty(value, "NumberOfPhysicalCores");
                _numberOfPhysicalCores = value;
                ProcessEdit();
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Podaj liczbę większą od zera.")]
        public int? Frequency
        {
            get => _frequency;
            set
            {
                ValidateProperty(value, "Frequency");
                _frequency = value;
                ProcessEdit();
            }
        }

        public string Ram
        {
            get => _ram;
            set
            {
                _ram = value;
                ProcessEdit();
            }
        }

        public string DiskSize
        {
            get => _diskSize;
            set
            {
                _diskSize = value;
                ProcessEdit();
            }
        }

        public string DiskType
        {
            get => _diskType;
            set
            {
                _diskType = value;
                ProcessEdit();
            }
        }

        public string Gpu
        {
            get => _gpu;
            set
            {
                _gpu = value;
                ProcessEdit();
            }
        }

        public string Vram
        {
            get => _vram;
            set
            {
                _vram = value;
                ProcessEdit();
            }
        }

        public string Os
        {
            get => _os;
            set
            {
                _os = value;
                ProcessEdit();
            }
        }

        public string Drive
        {
            get => _drive;
            set
            {
                _drive = value;
                ProcessEdit();
            }
        }

        private void ValidateProperty<T>(T value, string name)
        {
            try
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                {
                    MemberName = name
                });

                if (ShellViewModel.Errors.TryGetValue(this, out var output))
                {
                    if (output > 0)
                        ShellViewModel.Errors[this]--;
                }
            }
            catch (ValidationException e)
            {
                if (!ShellViewModel.Errors.TryGetValue(this, out _))
                    ShellViewModel.Errors.Add(this, 1);
                else
                {
                    ShellViewModel.Errors[this]++;
                }

                Console.WriteLine(e);
                throw;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ProcessEdit()
        {
            if (!ShellViewModel.IsInLoadingState)
            {
                RecordColor = ColorTemplates.ModifiedRow;
                RecordState = RecordState.Modified;

                RaisePropertyChanged("RecordColor");
            }
        }
    }
}