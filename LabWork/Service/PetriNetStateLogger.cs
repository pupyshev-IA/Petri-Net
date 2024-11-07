using LabWork.Domain;
using System.Text;

namespace LabWork.Service
{
    public static class PetriNetStateLogger
    {
        public static ICollection<string> GetCurrentStageLogs(List<GraphInfo> stages, int currentStageNum)
        {
            var stageLogs = new List<string>();
            var builder = new StringBuilder();

            for (int i = 0; i < currentStageNum + 1; i++)
            {
                builder.Append($"Этап {i}:  [  ");
                foreach (var place in stages[i].PlacesInfo.Values)
                {
                    builder.Append(place.Tokens.Count);
                    builder.Append("; ");
                }
                builder.Append("  ]");

                stageLogs.Add(builder.ToString());
                builder.Clear();
            }

            return stageLogs;
        }

        public static string GetDetailedLogs(List<GraphInfo> stages)
        {
            var builder = new StringBuilder();

            foreach (var index in Enumerable.Range(0, stages.Count))
            {
                builder.AppendLine($"[-----ЭТАП {index}-----]");
                builder.AppendLine();
                builder.AppendLine(">>Текущее положение меток<<");

                var currentTokenSequence = new List<int>();
                foreach (var place in stages[index].PlacesInfo.Values)
                {
                    currentTokenSequence.Add(place.Tokens.Count);
                    builder.AppendLine($"\tМесто {place.Id}  >  Кол-во Меток: {place.Tokens.Count}");
                }

                builder.AppendLine();

                if (index == 0)
                    continue;

                builder.AppendLine(">>Перемещение меток<<");

                var previousTokenSequence = new List<int>();
                foreach (var place in stages[index - 1].PlacesInfo.Values)
                    previousTokenSequence.Add(place.Tokens.Count);

                var movement = TrackTokenMovement(currentTokenSequence, previousTokenSequence);
                builder.AppendLine($"[Место {movement.placeIdFrom}] --> [Место {movement.placeIdTo}]");
                builder.AppendLine();
            }

            return builder.ToString();
        }

        private static (int placeIdFrom, int placeIdTo) TrackTokenMovement(List<int> current, List<int> previous)
        {
            int placeIdFrom = -1;
            int placeIdTo = -1;

            for (int i = 0; i < current.Count; i++)
            {
                int difference = current[i] - previous[i];

                if (difference == -1)
                    placeIdFrom = i;
                else if (difference == 1)
                    placeIdTo = i;

                if (placeIdFrom != -1 && placeIdTo != -1)
                    break;
            }

            return (placeIdFrom + 1, placeIdTo + 1);
        }

        public static void WriteToTextFile(string text)
        {
            try
            {
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string relativePath = Path.Combine(projectDirectory, @"..\..\..\result");
                string fullPath = Path.GetFullPath(relativePath);

                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);

                using StreamWriter writer = new StreamWriter(fullPath + @"\result.txt");
                writer.WriteLine(text);
            }
            catch
            {

            }
        }
    }
}
