using LabWork.Domain;

namespace LabWork.Abstractions
{
    public interface IGraphBuilder
    {
        GraphInfo BuildPetriGraph(ScrollableControl layout, List<int> tokenSequence);

        void VisualizePetriGraph(GraphInfo graphInfo, ScrollableControl layout, Graphics graphics);
    }
}
