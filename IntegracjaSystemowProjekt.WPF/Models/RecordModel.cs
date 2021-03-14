using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IntegracjaSystemowProjekt.WPF.ViewModels;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace IntegracjaSystemowProjekt.WPF.Models
{
    public class RecordModel
    {
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
            set => _manufacturerName = value;
        }

        public string ScreenDiagonal
        {
            get => _screenDiagonal;
            set => _screenDiagonal = value;
        }

        public string Resolution
        {
            get => _resolution;
            set => _resolution = value;
        }

        public string ScreenSurfaceType
        {
            get => _screenSurfaceType;
            set => _screenSurfaceType = value;
        }

        public bool IsTouchable
        {
            get => _isTouchable;
            set => _isTouchable = value;
        }

        public string ProcessorName
        {
            get => _processorName;
            set => _processorName = value;
        }

        [Range(1, int.MaxValue, ErrorMessage = "Podaj liczbę większą od zera.")]
        public int? NumberOfPhysicalCores
        {
            get => _numberOfPhysicalCores;
            set
            {
                ValidateProperty(value, "NumberOfPhysicalCores");
                _numberOfPhysicalCores = value;
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
            }
        }

        public string Ram
        {
            get => _ram;
            set => _ram = value;
        }

        public string DiskSize
        {
            get => _diskSize;
            set => _diskSize = value;
        }

        public string DiskType
        {
            get => _diskType;
            set => _diskType = value;
        }

        public string Gpu
        {
            get => _gpu;
            set => _gpu = value;
        }

        public string Vram
        {
            get => _vram;
            set => _vram = value;
        }

        public string Os
        {
            get => _os;
            set => _os = value;
        }

        public string Drive
        {
            get => _drive;
            set => _drive = value;
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
    }
}