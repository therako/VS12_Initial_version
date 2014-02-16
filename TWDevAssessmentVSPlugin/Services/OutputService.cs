using EnvDTE;

namespace TWDevAssessmentVSPlugin.Services
{
    public class OutputService
    {
        private OutputWindow outputWindow;

        public OutputService(Window window)
        {
            this.outputWindow = (OutputWindow)window.Object;
        }

        public void WriteToOutput(string outputText)
        {
            outputWindow.ActivePane.Activate();
            outputWindow.ActivePane.OutputString(outputText);
        }
    }
}
