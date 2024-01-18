using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin;
using System.Runtime.CompilerServices;

namespace API.Utils.ReportUtil
{
    class ExtentTestManager
    {
        [ThreadStatic]
        private static ExtentTest feature = null!;

        [ThreadStatic]
        private static ExtentTest scenario = null!;

        [ThreadStatic]
        private static ExtentTest step = null!;


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateFeature(string testName, string description = null!)
        {
            feature = ExtentService.GetExtent().CreateTest(new GherkinKeyword("Feature"), testName, description);
            return feature;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateScenario(string testName, string description = null!)
        {
            scenario = feature.CreateNode(new GherkinKeyword("Scenario"), testName, description);
            return scenario;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateStep(string keyword, string testName, string description = null!)
        {
            step = scenario.CreateNode(new GherkinKeyword(keyword), testName, description);
            return step;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetScenario()
        {
            return scenario;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetStep()
        {
            return step;
        }
    }
}
