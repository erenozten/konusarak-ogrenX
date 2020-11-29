using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication.Core.Entities;
using WebApplication.Data;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            using (var scope = builder.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var initUser = new ApplicationUser()
                {
                    FullName = "Eren Özten",
                    UserName = "erenozten@gmail.com",
                    Email = "erenozten@gmail.com",
                    EmailConfirmed = true,
                };
                
                // asenkron metodu senkron çalýþtýrmak için .GetAwaiter().GetResult();
                IdentityResult result = userManager.CreateAsync(initUser, "Ee123456789*#$").GetAwaiter().GetResult();

                //var existingUser = userManager.FindByEmailAsync("erenozten@gmail.com").GetAwaiter().GetResult();

                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                var questions = context.Questions.ToList();
                var answers = context.Answers.ToList();
                var quiz = context.Quizzes.ToList();
                //context.Quizzes.Add(new Quiz { Title = "Quiz 1",  ApplicationUserId = existingUser.Id });
                //context.Quizzes.Add(new Quiz { Title = "Quiz 2",  ApplicationUserId = existingUser.Id });
                //context.SaveChanges();
            }
            
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
