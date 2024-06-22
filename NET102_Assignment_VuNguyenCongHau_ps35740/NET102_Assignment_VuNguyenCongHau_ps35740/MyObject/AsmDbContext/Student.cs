using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace NET102_Assignment_VuNguyenCongHau_ps35740.AsmObject.TableObj
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int StId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public double Mark { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public int IdClass { get; set; }

        public virtual Class Class { get; set; }
    }
}
