using LabWork.Abstractions;
using LabWork.Domain;
using LabWork.Domain.GraphElements;

namespace LabWork.Service
{
    public class GraphBuilder : IGraphBuilder
    {
        public GraphInfo BuildPetriGraph(ScrollableControl layout, List<int> tokenSequence)
        {
            var graphInfo = new GraphInfo();
            var matrix = CreateMatrixFromRegion(layout);

            foreach (var index in Enumerable.Range(1, AppConstants.PlacesMaxCount))
            {
                var place = CreatePlaceElement(matrix, index, tokenSequence[index - 1]);
                graphInfo.PlacesInfo.Add(place.Id, place);
            }

            return graphInfo;
        }

        public void VisualizePetriGraph(GraphInfo graphInfo, ScrollableControl layout, Graphics graphics)
        {
            Pen penLine = new Pen(Color.Gray, 1);
            for (int x = 0; x <= layout.Width; x += (int)AppConstants.PlaceWidth)
                graphics.DrawLine(penLine, x, 0, x, layout.Height);
            for (int y = 0; y <= layout.Height; y += (int)AppConstants.PlaceHeight)
                graphics.DrawLine(penLine, 0, y, layout.Width, y);


            foreach (var place in graphInfo.PlacesInfo.Values)
            {
                Pen penPlace = new Pen(place.Parameters.BrushColor, place.Parameters.Thickness);
                graphics.DrawEllipse(penPlace, new Rectangle(place.Сoordinates, place.Parameters.ShapeMetrics));
            }
        }

        private Place CreatePlaceElement(Array matrix, int id, int tokenCount)
        {
            var place = new Place
            {
                Id = id,
                Сoordinates = GetRandomPositionInMatrix(matrix),
                Parameters = new FigureParameters
                {
                    ShapeMetrics = new Size((int)AppConstants.PlaceWidth, (int)AppConstants.PlaceHeight),
                    BrushColor = AppConstants.PlaceColor,
                    Thickness = AppConstants.PlaceThickness,
                }
            };

            return place;
        }

        private Array CreateMatrixFromRegion(ScrollableControl layout)
        {
            var columnCount = (int)Math.Floor((double)layout.Width / AppConstants.PlaceWidth);
            var rowCount = (int)Math.Floor((double)layout.Height / AppConstants.PlaceHeight);
            
            GraphElement[,] matrix = new GraphElement[rowCount, columnCount];
            
            return matrix;
        }

        private Point GetRandomPositionInMatrix(Array matrix)
        {
            Random random = new Random();

            int rowIndex = random.Next(0, matrix.GetLength(0));
            int columnIndex = random.Next(0, matrix.GetLength(1));
            
            return matrix.GetValue(rowIndex, columnIndex) is null
                ? ConvertIndexPositionToPoint(rowIndex, columnIndex)
                : GetRandomPositionInMatrix(matrix);
        }

        private Point ConvertIndexPositionToPoint(int rowIndex, int columnIndex) =>
            new Point((int)((columnIndex) * AppConstants.PlaceHeight), (int)((rowIndex) * AppConstants.PlaceWidth));
    }
}
