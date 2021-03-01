namespace IntegracjaSystemowProjekt.Models
{
    public static class ColumnNames
    {
        public static string ManufacturerNameColumnName { get; set; } = "nazwa producenta";
        public static string ScreenDiagonalColumnName { get; set; } = "przekątna ekranu";
        public static string ResolutionColumnName { get; set; } = "Rozdzielczość";
        public static string ScreenSurfaceTypeColumnName { get; set; } = "rodzaj powierzchni ekranu";
        public static string IsTouchableColumnName { get; set; } = "czy ekran jest dotykowy";
        public static string ProcessorNameColumnName { get; set; } = "nazwa procesora";
        public static string NumberOfPhysicalCoresColumnName { get; set; } = "liczba rdzeni fizycznych";
        public static string FrequencyColumnName { get; set; } = "prędkość taktowania MHz";
        public static string RamColumnName { get; set; } = "wielkość pamięci RAM";
        public static string DiskSizeColumnName { get; set; } = "pojemność dysku";
        public static string DiskTypeColumnName { get; set; } = "rodzaj dysku";
        public static string GpuColumnName { get; set; } = "nazwa układu graficznego";
        public static string VramColumnName { get; set; } = "pamięć układu graficznego";
        public static string OsColumnName { get; set; } = "nazwa systemu operacyjnego";
        public static string DriveColumnName { get; set; } = "rodzaj napędu fizycznego w komputerze";
    }
}