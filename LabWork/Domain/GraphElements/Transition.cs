namespace LabWork.Domain.GraphElements
{
    public class Transition : GraphElement
    {
        public IDictionary<int, Place> Connections { get; set; } = new Dictionary<int, Place>();
    }
}
