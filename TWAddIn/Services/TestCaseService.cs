using System.Collections.Generic;
using TWAddIn.Models;

namespace TWAddIn.Services
{
    public class TestCaseService
    {
        
        private readonly ProcessService processService;

        public TestCaseService(string executableName)
        {
            this.processService = new ProcessService(executableName);
        }

        public List<TestCase> ExecuteTestCase()
        {
            var testCases = new List<TestCase>();
            UserService.User.Problem.TestCases.ForEach(@case =>
            {
                var testCase = new TestCase(@case.TestCaseSteps);
                Execute(testCase);
                CompareOutputWithTheExpected(testCase);
                testCases.Add(testCase);
            });
            return testCases;
        }

        private void CompareOutputWithTheExpected(TestCase testCaseToBeExecuted)
        {
            testCaseToBeExecuted.TestStatus = testCaseToBeExecuted.OutputText.Equals(testCaseToBeExecuted.TestCaseSteps[0].ExpectedOutput) ? TestCase.Status.Passed : TestCase.Status.Failed;
        }

        private void Execute(TestCase testCase)
        {
            processService.WriteToInput(testCase.TestCaseSteps[0].Input);
            testCase.OutputText = processService.GetOutput().Trim();
            processService.CloseProcess();
        }
    }
}
