using LabWork.Domain.GraphElements;

namespace LabWork.Domain
{
    public class GraphInfo
    {
        public IDictionary<int, Place> PlacesInfo { get; set; }

        public IDictionary<int, Transition> TransitionsInfo { get; set; }

        public IDictionary<int, Token> TokensInfo { get; set; }
    }
}
