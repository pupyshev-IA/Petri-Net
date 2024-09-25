namespace LabWork.Domain.GraphElements
{
    public class Token
    {
        public required int Id { get; set; }

        public Point Сoordinates { get; set; }

        public FigureParameters Parameters { get; set; }
    }
}
