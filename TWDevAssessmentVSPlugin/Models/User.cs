using System.Runtime.Serialization;

namespace TWDevAssessmentVSPlugin.Models
{
    public class User
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "email")]
        public string EmailId { get; set; }
        
        [DataMember(Name = "TimeGiven")]
        public int TimeGiven { get; set; }//in minutes
        
        [DataMember(Name = "problem")]
        public Problem Problem { get; set; }
    }
}