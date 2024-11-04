namespace LabWork.Domain.GraphElements
{
    public class Place : GraphElement
    {
        public ICollection<Token> Tokens { get; set; } = new List<Token>();
    }
}
