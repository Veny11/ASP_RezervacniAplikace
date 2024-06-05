using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezervace_Ples.Models.TableObjects
{
    [Table("Table_Stul")]
    public class Stul
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_Stul")]
        public int ID_Stul { get; set; }

        [Column("PocetMist")]
        public int PocetMist { get; set; }

        [Column("Podlazi")]
        public bool Podlazi { get; set; }

        [NotMapped]
        public int ZbyvajiciMista { get; set; }

        public Stul()
        {

        }

    }
}
