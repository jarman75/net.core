using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityManagment.Models.user_db
{
    [Table("users", Schema = "public")]
    public partial class User
    {
        [Key]
        [Required]
        public long id { get; set; }

        [Required]
        [ConcurrencyCheck]
        public char[] name { get; set; }

    }
}