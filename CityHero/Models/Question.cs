using System;
using System.Collections.Generic;

namespace CityHero.Models
{
    public partial class Question
    {
        public Question()
        {
            AnsweredQuestions = new HashSet<AnsweredQuestions>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public string PlaceId { get; set; }

        public virtual ICollection<AnsweredQuestions> AnsweredQuestions { get; set; }
    }
}
