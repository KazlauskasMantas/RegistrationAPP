namespace RegistrationAPP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name field should contain at least 2 letters!")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Name should start with an uppercase letter and contain only letters!")]
        [Display(Name = "First name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password should contain from 5 to 50 letters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime Bdate { get; set; }

        [Display(Name = "Additional information")]
        [DataType(DataType.MultilineText)]
        public string AddInfo { get; set; }
    }
}
