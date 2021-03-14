using System.Text;
using Caliburn.Micro;
using IntegracjaSystemowProjekt.WPF.Models;

namespace IntegracjaSystemowProjekt.WPF.Helpers
{
    public static class FileHelper
    {
        public static string BuildFileContent(BindableCollection<RecordModel> records)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var recordModel in records)
            {
                var line = BuildLine(recordModel);

                stringBuilder.AppendLine(line);
            }

            return stringBuilder.ToString();
        }

        private static string BuildLine(RecordModel recordModel)
        {
            return
                $"{recordModel.ManufacturerName};{recordModel.ScreenDiagonal};{recordModel.Resolution};{recordModel.ScreenSurfaceType};{recordModel.IsTouchable};" +
                $"{recordModel.ProcessorName};{recordModel.NumberOfPhysicalCores};{recordModel.Frequency};{recordModel.Ram};{recordModel.DiskSize};" +
                $"{recordModel.DiskType};{recordModel.Gpu};{recordModel.Vram};{recordModel.Os};{recordModel.Drive};";
        }
    }
}