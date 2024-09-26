namespace LabWork.Domain.GraphElements
{
    public abstract class GraphElement
    {
        public required int Id { get; set; }

        public Point Сoordinates { get; set; }

        public FigureParameters Parameters { get; set; }
    }
}
