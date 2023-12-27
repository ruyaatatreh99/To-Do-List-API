using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    [Table("tasktable")]
    public class Tasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MinLength(1)]
        public string? Title { get; set; }

        [MinLength(1)]
        public string? Description { get; set; }
        public int? IsCompleted { get; set; }
        
        [MinLength(1)]
        public string? Category { get; set; }



    }
}
