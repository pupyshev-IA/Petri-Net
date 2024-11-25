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

            PetriNetStateLogger.InitializeNewBuilder();
            PetriNetStateLogger.AddInfo($"[-----ЭТАП {0}-----]");
            PetriNetStateLogger.AddInfo(">>Текущее положение меток<<");
            foreach (var place in stages[0].PlacesInfo.Values)
                PetriNetStateLogger.AddInfo($"\tМесто {place.Id}  >  Кол-во Меток: {place.Tokens.Count}");

            foreach (var index in Enumerable.Range(1, AppConstants.NumberOfFirings))
            {
                var currentStage = DeepCopyGraphInfo(stages.Last());

                PetriNetStateLogger.AddInfo("");
                PetriNetStateLogger.AddInfo($"[-----ЭТАП {index}-----]");
                PetriNetStateLogger.AddInfo(">>Перемещение меток<<");

                var activeTransitions = GetActiveTransitions(currentStage.TransitionsInfo.Values.ToList(), random);

                if (!activeTransitions.Any())
                {
                    PetriNetStateLogger.AddInfo("-ТУПИК-");

                    int count = stages.Count;
                    for (int i = 0; i <= AppConstants.NumberOfFirings - count; i++)
                        stages.Add(currentStage);

                    break;
                }

                foreach (var transition in activeTransitions)
                    Fire(transition, random, currentStage, graphBuilder);

                stages.Add(currentStage);

                PetriNetStateLogger.AddInfo("");
                PetriNetStateLogger.AddInfo(">>Текущее положение меток<<");
                foreach (var place in stages[index].PlacesInfo.Values)
                    PetriNetStateLogger.AddInfo($"\tМесто {place.Id}  >  Кол-во Меток: {place.Tokens.Count}");
            }

            return stages;
        }

        private static void Fire(Transition transition, Random random, GraphInfo currentStage, IGraphBuilder graphBuilder)
        {
            var placesForUpdate = new List<Place>();

            var placeFrom = transition.IncomingPlaces
                .Where(place => place.Tokens.Count > 0)
                .OrderBy(_ => random.Next())
                .First();

            placesForUpdate.Add(placeFrom);
            var token = placeFrom.Tokens.First();
            placeFrom.Tokens.Remove(token);

            PetriNetStateLogger.AddInfo($"ПЕРЕХОД {transition.Id}");
            PetriNetStateLogger.AddInfo($"-----------------------");
            for (int i = 0; i < transition.OutgoingPlaces.Count; i++)
            {
                var placeTo = transition.OutgoingPlaces.ElementAt(i);

                PetriNetStateLogger.AddInfo($"[Место {placeFrom.Id}] --> (токен) --> [Место {placeTo.Id}]");

                if (i == 0)
                {
                    placeTo.Tokens.Add(token);
                }
                else
                {
                    var tokenCopy = DeepCopyToken(token, currentStage.TokensInfo.Keys.Max() + 1);
                    currentStage.TokensInfo.Add(tokenCopy.Id, tokenCopy);
                    placeTo.Tokens.Add(tokenCopy);
                }

                placesForUpdate.Add(placeTo);
            }

            graphBuilder.UpdateTokensPositionForPlaces(placesForUpdate);
        }

        private static List<Transition> GetActiveTransitions(List<Transition> transitions, Random random)
        {
            var availableTokens = new Dictionary<int, int>();

            foreach (var transition in transitions)
            {
                foreach (var place in transition.IncomingPlaces)
                {
                    if (!availableTokens.ContainsKey(place.Id))
                        availableTokens[place.Id] = place.Tokens.Count;
                }
            }

            var activeTransitions = new List<Transition>();
            var shuffledTransitions = transitions.OrderBy(_ => random.Next()).ToList();

            foreach (var transition in shuffledTransitions)
            {
                if (transition.IncomingPlaces.All(place => availableTokens[place.Id] > 0))
                {
                    activeTransitions.Add(transition);

                    foreach (var place in transition.IncomingPlaces)
                        availableTokens[place.Id]--;
                }
            }

            return activeTransitions;
        }

        private static GraphInfo DeepCopyGraphInfo(GraphInfo original)
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
                Metrics = original.Metrics,
                Color = original.Color
            };

            tokenCache[original.Id] = newToken;
            return newToken;
        }

        private static Token DeepCopyToken(Token original, int id)
        {
            return new Token
            {
                Id = id,
                Сoordinates = original.Сoordinates,
                Metrics = original.Metrics,
                Color = original.Color
            };
        }
    }
}
