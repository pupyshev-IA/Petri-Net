namespace LabWork.Domain.GraphElements
{
    public class Transition : GraphElement
    {
        public Dictionary<int, Place> Connections { get; set; } = new Dictionary<int, Place>();
    }
}
