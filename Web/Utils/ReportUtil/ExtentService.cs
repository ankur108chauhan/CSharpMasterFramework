using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;

namespace Web.Utils.ReportUtil
{
    internal class ExtentService
    {
        private static ExtentReports extent = null!;

        public static ExtentReports GetExtent()
        {
            if (extent == null)
            {
                extent = new ExtentReports();
                string reportDir = Path.Combine(JsonHelper.GetProjectRootDirectory(), "Report");
                if (!Directory.Exists(reportDir))
                    Directory.CreateDirectory(reportDir);

                string path = Path.Combine(reportDir, "index.html");
                var reporter = new ExtentSparkReporter(path);
                reporter.Config.DocumentTitle = "Framework Report";
                reporter.Config.ReportName = "Test Automation Report";
                reporter.Config.Theme = Theme.Standard;
                extent.AddSystemInfo("Browser", TestContext.Parameters["Browser"]);
                extent.AttachReporter(reporter);
            }
            return extent;
        }
    }
}
