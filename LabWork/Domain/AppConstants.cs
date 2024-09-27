namespace LabWork.Domain
{
    public static class AppConstants
    {
        public const ushort PlacesMaxCount = 7;
        public const ushort TransitionsMaxCount = 5;
        public const ushort TokensMaxCountPerPlace = 3;
        public const ushort NumberOfFirings = 7;


        // Place parameters
        public const uint PlaceWidth = 50;
        public const uint PlaceHeight = 50;
        public const uint PlaceThickness = 3;
        public static Color PlaceColor = Color.Black;
        public const string TextFontFamily = "Arial";
        public const uint TextSize = 12;
        public static Color TextColor = Color.Black;

        // Transition parameters
        public const uint TransitionWidth = 10;
        public const uint TransitionHeight = 50;
        public static Color TransitionColor = Color.Black;

        // Token parameters
        public const uint TokenWidth = 15;
        public const uint TokenHeight = 15;
        public static Color TokenColor = Color.Black;

        // Line parameters
        public const uint LineThickness = 2;
        public static Color LineColor = Color.Black;

        // Graph parameters
        public const uint CellThickness = 1;
        public static Color CellColor = Color.Gray;
        public const uint CellGap = 2;
    }
}
