using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonApiDotNetCore;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProj.Models
{

    public class EmailModel
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }

        [RegularExpression(@"^\w+([.]?[-+'\w]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        [NotMapped]
        public string[] Attributes { get; set; }

        public string Attribute { get; set; }

        public DateTime Date { get;set; }
    }
}
