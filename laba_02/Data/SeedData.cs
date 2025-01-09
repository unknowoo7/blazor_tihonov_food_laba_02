using laba_02.Data;
using laba_02.Databse;
using Microsoft.AspNetCore.Identity;

public class SeedData
{
  public async Task InitializeAsync(ApplicationDbContext context)
  {
    var pols = new List<Pol>
    {
      new Pol { Id = 1, NamePol = "Мужской" },
      new Pol { Id = 2, NamePol = "Женский" }
    };
    context.Pols.AddRange(pols);
    await context.SaveChangesAsync();

    var role1 = new IdentityRole("Administrator");

    var role2 = new IdentityRole("Cook");

    await context.Roles.AddAsync(role1);

    await context.Roles.AddAsync(role2);

    var user1 = new ApplicationUser
    {
      UserName = "kazakov@mail.ru",
      Email = "kazakov@mail.ru",
      Surname = "Казаков",
      Ima = "Андрей",
      SecSurname = "Юрьевич",
      PolId = 1,
      DateBirth = DateTime.Parse("11.01.1995"),
      Phone = "234234234234",
      NormalizedEmail = "KAZAKOV@MAIL.RU",
      NormalizedUserName = "KAZAKOV@MAIL.RU",
      LockoutEnabled = true,
      DishId = null
    };

    var passwordHasher = new PasswordHasher<ApplicationUser>();

    user1.PasswordHash = passwordHasher.HashPassword(user1, "password123");

    var res = await context.Users.AddAsync(user1);

    await context.SaveChangesAsync();

    var res2 = await context.UserRoles.AddAsync(
      new IdentityUserRole<string>
      {
        RoleId = role1.Id,
        UserId = user1.Id
      }
    );

    await context.SaveChangesAsync();
  }
}