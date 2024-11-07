using LabWork.Domain;
using LabWork.Domain.GraphElements;

namespace LabWork.Abstractions
{
    public interface IGraphBuilder
    {
        GraphInfo BuildPetriGraph(ScrollableControl layout, List<int> tokenSequence);

        void VisualizePetriGraph(GraphInfo graphInfo, ScrollableControl layout, Graphics graphics);

        void UpdateTokensPositionForPlace(List<Place> places);
    }
}
