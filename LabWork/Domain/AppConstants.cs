namespace LabWork.Domain
{
    public static class AppConstants
    {
        public static ushort PlacesMaxCount = 7;

        public static ushort TransitionsMaxCount = 5;

        public static ushort TokensMaxCount = 3;

        public static ushort NumberOfFirings = 7;


        // Place parameters
        public static uint PlaceWidth = 30;
        public static uint PlaceHeight = 30;
        public static uint PlaceThickness = 4;
        public static Color PlaceColor = Color.Black;

        // Transition parameters
        public static uint TransitionWidth = 10;
        public static uint TransitionHeight = 50;
        public static Color TransitionColor = Color.Black;

        // Token parameters
        public static uint TokenWidth = 10;
        public static uint TokenHeight = 10;
        public static Color TokenColor = Color.Black;

        // Line parameters
        public static uint LineThickness = 2;
        public static Color LineColor = Color.Black;
    }
}
