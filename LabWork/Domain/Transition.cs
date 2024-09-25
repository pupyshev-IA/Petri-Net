namespace LabWork.Domain
{
    public class Transition
    {
        public required int Id { get; set; }

        public Position Сoordinates { get; set; }

        public FigureParameters Parameters { get; set; }
    }
}
