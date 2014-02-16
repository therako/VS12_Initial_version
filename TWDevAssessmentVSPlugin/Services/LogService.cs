using System;
using System.IO;

namespace TWDevAssessmentVSPlugin.Services
{
    public class LogService
    {
        private readonly string logFilePath;

        public LogService()
        {
            this.logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "TW","log",string.Format("{0}_log.txt",UserService.User.Id));

            File.Create(this.logFilePath);
        }
        public void Log(string logMessage, LogType logType)
        {
            using (var writer = new StreamWriter(this.logFilePath))
            {
                writer.WriteLine(logType);
                writer.WriteLine(logMessage);
                writer.WriteLine(DateTime.Now.ToString());
                writer.WriteLine("===============================");
            }
        }
    }

    public enum LogType
    {
        UserCreated,
        SolutionPosted,
        Refactoring,
    }
}
