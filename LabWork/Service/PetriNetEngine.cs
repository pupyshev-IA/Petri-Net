using LabWork.Abstractions;
using LabWork.Domain;
using LabWork.Domain.GraphElements;

namespace LabWork.Service
{
    public static class PetriNetEngine
    {
        public static List<GraphInfo> Simulate(GraphInfo graphInfo, IGraphBuilder graphBuilder)
        {
            var random = new Random();
            var stages = new List<GraphInfo>() { graphInfo };

            foreach (var _ in Enumerable.Range(1, AppConstants.NumberOfFirings))
            {
                var currentStage = DeepCopyGraphInfo(stages.Last());
                var activeTransitions = GetActiveTransitions(currentStage.TransitionsInfo.Values.ToList());

                if (!activeTransitions.Any())
                {
                    int count = stages.Count;
                    for (int i = 0; i <= AppConstants.NumberOfFirings - count; i++)
                        stages.Add(currentStage);

                    break;
                }

                var randomTransition = activeTransitions[random.Next(activeTransitions.Count)];
                Fire(randomTransition, random, graphBuilder);

                stages.Add(currentStage);
            }

            return stages;
        }

        private static void Fire(Transition transition, Random random, IGraphBuilder graphBuilder)
        {
            var incomingPlaces = transition.IncomingPlaces.ToList();
            var outgoingPlaces = transition.OutgoingPlaces.ToList();

            var placeTo = outgoingPlaces[random.Next(outgoingPlaces.Count)];
            var placeFrom = incomingPlaces[random.Next(incomingPlaces.Count)];

            var randomToken = placeFrom.Tokens.ElementAt(random.Next(placeFrom.Tokens.Count - 1));

            placeTo.Tokens.Add(randomToken);
            placeFrom.Tokens.Remove(randomToken);

            transition.IncomingPlaces = incomingPlaces;
            transition.OutgoingPlaces = outgoingPlaces;

            graphBuilder.UpdateTokensPositionForPlace(new List<Place> { placeFrom, placeTo });
        }

        private static List<Transition> GetActiveTransitions(List<Transition> transitions) =>
            transitions.Where(transition => transition.IncomingPlaces.All(place => place.Tokens.Any())).ToList();

        public static GraphInfo DeepCopyGraphInfo(GraphInfo original)
        {
            var copy = new GraphInfo();

            var placeCache = new Dictionary<int, Place>();
            var transitionCache = new Dictionary<int, Transition>();
            var tokenCache = new Dictionary<int, Token>();

            foreach (var kvp in original.PlacesInfo)
                copy.PlacesInfo[kvp.Key] = DeepCopyPlace(kvp.Value, placeCache, tokenCache);

            foreach (var kvp in original.TransitionsInfo)
                copy.TransitionsInfo[kvp.Key] = DeepCopyTransition(kvp.Value, placeCache, transitionCache, tokenCache);

            foreach (var kvp in original.TokensInfo)
                copy.TokensInfo[kvp.Key] = DeepCopyToken(kvp.Value, tokenCache);

            return copy;
        }

        private static Place DeepCopyPlace(Place original, Dictionary<int, Place> placeCache, Dictionary<int, Token> tokenCache)
        {
            if (placeCache.TryGetValue(original.Id, out var existingPlace))
                return existingPlace;

            var newPlace = new Place
            {
                Id = original.Id,
                Сoordinates = original.Сoordinates,
                Metrics = original.Metrics,
                Tokens = original.Tokens.Select(token => DeepCopyToken(token, tokenCache)).ToList()
            };

            placeCache[original.Id] = newPlace;
            return newPlace;
        }

        private static Transition DeepCopyTransition(Transition original, Dictionary<int, Place> placeCache, Dictionary<int, Transition> transitionCache, Dictionary<int, Token> tokenCache)
        {
            if (transitionCache.TryGetValue(original.Id, out var existingTransition))
                return existingTransition;

            var newTransition = new Transition
            {
                Id = original.Id,
                Сoordinates = original.Сoordinates,
                Metrics = original.Metrics,
                IncomingPlaces = original.IncomingPlaces.Select(place => DeepCopyPlace(place, placeCache, tokenCache)).ToList(),
                OutgoingPlaces = original.OutgoingPlaces.Select(place => DeepCopyPlace(place, placeCache, tokenCache)).ToList()
            };

            transitionCache[original.Id] = newTransition;
            return newTransition;
        }

        private static Token DeepCopyToken(Token original, Dictionary<int, Token> tokenCache)
        {
            if (tokenCache.TryGetValue(original.Id, out var existingToken))
                return existingToken;

            var newToken = new Token
            {
                Id = original.Id,
                Сoordinates = original.Сoordinates,
                Metrics = original.Metrics
            };

            tokenCache[original.Id] = newToken;
            return newToken;
        }
    }
}
