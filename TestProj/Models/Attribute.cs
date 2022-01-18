using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonApiDotNetCore;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestProj.Models
{

    public class AttributeModel
    {   [Key]
        public int Id { get; set; }
        public string Attribute { get; set; }

        public string EmailKey { get; set; }



    }
}

