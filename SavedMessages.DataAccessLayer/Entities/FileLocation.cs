﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavedMessages.DataAccessLayer.Entities
{
    public class FileLocation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int FileId { get; set; }

        public byte[] File { get; set; }

        public Guid UserId { get; set; }

        public DateTime Time { get; set; }

        public bool IsSaved { get; set; }

        public User User { get; set; }
    }
}
