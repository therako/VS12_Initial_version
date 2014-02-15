using System.Diagnostics;

namespace TWAddIn.Services
{
    public class ProcessService
    {
        private Process process;

        public ProcessService(string executableName)
        {
            this.process = new Process
                               {
                                   StartInfo =
                                       {
                                           FileName = executableName,
                                           UseShellExecute = false,
                                           RedirectStandardInput = true,
                                           RedirectStandardOutput = true
                                       }
                               };
        }

        public void WriteToInput(string testCaseInput)
        {
            this.process.Start();
            var streamWriterToWriteToProcess = this.process.StandardInput;
            streamWriterToWriteToProcess.Write(testCaseInput);
            streamWriterToWriteToProcess.Close();
        }

        public string GetOutput()
        {
            var streamReaderToReadFromProcess = process.StandardOutput;
            var outputText = streamReaderToReadFromProcess.ReadToEnd();
            streamReaderToReadFromProcess.Close();

            return outputText;
        }

        public void CloseProcess()
        {
            this.process.WaitForExit();
            this.process.Close();
        }
    }
}
