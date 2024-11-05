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
            var random = new Random();
            var places = new Dictionary<int, Place>();
            var graphInfo = new GraphInfo();

            var occupancyMatrix = CreateOccupancyMatrix(layout);

            foreach (var index in Enumerable.Range(1, AppConstants.PlacesMaxCount))
            {
                var coordinates = GetRandomPositionInMatrix(occupancyMatrix);
                var place = CreatePlaceElement(index, coordinates);
                places.Add(place.Id, place);
            }
            graphInfo.PlacesInfo = ArrangePlacesBySwapping(places);

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

            var unconnectedPlaces = graphInfo.PlacesInfo.Values.OrderBy(place => place.Сoordinates.X).ToList();
            foreach (var index in Enumerable.Range(1, AppConstants.TransitionsMaxCount))
            {
                var connectedPlaces = GetConnectedPlaces(unconnectedPlaces, random);
                var transitionPosition = CalculateTransitionPosition(connectedPlaces.ToList());
                var transition = CreateTransitionElement(index, transitionPosition, connectedPlaces);

                unconnectedPlaces.RemoveRange(0, connectedPlaces.Count - 1);
                graphInfo.TransitionsInfo.Add(transition.Id, transition);
            }

            if (unconnectedPlaces.Count > 1)
                UpdateLastTransitionWithRemainingPlaces(graphInfo, unconnectedPlaces);

            AddNearestPlaceToRandomTransition(graphInfo, random);

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

            DrawConnectingLines(graphics, graphInfo.TransitionsInfo.Values);
        }

        private void DrawCells(ScrollableControl layout, Graphics graphics)
        {
            Pen pen = new Pen(AppConstants.CellColor, AppConstants.CellThickness);

            for (int x = 0; x <= layout.Width; x += (int)AppConstants.PlaceWidth)
                graphics.DrawLine(pen, x, 0, x, layout.Height);

            for (int y = 0; y <= layout.Height; y += (int)AppConstants.PlaceHeight)
                graphics.DrawLine(pen, 0, y, layout.Width, y);
        }

        private void DrawConnectingLines(Graphics graphics, ICollection<Transition> transitions)
        {
            Pen linePen = new Pen(AppConstants.LineColor, AppConstants.LineThickness);

            foreach (var transition in transitions)
            {
                Point transitionCenter = new Point(
                    transition.Сoordinates.X + transition.Metrics.Width / 2,
                    transition.Сoordinates.Y + transition.Metrics.Height / 2
                );

                foreach (var place in transition.ConnectedPlaces)
                {
                    Point placeCenter = new Point(
                        place.Сoordinates.X + place.Metrics.Width / 2,
                        place.Сoordinates.Y + place.Metrics.Height / 2
                    );

                    graphics.DrawLine(linePen, placeCenter, transitionCenter);
                }
            }

            linePen.Dispose();
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

        private Transition CreateTransitionElement(int id, Point coordinates, ICollection<Place> connectedPlaces)
        {
            var transition = new Transition
            {
                Id = id,
                Сoordinates = coordinates,
                Metrics = new Size((int)AppConstants.TransitionWidth, (int)AppConstants.TransitionHeight),
                ConnectedPlaces = connectedPlaces
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

        private ICollection<Place> GetConnectedPlaces(List<Place> unconnectedPlaces, Random random)
        {
            var currentPlace = unconnectedPlaces.First();
            var connectedPlaces = new List<Place> { currentPlace };

            int maxValue = (int)Math.Ceiling((double)unconnectedPlaces.Count / AppConstants.TransitionsMaxCount);
            int placesPerTransition = random.Next(1, Math.Min(maxValue, unconnectedPlaces.Count) + 1);

            connectedPlaces.AddRange(unconnectedPlaces.Skip(1).Take(placesPerTransition));

            return connectedPlaces;
        }

        private void UpdateLastTransitionWithRemainingPlaces(GraphInfo graphInfo, List<Place> remainingPlaces)
        {
            var lastTransition = graphInfo.TransitionsInfo.Values.Last();
            var updatedConnectedPlaces = lastTransition.ConnectedPlaces.ToList();

            updatedConnectedPlaces.AddRange(remainingPlaces);
            var updatedPosition = CalculateTransitionPosition(updatedConnectedPlaces);

            lastTransition.ConnectedPlaces = updatedConnectedPlaces;
            lastTransition.Сoordinates = updatedPosition;
        }

        private void AddNearestPlaceToRandomTransition(GraphInfo graphInfo, Random random)
        {
            var minConnections = graphInfo.TransitionsInfo.Values
                .Min(transition => transition.ConnectedPlaces.Count);

            var transitionsWithMinConnections = graphInfo.TransitionsInfo.Values
                .Where(transition => transition.ConnectedPlaces.Count == minConnections)
                .ToList();

            foreach (var _ in Enumerable.Range(1, (int)AppConstants.AdditionalConnectionsNumber))
            {
                if (transitionsWithMinConnections.Count == 0)
                    break;

                var randomTransition = transitionsWithMinConnections[random.Next(transitionsWithMinConnections.Count)];
                var nearestPlace = FindNearestUnconnectedPlace(randomTransition, graphInfo.PlacesInfo.Values.ToList());

                if (nearestPlace != null)
                {
                    randomTransition.ConnectedPlaces.Add(nearestPlace);
                    var updatedPosition = CalculateTransitionPosition(randomTransition.ConnectedPlaces.ToList());
                    randomTransition.Сoordinates = updatedPosition;

                    transitionsWithMinConnections.Remove(randomTransition);
                }
            }
        }

        private Place FindNearestUnconnectedPlace(Transition transition, List<Place> allPlaces)
        {
            var connectedPlaces = transition.ConnectedPlaces.ToHashSet();

            Place nearestPlace = null;
            double minDistance = double.MaxValue;

            foreach (var place in allPlaces)
            {
                if (!connectedPlaces.Contains(place))
                {
                    double distance = CalculateDistance(transition.Сoordinates, place.Сoordinates);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestPlace = place;
                    }
                }
            }

            return nearestPlace;
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

        private IDictionary<int, Place> ArrangePlacesBySwapping(Dictionary<int, Place> places)
        {
            var coordinates = places.Values
                .OrderBy(place => place.Сoordinates.X)
                .Select(place => place.Сoordinates)
                .ToList();

            foreach (var index in Enumerable.Range(1, coordinates.Count))
                places[index].Сoordinates = coordinates[index - 1];

            return places;
        }

        private Point CalculateTransitionPosition(List<Place> connectedPlaces)
        {
            int avgX = (int)connectedPlaces.Average(place => place.Сoordinates.X);
            int avgY = (int)connectedPlaces.Average(place => place.Сoordinates.Y);
            return new Point(avgX, avgY);
        }

        private Point ConvertIndexPositionToPoint(int rowIndex, int columnIndex) =>
            new Point((int)(columnIndex * AppConstants.PlaceHeight), (int)(rowIndex * AppConstants.PlaceWidth));

        private double CalculateDistance(Point coords1, Point coords2) =>
            Math.Sqrt(Math.Pow(coords1.X - coords2.X, 2) + Math.Pow(coords1.Y - coords2.Y, 2));
    }
}
