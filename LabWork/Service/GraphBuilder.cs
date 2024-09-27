using LabWork.Abstractions;
using LabWork.Domain;
using LabWork.Domain.GraphElements;

namespace LabWork.Service
{
    public class GraphBuilder : IGraphBuilder
    {
        public GraphInfo BuildPetriGraph(ScrollableControl layout, List<int> tokenSequence)
        {
            int tokenId = 1;
            var graphInfo = new GraphInfo();
            var occupancyMatrix = CreateOccupancyMatrix(layout);

            foreach (var index in Enumerable.Range(1, AppConstants.PlacesMaxCount))
            {
                var coordinates = GetRandomPositionInMatrix(occupancyMatrix);
                var place = CreatePlaceElement(index, coordinates);
                graphInfo.PlacesInfo.Add(place.Id, place);
            }
            foreach (var place in graphInfo.PlacesInfo.Values)
            {
                var tokenCount = tokenSequence[place.Id - 1];
                var tokens = CreateTokensForPlace(place, tokenCount, ref tokenId);

                foreach (var token in tokens)
                {
                    place.Tokens.Add(token);
                    graphInfo.TokensInfo.Add(token.Id, token);
                }
            }

            return graphInfo;
        }

        public void VisualizePetriGraph(GraphInfo graphInfo, ScrollableControl layout, Graphics graphics)
        {
            DrawCells(layout, graphics);

            Pen placePen = new Pen(AppConstants.PlaceColor, AppConstants.PlaceThickness);
            Font font = new Font(AppConstants.TextFontFamily, AppConstants.TextSize);
            SolidBrush textBrush = new SolidBrush(AppConstants.TextColor);
            foreach (var place in graphInfo.PlacesInfo.Values)
            {
                graphics.DrawEllipse(placePen, new Rectangle(place.Сoordinates, place.ShapeMetrics));

                Point markerPosition = new Point(place.Сoordinates.X + 5, place.Сoordinates.Y + 5);
                graphics.DrawString(place.Id.ToString(), font, textBrush, markerPosition);
            }

            SolidBrush tokenBrush = new SolidBrush(AppConstants.TokenColor);
            foreach (var token in graphInfo.TokensInfo.Values)
            {
                graphics.FillEllipse(tokenBrush, new Rectangle(token.Сoordinates, token.ShapeMetrics));
            }
        }

        private void DrawCells(ScrollableControl layout, Graphics graphics)
        {
            Pen pen = new Pen(AppConstants.CellColor, AppConstants.CellThickness);

            for (int x = 0; x <= layout.Width; x += (int)AppConstants.PlaceWidth)
                graphics.DrawLine(pen, x, 0, x, layout.Height);

            for (int y = 0; y <= layout.Height; y += (int)AppConstants.PlaceHeight)
                graphics.DrawLine(pen, 0, y, layout.Width, y);
        }

        private ICollection<Token> CreateTokensForPlace(Place place, int tokenCount, ref int tokenId)
        {
            var tokens = new List<Token>();

            if (tokenCount == 1)
            {
                int tokenX = (int)(place.Сoordinates.X + (place.ShapeMetrics.Width - AppConstants.TokenWidth) / 2);
                int tokenY = (int)(place.Сoordinates.Y + (place.ShapeMetrics.Height - AppConstants.TokenHeight) / 2);
                var coordinates = new Point(tokenX, tokenY);

                var token = CreateTokenElement(tokenId++, coordinates);
                tokens.Add(token);

                return tokens;
            }
            else
            {
                double angleStep = 360.0 / tokenCount;
                double angle = 0;

                for (int i = 0; i < tokenCount; i++)
                {
                    double radian = angle * (Math.PI / 180);
                    int smallCircleX = place.Сoordinates.X + (int)((place.ShapeMetrics.Width / 2.5) * Math.Cos(radian)) + (place.ShapeMetrics.Width / 2 - (int)AppConstants.TokenWidth / 2);
                    int smallCircleY = place.Сoordinates.Y + (int)((place.ShapeMetrics.Height / 2.5) * Math.Sin(radian)) + (place.ShapeMetrics.Height / 2 - (int)AppConstants.TokenHeight / 2);

                    var coordinates = new Point(smallCircleX, smallCircleY);
                    var token = CreateTokenElement(tokenId++, coordinates);
                    tokens.Add(token);

                    angle += angleStep;
                }

                return tokens;
            }
        }

        private Place CreatePlaceElement(int id, Point coordinates)
        {
            var place = new Place
            {
                Id = id,
                Сoordinates = coordinates,
                ShapeMetrics = new Size((int)AppConstants.PlaceWidth, (int)AppConstants.PlaceHeight)
            };

            return place;
        }

        private Token CreateTokenElement(int id, Point coordinates)
        {
            var token = new Token
            {
                Id = id,
                Сoordinates = coordinates,
                ShapeMetrics = new Size((int)AppConstants.TokenWidth, (int)AppConstants.TokenHeight)
            };

            return token;
        }

        private Array CreateOccupancyMatrix(ScrollableControl layout)
        {
            var colCount = (int)Math.Floor((double)layout.Width / AppConstants.PlaceWidth);
            var rowCount = (int)Math.Floor((double)layout.Height / AppConstants.PlaceHeight);

            bool[,] occupancyMatrix = new bool[rowCount, colCount];

            return occupancyMatrix;
        }

        private Point GetRandomPositionInMatrix(Array occupancyMatrix)
        {
            Random random = new Random();
            int rowIndex = random.Next(0, occupancyMatrix.GetLength(0));
            int colIndex = random.Next(0, occupancyMatrix.GetLength(1));

            if (occupancyMatrix.GetValue(rowIndex, colIndex) is true)
                return GetRandomPositionInMatrix(occupancyMatrix);

            if (AreNeighborCellsFree(occupancyMatrix, rowIndex, colIndex) is false)
                return GetRandomPositionInMatrix(occupancyMatrix);

            occupancyMatrix.SetValue(true, rowIndex, colIndex);
            return ConvertIndexPositionToPoint(rowIndex, colIndex);
        }

        private bool AreNeighborCellsFree(Array occupancyMatrix, int rowIndex, int colIndex)
        {
            int rowsCount = occupancyMatrix.GetLength(0);
            int colsCount = occupancyMatrix.GetLength(1);

            for (int i = -(int)AppConstants.CellGap; i <= (int)AppConstants.CellGap; i++)
            {
                for (int j = -(int)AppConstants.CellGap; j <= (int)AppConstants.CellGap; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int neighborRow = rowIndex + i;
                    int neighborCol = colIndex + j;

                    if (neighborRow >= 0 && neighborRow < rowsCount && neighborCol >= 0 && neighborCol < colsCount)
                    {
                        if (occupancyMatrix.GetValue(neighborRow, neighborCol) is true)
                            return false;
                    }
                }
            }

            return true;
        }

        private Point ConvertIndexPositionToPoint(int rowIndex, int columnIndex) =>
            new Point((int)((columnIndex) * AppConstants.PlaceHeight), (int)((rowIndex) * AppConstants.PlaceWidth));
    }
}
