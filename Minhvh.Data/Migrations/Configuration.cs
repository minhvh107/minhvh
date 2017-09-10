using Minhvh.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Minhvh.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Minhvh.Data.MinhvhDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Minhvh.Data.MinhvhDbContext context)
        {
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MinhvhDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new MinhvhDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "tedu",
            //    Email = "tedu.international@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Technology Education"

            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("tedu.international@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

            CreateProductCategorySample(context);
        }

        private void CreateProductCategorySample(MinhvhDbContext context)
        {
            if (!context.ProductCategories.Any())
            {
                List<ProductCategory> listProductCategories = new List<ProductCategory>
                {
                    new ProductCategory(){Name = "Điện lạnh",Alias = "dien-lanh",CreatedBy = "admin",Status = true},
                    new ProductCategory(){Name = "Đồ gia dụng",Alias = "do-gia-dung",CreatedBy = "admin",Status = true},
                    new ProductCategory(){Name = "Quần áo",Alias = "dien-lanh",CreatedBy = "admin",Status = true}
                };
                context.ProductCategories.AddRange(listProductCategories);
                context.SaveChanges();
            }
            
            
        }
    }
}