using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.DataAccessLayer.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        public string MessageText { get; set; }

        public DateTime Time { get; set; }

        public bool IsSaved { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
