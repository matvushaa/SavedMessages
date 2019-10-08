using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.DataAccessLayer.Entities
{
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int PermissionId { get; set; }

        public bool IsVerifyed { get; set; }

        public virtual Permissions Permission { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<FileLocation> Files { get; set; }

        public ICollection<Sticker> Stickers { get; set; }
    }
}
