﻿using System.Runtime.InteropServices;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

namespace TWDevAssessmentVSPlugin
{
    [Guid("E87F0FC8-5330-442C-AF56-4F42B5F1AD11")]
    public class TWDevAssessmentControl : ToolWindowPane
    {
        public TWDevAssessmentControl()
        {
            base.Content = new TWDevAssessmentScreen();
        }
    }
}
