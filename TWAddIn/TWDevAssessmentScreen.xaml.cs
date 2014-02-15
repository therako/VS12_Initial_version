using System.ComponentModel;
using System.Windows;
using TWAddIn.Services;

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

        public string UserId { get; set; }

        public string QuestionDescription { get; set; }

        
        private void StartTest(object sender, RoutedEventArgs e)
        {
            var userService = new UserService();
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
    }
}
