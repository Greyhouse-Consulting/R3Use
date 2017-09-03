using System;

namespace R3Use.Core.Entities
{
    public class Period
    {

        

        public int Id { get; set; }
        public int AssignmentId { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; } 
        public DateTime End { get; set; } 
    }
}