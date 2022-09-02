using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Model.Attributes;
using Model.Entities.Base;

namespace Model.Entities
{
    public class Filter:BaseEntity
    {
        [Index("IX_PrivateCode",IsUnique = false)]
        public override string PrivateCode { get; set; }
        [Required,StringLength(100),RequiredFields("Filter Name","txtFilterName")]
        public string FilterName { get; set; }
        [Required,StringLength(1000),RequiredFields("Filter Text","txtFilterText")]
        public string FilterText { get; set; }
        [Required]
        public FormType FormType { get; set; }
    }
}
