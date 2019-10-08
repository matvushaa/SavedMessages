using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.DataAccessLayer.Entities
{
    public class Sticker
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StickerId { get; set; }

        public byte[] Stickers { get; set; }

        public string Title { get; set; }

        public Guid UserId { get; set; }

    }
}
