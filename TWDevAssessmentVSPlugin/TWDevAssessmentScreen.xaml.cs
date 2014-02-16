using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows;
using EnvDTE;
using EnvDTE80;
using TWDevAssessmentVSPlugin.Services;
using IServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace TWDevAssessmentVSPlugin
{
    public partial class TWDevAssessmentScreen
    {
        private BuildEvents buildEvents;

        public TWDevAssessmentScreen()
        {
            IsLoginEnabled = true;
            IsQuestionEnabled = true;
            InitializeComponent();
            userService = new UserService();
            zipService = new ZipService();
            Dte = Marshal.GetActiveObject("VisualStudio.DTE.11.0") as DTE2;
        }

        private readonly UserService userService;
        private readonly ZipService zipService;
        private DTE2 Dte;
        private string userId = "";
        private string questionDescription ="";
        private bool isQuestionEnabled;
        private bool isLoginEnabled;
        private string testResults;


        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                NotifyPropertyChanged("UserId");
            }
        }

        public bool IsQuestionEnabled
        {
            get { return isQuestionEnabled; }
            set
            {
                isQuestionEnabled = value;
                NotifyPropertyChanged("IsQuestionEnabled");
            }
        }

        public bool IsLoginEnabled
        {
            get { return isLoginEnabled; }
            set
            {
                isLoginEnabled = value;
                NotifyPropertyChanged("IsLoginEnabled");
            }
        }

        public string QuestionDescription
        {
            get { return questionDescription; }
            set
            {
                questionDescription = value;
                NotifyPropertyChanged("QuestionDescription");
            }
        }

        public string TestResults
        {
            get { return testResults; }
            set
            {
                testResults = value;
                NotifyPropertyChanged("TestResults");
            }
        }

        private void StartTest(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(UserId))
                return;
            var result = userService.BeginSessionForTheUser(UserId);
            if (result)
            {
                MessageBox.Show("Successfully Registered, You can start writing your code on a Console Application Project.\n Refer to THOUGHTWORKS menu for Questions and Test cases execution.");
                IsLoginEnabled = false;
                IsQuestionEnabled = true;
                QuestionDescription = WebUtility.HtmlDecode(UserService.User.Problem.Description);
            }
            else
            {
                MessageBox.Show("ThoughtWorks server not reachable! Please try again after sometime.\nIf issue continues, please contact recruitment team.");
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string name)
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
            var dte = (DTE)Marshal.GetActiveObject("VisualStudio.DTE");
            var activeSolutionProjects = dte.ActiveSolutionProjects as Array;
            var project = activeSolutionProjects.GetValue(0) as Project;
            var projectDirectory = Path.GetDirectoryName(project.FullName);
            var executableName = Path.Combine(projectDirectory, "bin", "debug", string.Format("{0}.exe", project.Name));

            var testCaseService = new TestCaseService(executableName);
            var executedTestCases = testCaseService.ExecuteTestCase();

            testResults = "";
            
            executedTestCases.ForEach(
                @case =>
                {
                    testResults += string.Format("{0} : {1} \n", "TestCase", executedTestCases.IndexOf(@case) + 1);
                    testResults += string.Format("{0} : {1} \n", "Status", @case.TestStatus);
                    testResults += string.Format("{0} : {1} \n", "Input", @case.TestCaseSteps[0].Input);
                    testResults += string.Format("{0} : {1} \n", "Expected Output", @case.TestCaseSteps[0].ExpectedOutput);
                    testResults += string.Format("{0} : {1} \n\n", "Actual Output", @case.OutputText);
                });
        }
    }
}
