namespace LabWork.Domain.GraphElements
{
    public abstract class GraphElement
    {
        public required int Id { get; set; }

        public required Point Сoordinates { get; set; }

        public required Size ShapeMetrics { get; set; }
    }
}
