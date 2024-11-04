namespace LabWork.Domain.GraphElements
{
    public class Transition : GraphElement
    {
        public ICollection<Place> ConnectedPlaces { get; set; } = new List<Place>();
    }
}
