using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TWAddIn.Models
{

    [DataContract, Serializable]
    public class Problem
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "test_cases")]
        public List<QuestionTestCase> TestCases { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }
    }

    [DataContract, Serializable]
    public class QuestionTestCase
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "steps")]
        public List<TestCaseStep> TestCaseSteps { get; set; }
    }

    [DataContract, Serializable]
    public class TestCaseStep
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "output")]
        public string ExpectedOutput { get; set; }

        [DataMember(Name = "input")]
        public string Input { get; set; }
    }
}
