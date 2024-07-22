using digital.Shared.Entities.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital.Shared.DTOs
{
    public class LastQuestionDTO
    {
        
        //public int LastQuestionId { get; set; }
        public List<Question>? Questions { get; set; }
            
        public Dictionary<int, int?>? SelectedAnswers { get; set; } 
    }
}
