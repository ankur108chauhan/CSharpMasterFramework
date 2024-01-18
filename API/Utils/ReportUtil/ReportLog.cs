using API.Utils.ReportUtil;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Model;

namespace API.Utils
{
    public class ReportLog
    {
        public static void Info(string message)
        {
            ExtentTestManager.GetStep().Info(message);
        }

        public static void Info(string[,] table)
        {
            ExtentTestManager.GetStep().Info(MarkupHelper.CreateTable(table));
        }

        public static void Pass(string message)
        {
            ExtentTestManager.GetStep().Pass(message);
        }

        public static void Fail(string message, Media media = null!)
        {
            ExtentTestManager.GetStep().Fail(message, media);
        }

        public static void Skip(string message)
        {
            ExtentTestManager.GetStep().Skip(message);
        }
    }
}
