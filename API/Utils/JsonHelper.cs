using Newtonsoft.Json.Linq;

namespace API.Utils
{
    public class JsonHelper
    {
        public static int RandomNumber()
        {
            return new Random().Next(100000);
        }

        public static string GetProjectRootDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory.Split("bin")[0];
        }

        public static string GetDownloadDirectory()
        {
            string downloadsDir = Path.Combine(GetProjectRootDirectory(), "Downloads");
            if (!Directory.Exists(downloadsDir))
                Directory.CreateDirectory(downloadsDir);
            return downloadsDir;
        }

        private static JObject GetTestDataJsonObject(string folderName, string fileName)
        {
            string path;
            if (folderName.Equals("Testdata"))
                path = Path.Combine(GetProjectRootDirectory(), folderName);
            else
                path = Path.Combine(GetProjectRootDirectory(), "Testdata", folderName);
            string jsonPath = path + "\\" + fileName + ".json";
            JObject jObject = JObject.Parse(File.ReadAllText(jsonPath));
            return jObject;
        }

        public static string GetTestDataString(string folderName, string fileName, string label1, string label2 = null!, string label3 = null!, string label4 = null!)
        {
            var jObject = GetTestDataJsonObject(folderName, fileName);
            if (label4 != null)
                return jObject[label1]![label2]![label3]![label4]!.ToString();
            else if (label3 != null)
                return jObject[label1]![label2]![label3]!.ToString();
            else if (label2 != null)
                return jObject[label1]![label2]!.ToString();
            else
                return jObject[label1]!.ToString();
        }

        public static List<string> GetTestDataArray(string folderName, string fileName, string label1, string label2 = null!, string label3 = null!, string label4 = null!)
        {
            var jObject = GetTestDataJsonObject(folderName, fileName);
            if (label4 != null)
                return jObject[label1]![label2]![label3]![label4]!.ToObject<List<string>>()!;
            else if (label3 != null)
                return jObject[label1]![label2]![label3]!.ToObject<List<string>>()!;
            else if (label2 != null)
                return jObject[label1]![label2]!.ToObject<List<string>>()!;
            else
                return jObject[label1]!.ToObject<List<string>>()!;
        }
    }
}
