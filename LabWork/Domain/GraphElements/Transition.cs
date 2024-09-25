namespace LabWork.Domain.GraphElements
{
    public class Transition
    {
        public required int Id { get; set; }

        public Point Сoordinates { get; set; }

        public FigureParameters Parameters { get; set; }

        public Dictionary<int, Place> Connections { get; set; } = new Dictionary<int, Place>();
    }
}
