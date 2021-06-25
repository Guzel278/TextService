
using System;

namespace TextService.Entities.Models
{
    public class TextModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Text { get; set; }
    }
}
