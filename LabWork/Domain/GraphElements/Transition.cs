namespace LabWork.Domain.GraphElements
{
    public class Transition : GraphElement
    {
        public ICollection<Place> IncomingPlaces { get; set; } = new List<Place>();

        public ICollection<Place> OutgoingPlaces { get; set; } = new List<Place>();
    }
}
