using System;
using System.Collections.Generic;

namespace CityHero.Models
{
    public partial class AnsweredQuestions
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
