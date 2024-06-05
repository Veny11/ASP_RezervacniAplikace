using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Rezervace_Ples.Models.TableObjects
{
    [Table("Table_Users")]
    public class User
    {
        [Key]
        [Column("ID")]
        public int ID_User { get; set; }

        [Column("PrihlJmeno")]
        public string Prihlasovaci_Jmeno { get; set; }

        [Column("Heslo")]
        public string Heslo { get; set; }

        [Column("isAdmin")]
        public bool isAdmin { get; set; }

        public User()
        {

        }
    }
}
