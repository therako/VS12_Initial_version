using System.Collections.Generic;

namespace TWDevAssessmentVSPlugin.Models
{
    public class TestCase
    {
        public List<TestCaseStep> TestCaseSteps;

        public enum Status
        {
            Passed, Failed, Inconclusive
        }

        public Status TestStatus { get; set; }

        public string OutputText { get; set; }

        public TestCase(List<TestCaseStep> testCaseSteps)
        {
            this.TestCaseSteps = testCaseSteps;
        }
    }
}
