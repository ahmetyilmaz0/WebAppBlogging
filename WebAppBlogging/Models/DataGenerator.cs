using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppBlogging.Models
{
    public class DataGenerator
    {
        public static void Initialize(BloggingDbContext context)
        {

            // Look for any board games.
            if (context.Users.Any())
            {
                return;   // Data was already seeded
            }

            context.Users.AddRange(
                    new Users
                    {
                        UserID = 1,
                        UserName = "user",
                        Email = "user1@gmail.com",
                        Password = "user1",
                        UserRole = "user"
                    },
                    new Users
                    {
                        UserID = 2,
                        UserName = "admin",
                        Email = "useradmin@admin.com",
                        Password = "admin",
                        UserRole = "admin"
                    },
                    new Users
                    {
                        UserID = 3,
                        UserName = "test",
                        Email = "user2@outlook.com",
                        Password = "user2",
                        UserRole = "user"
                    });
            context.SaveChanges();
            context.Entities.AddRange(
                new Entities
                {
                    EntityID = 1,
                    Entity = "It can be Entity!",
                    User = context.Users.FirstOrDefault(x => x.UserID == 1)
                },
                new Entities
                {
                    EntityID = 2,
                    Entity = "It can be money 150$",
                    User = context.Users.FirstOrDefault(x => x.UserID == 1)
                },
                new Entities
                {
                    EntityID = 3,
                    Entity = "It can be entity something.",
                    User = context.Users.FirstOrDefault(x => x.UserID == 1)
                },
                new Entities
                {
                    EntityID = 4,
                    Entity = "According to the program we can change it.",
                    User = context.Users.FirstOrDefault(x => x.UserID == 3)
                },
                new Entities
                {
                    EntityID = 5,
                    Entity = "This is kind of entity.",
                    User = context.Users.FirstOrDefault(x => x.UserID == 2)
                });
            context.SaveChanges();
            
        }
    }
}
