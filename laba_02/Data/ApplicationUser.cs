using laba_02.Data;
using laba_02.Models;
using Microsoft.AspNetCore.Identity;

namespace laba_02.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? Surname { get; set; }

        public string? Ima { get; set; }

        public string? SecSurname { get; set; }

        public int? PolId { get; set; }

        public int? DishId { get; set; }

        public DateTime? DateBirth { get; set; }

        public string? Phone { get; set; }

        public Dish? Dish { get; set; }

        public Pol? Pol { get; set; }
    }

}
