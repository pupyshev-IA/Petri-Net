using LabWork.Domain.GraphElements;

namespace LabWork.Domain
{
    public class GraphInfo
    {
        public IDictionary<int, Place> PlacesInfo { get; set; } = new Dictionary<int, Place>();

        public IDictionary<int, Transition> TransitionsInfo { get; set; } = new Dictionary<int, Transition>();

        public IDictionary<int, Token> TokensInfo { get; set; } = new Dictionary<int, Token>();
    }
}
