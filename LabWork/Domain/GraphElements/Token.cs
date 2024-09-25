namespace LabWork.Domain.GraphElements
{
    public class Token
    {
        public required int Id { get; set; }

        public Position Сoordinates { get; set; }

        public FigureParameters Parameters { get; set; }
    }
}
