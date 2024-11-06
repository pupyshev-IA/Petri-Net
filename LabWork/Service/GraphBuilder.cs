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
                var transitionPlaces = GetConnectedPlaces(unconnectedPlaces, random);
                var connectedPlaces = transitionPlaces.incoming.Concat(transitionPlaces.outgoing).ToList();
                var transitionPosition = CalculateTransitionPosition(connectedPlaces);
                var transition = CreateTransitionElement(index, transitionPosition, transitionPlaces.incoming, transitionPlaces.outgoing);

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
            {
                graphics.FillRectangle(transitionBrush, new Rectangle(transition.Сoordinates, transition.Metrics));

                Point markerPosition = new Point(transition.Сoordinates.X - 20, transition.Сoordinates.Y);
                graphics.DrawString("t" + transition.Id.ToString(), textFont, textBrush, markerPosition);
            }

            DrawConnectingLines(graphics, graphInfo.TransitionsInfo.Values);

            textBrush.Dispose();
            placePen.Dispose();
            tokenBrush.Dispose();
            transitionBrush.Dispose();
        }

        private void DrawCells(ScrollableControl layout, Graphics graphics)
        {
            Pen pen = new Pen(AppConstants.CellColor, AppConstants.CellThickness);

            for (int x = 0; x <= layout.Width; x += (int)AppConstants.PlaceWidth)
                graphics.DrawLine(pen, x, 0, x, layout.Height);

            for (int y = 0; y <= layout.Height; y += (int)AppConstants.PlaceHeight)
                graphics.DrawLine(pen, 0, y, layout.Width, y);

            pen.Dispose();
        }

        private void DrawConnectingLines(Graphics graphics, ICollection<Transition> transitions)
        {
            Pen linePen = new Pen(AppConstants.LineColor, AppConstants.LineThickness);

            foreach (var transition in transitions)
            {
                foreach (var place in transition.IncomingPlaces)
                {
                    var edgePoints = GetElementsEdgePoints(place, transition);

                    graphics.DrawLine(linePen, edgePoints.circle, edgePoints.rect);
                    DrawArrowHead(graphics, linePen, edgePoints.circle, edgePoints.rect);
                }

                foreach (var place in transition.OutgoingPlaces)
                {
                    var edgePoints = GetElementsEdgePoints(place, transition);

                    graphics.DrawLine(linePen, edgePoints.rect, edgePoints.circle);
                    DrawArrowHead(graphics, linePen, edgePoints.rect, edgePoints.circle);
                }
            }

            linePen.Dispose();
        }

        private void DrawArrowHead(Graphics graphics, Pen pen, Point start, Point end)
        {
            double angle = Math.Atan2(end.Y - start.Y, end.X - start.X);

            PointF arrowPoint1 = new PointF(
                end.X - AppConstants.ArrowHeadLength * (float)Math.Cos(angle - Math.PI / 180 * AppConstants.ArrowHeadAngle),
                end.Y - AppConstants.ArrowHeadLength * (float)Math.Sin(angle - Math.PI / 180 * AppConstants.ArrowHeadAngle));

            PointF arrowPoint2 = new PointF(
                end.X - AppConstants.ArrowHeadLength * (float)Math.Cos(angle + Math.PI / 180 * AppConstants.ArrowHeadAngle),
                end.Y - AppConstants.ArrowHeadLength * (float)Math.Sin(angle + Math.PI / 180 * AppConstants.ArrowHeadAngle));

            SolidBrush arrowHeadBrush = new SolidBrush(AppConstants.LineColor);
            graphics.FillPolygon(arrowHeadBrush, new[] { end, arrowPoint1, arrowPoint2 });

            arrowHeadBrush.Dispose();
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

        private Transition CreateTransitionElement(int id, Point coordinates, ICollection<Place> incomingPlaces, ICollection<Place> outgoingPlaces)
        {
            var transition = new Transition
            {
                Id = id,
                Сoordinates = coordinates,
                Metrics = new Size((int)AppConstants.TransitionWidth, (int)AppConstants.TransitionHeight),
                IncomingPlaces = incomingPlaces,
                OutgoingPlaces = outgoingPlaces
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

        private (ICollection<Place> incoming, ICollection<Place> outgoing) GetConnectedPlaces(List<Place> unconnectedPlaces, Random random)
        {
            var currentPlace = unconnectedPlaces.First();
            var incomingPlaces = new List<Place> { currentPlace };
            var outgoingPlaces = new List<Place>();

            int maxValue = (int)Math.Ceiling((double)unconnectedPlaces.Count / AppConstants.TransitionsMaxCount);
            int placesPerTransition = random.Next(1, Math.Min(maxValue, unconnectedPlaces.Count) + 1);

            outgoingPlaces.AddRange(unconnectedPlaces.Skip(1).Take(placesPerTransition));

            return (incomingPlaces, outgoingPlaces);
        }

        private void UpdateLastTransitionWithRemainingPlaces(GraphInfo graphInfo, List<Place> remainingPlaces)
        {
            var lastTransition = graphInfo.TransitionsInfo.Values.Last();
            var outgoingPlaces = lastTransition.OutgoingPlaces.ToList();

            outgoingPlaces.AddRange(remainingPlaces);
            var updatedPosition = CalculateTransitionPosition(outgoingPlaces.Concat(lastTransition.IncomingPlaces).ToList());

            lastTransition.OutgoingPlaces = outgoingPlaces;
            lastTransition.Сoordinates = updatedPosition;
        }

        private void AddNearestPlaceToRandomTransition(GraphInfo graphInfo, Random random)
        {
            var minConnections = graphInfo.TransitionsInfo.Values
                .Min(transition => transition.IncomingPlaces.Count + transition.OutgoingPlaces.Count);

            var transitionsWithMinConnections = graphInfo.TransitionsInfo.Values
                .Where(transition => transition.IncomingPlaces.Count + transition.OutgoingPlaces.Count == minConnections)
                .ToList();

            foreach (var _ in Enumerable.Range(1, (int)AppConstants.AdditionalConnectionsNumber))
            {
                if (transitionsWithMinConnections.Count == 0)
                    break;

                var randomTransition = transitionsWithMinConnections[random.Next(transitionsWithMinConnections.Count)];
                var nearestPlace = FindNearestUnconnectedPlace(randomTransition, graphInfo.PlacesInfo.Values.ToList());

                if (nearestPlace != null)
                {
                    randomTransition.IncomingPlaces.Add(nearestPlace);
                    randomTransition.OutgoingPlaces.Add(nearestPlace);
                    var updatedPosition = CalculateTransitionPosition(randomTransition.IncomingPlaces.Concat(randomTransition.OutgoingPlaces).ToList());
                    randomTransition.Сoordinates = updatedPosition;

                    transitionsWithMinConnections.Remove(randomTransition);
                }
            }
        }

        private Place FindNearestUnconnectedPlace(Transition transition, List<Place> allPlaces)
        {
            var connectedPlaces = transition.IncomingPlaces.Concat(transition.OutgoingPlaces).ToHashSet();

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

        private (Point circle, Point rect) GetElementsEdgePoints(Place place, Transition transition)
        {
            Point placeCenter = GetElementCenter(place);
            Point transitionCenter = GetElementCenter(transition);

            double angle = Math.Atan2(transitionCenter.Y - placeCenter.Y, transitionCenter.X - placeCenter.X);
            var radius = (int)AppConstants.PlaceWidth / 2;

            Rectangle rect = new Rectangle(transition.Сoordinates, transition.Metrics);

            Point edgePointCircle = GetEdgePointCircle(placeCenter, radius, angle);
            Point edgePointRect = GetEdgePointRect(rect, angle + Math.PI);

            return (edgePointCircle, edgePointRect);
        }

        private Point GetEdgePointCircle(Point center, int radius, double angle)
        {
            return new Point(
                (int)(center.X + radius * Math.Cos(angle)),
                (int)(center.Y + radius * Math.Sin(angle))
            );
        }

        private Point GetEdgePointRect(Rectangle rect, double angle)
        {
            double tanAngle = Math.Tan(angle);
            Point center = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);

            if (Math.Abs(tanAngle) < (double)rect.Height / rect.Width)
            {
                if (angle > -Math.PI / 2 && angle < Math.PI / 2)
                    return new Point(rect.Right, (int)(center.Y + (rect.Width / 2) * tanAngle));
                else
                    return new Point(rect.Left, (int)(center.Y - (rect.Width / 2) * tanAngle));
            }
            else
            {
                if (angle >= 0)
                    return new Point((int)(center.X + (rect.Height / 2) / tanAngle), rect.Bottom);
                else
                    return new Point((int)(center.X - (rect.Height / 2) / tanAngle), rect.Top);
            }
        }

        private Point ConvertIndexPositionToPoint(int rowIndex, int columnIndex) =>
            new Point((int)(columnIndex * AppConstants.PlaceHeight), (int)(rowIndex * AppConstants.PlaceWidth));

        private Point GetElementCenter(GraphElement element) =>
            new Point(element.Сoordinates.X + element.Metrics.Width / 2, element.Сoordinates.Y + element.Metrics.Height / 2);

        private double CalculateDistance(Point coords1, Point coords2) =>
            Math.Sqrt(Math.Pow(coords1.X - coords2.X, 2) + Math.Pow(coords1.Y - coords2.Y, 2));
    }
}
