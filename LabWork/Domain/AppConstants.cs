namespace LabWork.Domain
{
    public static class AppConstants
    {
        public static ushort PlacesMaxCount = 7;

        public static ushort TransitionsMaxCount = 5;

        public static ushort TokensMaxCount = 3;

        public static ushort NumberOfFirings = 7;


        // Place parameters
        public static uint PlaceWidth = 50;
        public static uint PlaceHeight = 50;
        public static uint PlaceThickness = 5;
        public static Color PlaceColor = Color.Black;

        // Transition parameters
        public static uint TransitionWidth = 10;
        public static uint TransitionHeight = 80;
        public static Color TransitionColor = Color.Black;

        // Token parameters
        public static uint TokenWidth = 15;
        public static uint TokenHeight = 15;
        public static Color TokenColor = Color.Black;
    }
}
