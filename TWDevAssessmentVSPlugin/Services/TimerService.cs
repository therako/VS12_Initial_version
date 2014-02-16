using System.Timers;
using Microsoft.VisualStudio.Shell.Interop;

namespace TWDevAssessmentVSPlugin.Services
{
    public class TimerService
    {
        private readonly IVsStatusbar statusBar;

        private int TimeRemaining;

        private static int interval = 10;
        public TimerService(IVsStatusbar statusBar)
        {
            this.statusBar = statusBar;
        }

        public void InitializeTimer(int timeGiven)
        {
            TimeRemaining = timeGiven;
            var timer = new Timer();
            timer.Elapsed += this.TimerElapsed;
            timer.Interval = interval * 1000;
            timer.Enabled = true;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (TimeRemaining <= 0)
            {
                //TODO: Auto freeze
            }
            TimeRemaining -= interval;
            var hours = TimeRemaining / (60 * 60);
            var minutes = (TimeRemaining % (60 * 60)) / 60;
            var seconds = (TimeRemaining % (60 * 60)) % 60;
            var displayText = string.Format("Time Remaining {0}:{1}:{2}", hours, minutes, seconds);
            this.statusBar.SetText(displayText);
            this.statusBar.Clear();
        }
    }
}
