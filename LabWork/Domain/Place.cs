namespace LabWork.Domain
{
    public class Place
    {
        public required int Id { get; set; }

        public Position Сoordinates { get; set; }

        public FigureParameters Parameters { get; set; }

        public IEnumerable<Token> Tokens { get; set; } = new List<Token>();
    }
}
