using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezervace_Ples.Models.TableObjects
{
    [Table("Table_Lidi")]
    public class Lidi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_Lidi")]
        public int ID_Lidi { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Surname")]
        public string Surname { get; set; }

        [ForeignKey("Stul")]
        [Column("Stul")]
        public int ID_Stul { get; set; }


        public Lidi(string text1, string text2, int v2)
        {
            Name = text1;
            Surname = text2;
            ID_Stul = v2;
        }

        public Lidi()
        {

        }
    }
}
