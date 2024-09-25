namespace LabWork.Domain
{
    public static class AppConstants
    {
        public const ushort PlacesMaxCount = 7;

        public const ushort TransitionsMaxCount = 5;

        public const ushort TokensMaxCount = 3;

        public const ushort NumberOfFirings = 7;


        // Place parameters
        public const uint PlaceWidth = 30;
        public const uint PlaceHeight = 30;
        public const uint PlaceThickness = 4;
        public static Color PlaceColor = Color.Black;

        // Transition parameters
        public const uint TransitionWidth = 10;
        public const uint TransitionHeight = 50;
        public static Color TransitionColor = Color.Black;

        // Token parameters
        public const uint TokenWidth = 10;
        public const uint TokenHeight = 10;
        public static Color TokenColor = Color.Black;

        // Line parameters
        public const uint LineThickness = 2;
        public static Color LineColor = Color.Black;
    }
}
