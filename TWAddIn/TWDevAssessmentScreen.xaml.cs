using System.ComponentModel;
using System.Windows;

namespace TWAddIn
{
    public partial class TWDevAssessmentScreen
    {
        public TWDevAssessmentScreen()
        {
            InitializeComponent();
            IsLoggedIn = true;
        }
        private bool isLoggedIn;

        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set
            {
                isLoggedIn = value;
                OnPropertyChanged("IsLoggedIn");
            }
        }

        public string QuestionDescription { get; set; }

        
        private void StartTest(object sender, RoutedEventArgs e)
        {
            IsLoggedIn = true;

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
    }
}
