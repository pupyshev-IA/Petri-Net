using LabWork.Domain;
using System.Text;

namespace LabWork.Service
{
    public static class PetriNetStateLogger
    {
        private static StringBuilder _builder;

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

        public static void InitializeNewBuilder() => 
            _builder = new StringBuilder();

        public static void AddInfo(string text) => 
            _builder.AppendLine(text);

        public static string GetDetailedLogs() => 
            _builder.ToString();

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
