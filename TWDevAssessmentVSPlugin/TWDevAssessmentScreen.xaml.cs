using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using EnvDTE;
using TWDevAssessmentVSPlugin.Services;

namespace TWDevAssessmentVSPlugin
{
    public partial class TWDevAssessmentScreen
    {
        public TWDevAssessmentScreen()
        {
            InitializeComponent();
            IsLoggedIn = true;
            userService = new UserService();
            zipService = new ZipService();
        }
        private bool isLoggedIn;
        private readonly UserService userService;
        private readonly ZipService zipService;

        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set
            {
                isLoggedIn = value;
                OnPropertyChanged("IsLoggedIn");
            }
        }

        public string UserId { get; set; }

        public string QuestionDescription { get; set; }

        
        private void StartTest(object sender, RoutedEventArgs e)
        {
            
            var result = userService.BeginSessionForTheUser(UserId);
            if (result)
            {
                MessageBox.Show("Successfully Registered, You can start writing your code on a Console Application Project.\n Refer to THOUGHTWORKS menu for Questions and Test cases execution.");
                IsLoggedIn = true;
            }
            else
            {
                MessageBox.Show("ThoughtWorks server not reachable! Please try again after sometime.\nIf issue continues, please contact recruitment team.");
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void SubmitCode(object sender, RoutedEventArgs e)
        {
            userService.FreezeAssessment();
            zipService.DeleteExistingZips();
            MessageBox.Show("Thank you for taking the assessment.");
        }

        private void RunTestCases(object sender, RoutedEventArgs e)
        {
            var dte = (DTE)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE");
            var activeSolutionProjects = dte.ActiveSolutionProjects as Array;
            var project = activeSolutionProjects.GetValue(0) as Project;
            var projectDirectory = Path.GetDirectoryName(project.FullName);
            var executableName = Path.Combine(projectDirectory, "bin", "debug", string.Format("{0}.exe", project.Name));

            var testCaseService = new TestCaseService(executableName);
            var executedTestCases = testCaseService.ExecuteTestCase();
            
            var outputWindow = dte.Windows.Item(Constants.vsWindowKindOutput);
            var outputService = new OutputService(outputWindow);
            
            executedTestCases.ForEach(
                @case =>
                {
                    outputService.WriteToOutput(string.Format("{0} : {1} \n", "TestCase", executedTestCases.IndexOf(@case) + 1));
                    outputService.WriteToOutput(string.Format("{0} : {1} \n", "Status", @case.TestStatus));
                    outputService.WriteToOutput(string.Format("{0} : {1} \n", "Input", @case.TestCaseSteps[0].Input));
                    outputService.WriteToOutput(string.Format("{0} : {1} \n", "Expected Output", @case.TestCaseSteps[0].ExpectedOutput));
                    outputService.WriteToOutput(string.Format("{0} : {1} \n\n", "Actual Output", @case.OutputText));
                });
        }
    }
}
