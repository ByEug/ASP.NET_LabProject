using LabProject.Resources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Models
{
    public static class DbData
    {

        public static void Initialize(DataContext context)
        {
            if (!context.Shoes.Any())
            {
                //List<UseWay> useWays = new List<UseWay> { new UseWay { WayName = "Basketball" }, new UseWay { WayName = "Football" } };
                List<UseWay> useWays = new List<UseWay>();
                UseWay useWay1 = new UseWay { WayName = "Basketball" };
                UseWay useWay2 = new UseWay { WayName = "Football" };
                useWays.Add(useWay1);
                useWays.Add(useWay2);
                //List<Brand> brands = new List<Brand> { new Brand { BrandName = "Nike", BrandCountry = "USA" }, new Brand { BrandName = "Adidas", BrandCountry = "Germany" } };
                List<Brand> brands = new List<Brand>();
                Brand brand1 = new Brand { BrandName = "Nike", BrandCountry = "USA" };
                Brand brand2 = new Brand { BrandName = "Adidas", BrandCountry = "Germany" };
                brands.Add(brand1);
                brands.Add(brand2);

                context.UseWays.AddRange(useWays);
                context.Brands.AddRange(brands);
                context.SaveChanges();

                List<Shoe> shoes = new List<Shoe> {
                    new Shoe
                    {
                        Amount38 = 1,
                        Amount39 = 3,
                        Amount40 = 1,
                        Amount41 = 5,
                        Amount42 = 2,
                        Amount43 = 0,
                        Amount44 = 1,
                        Amount45 = 5,
                        Price = 289,
                        ImageUrl = "https://air-shop.by/image/cache/catalog/for-order/5fa1a96d4af7c-500x500.jpg",
                        ModelName = "KD TREY 5 VIII",
                        Discription = "New sneakers from Kevin Durant's signature line.",
                        UseWay = useWay1,
                        Brand = brand1
                    },

                    new Shoe
                    {
                        Amount38 = 0,
                        Amount39 = 2,
                        Amount40 = 3,
                        Amount41 = 2,
                        Amount42 = 2,
                        Amount43 = 1,
                        Amount44 = 1,
                        Amount45 = 2,
                        Price = 199,
                        ImageUrl = "https://air-shop.by/image/cache/catalog/for-order/5eb4d1f7b48a2-400x400.jpg",
                        ModelName = "Harden Stepback",
                        Discription = "New sneakers from James Harden's signature line.",
                        UseWay = useWay1,
                        Brand = brand2
                    }
                };
                
                context.Shoes.AddRange(shoes);
                context.SaveChanges();
            }
        }
    }
}
