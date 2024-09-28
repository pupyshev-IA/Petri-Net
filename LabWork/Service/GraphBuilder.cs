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

            Font textFont = new Font(AppConstants.TextFontFamily, AppConstants.TextSize);
            SolidBrush textBrush = new SolidBrush(AppConstants.TextColor);
            Pen placePen = new Pen(AppConstants.PlaceColor, AppConstants.PlaceThickness);
            SolidBrush tokenBrush = new SolidBrush(AppConstants.TokenColor);
            SolidBrush transitionBrush = new SolidBrush(AppConstants.TransitionColor);

            foreach (var place in graphInfo.PlacesInfo.Values)
            {
                graphics.DrawEllipse(placePen, new Rectangle(place.Сoordinates, place.Metrics));

                Point markerPosition = new Point(place.Сoordinates.X + 5, place.Сoordinates.Y + 5);
                graphics.DrawString(place.Id.ToString(), textFont, textBrush, markerPosition);
            }

            foreach (var token in graphInfo.TokensInfo.Values)
                graphics.FillEllipse(tokenBrush, new Rectangle(token.Сoordinates, token.Metrics));

            foreach (var transition in graphInfo.TransitionsInfo.Values)
                graphics.FillRectangle(transitionBrush, new Rectangle(transition.Сoordinates, transition.Metrics));
        }

        private void DrawCells(ScrollableControl layout, Graphics graphics)
        {
            Pen pen = new Pen(AppConstants.CellColor, AppConstants.CellThickness);

            for (int x = 0; x <= layout.Width; x += (int)AppConstants.PlaceWidth)
                graphics.DrawLine(pen, x, 0, x, layout.Height);

            for (int y = 0; y <= layout.Height; y += (int)AppConstants.PlaceHeight)
                graphics.DrawLine(pen, 0, y, layout.Width, y);
        }

        private Place CreatePlaceElement(int id, Point coordinates)
        {
            var place = new Place
            {
                Id = id,
                Сoordinates = coordinates,
                Metrics = new Size((int)AppConstants.PlaceWidth, (int)AppConstants.PlaceHeight)
            };

            return place;
        }

        private Token CreateTokenElement(int id, Point coordinates)
        {
            var token = new Token
            {
                Id = id,
                Сoordinates = coordinates,
                Metrics = new Size((int)AppConstants.TokenWidth, (int)AppConstants.TokenHeight)
            };

            return token;
        }

        private Transition CreateTransitionElement(int id, Point coordinates)
        {
            var transition = new Transition
            {
                Id = id,
                Сoordinates = coordinates,
                Metrics = new Size((int)AppConstants.TransitionWidth, (int)AppConstants.TransitionHeight)
            };

            return transition;
        }

        private ICollection<Token> CreateTokensForPlace(Place place, int tokenCount, ref int tokenId)
        {
            var tokens = new List<Token>();

            for (int tokenNum = 0; tokenNum < tokenCount; tokenNum++)
            {
                var coordinates = GetTokenPosition(place, tokenCount, tokenNum);
                var token = CreateTokenElement(tokenId++, coordinates);
                tokens.Add(token);
            }

            return tokens;
        }

        private Point GetTokenPosition(Place place, int tokenCount, int tokenNum)
        {
            int parentCenterX = place.Сoordinates.X + place.Metrics.Width / 2;
            int parentCenterY = place.Сoordinates.Y + place.Metrics.Height / 2;

            if (tokenCount == 1)
            {
                int tokenX = (int)(parentCenterX - AppConstants.TokenWidth / 2);
                int tokenY = (int)(parentCenterY - AppConstants.TokenHeight / 2);

                return new Point(tokenX, tokenY);
            }

            double angle = 0;
            double angleStep = 360.0 / tokenCount;
            angle = angleStep * tokenNum;

            double radian = angle * (Math.PI / 180);
            int distanceFromCenter = (place.Metrics.Width - (int)AppConstants.TokenWidth) / 3; // Determines the distance of Tokens from Place. Divider - number for regulation.

            int x = parentCenterX + (int)(distanceFromCenter * Math.Cos(radian)) - (int)AppConstants.TokenWidth / 2;
            int y = parentCenterY + (int)(distanceFromCenter * Math.Sin(radian)) - (int)AppConstants.TokenHeight / 2;

            return new Point(x, y);
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
