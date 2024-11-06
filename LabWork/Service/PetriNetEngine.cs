using LabWork.Domain.GraphElements;
using LabWork.Domain;

namespace LabWork.Service
{
    public class PetriNetEngine
    {
        private GraphInfo _graphInfo;

        public PetriNetEngine(GraphInfo graphInfo)
        {
            _graphInfo = graphInfo;
        }
    }
}
