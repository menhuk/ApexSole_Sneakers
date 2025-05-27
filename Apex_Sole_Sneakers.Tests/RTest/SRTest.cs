using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Data.Enum;
using ApexSole_Sneakers.Models;
using ApexSole_Sneakers.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Apex_Sole_Sneakers.Tests.RTest
{
    public class SRTest
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var option = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString()).Options;
            var context = new ApplicationDbContext(option);
            context.Database.EnsureCreated();
            if (await context.Sneakers.CountAsync() < 0 )
            {
                for (int i = 0; i < 10; i++)
                {
                    
                    context.Sneakers.Add( new Sneakers()
                    {
                        SneakersName = "SneakersName",
                        SneakersBrand = "SneakearsBrand",
                        SneakersColor = "red",
                        SneakersDescription = "Description",
                        SneakersType = SneakersType.SkateSneakers,
                        SneakersSize = 43,
                        SneakersPrice = 89,
                        Image = "https://nb.scene7.com/is/image/NB/m1906rgr_nb_04_i?$pdpflexf2$&qlt=80&fmt=webp&wid=440&hei=440"
                    });
                    await context.SaveChangesAsync();
                }
            }
            return context;
        }

        [Fact]
        public async void SneakersRepository_Add()
        {
            var sn = new Sneakers()
            {
                SneakersName = "SneakersName",
                SneakersBrand = "SneakearsBrand",
                SneakersColor = "red",
                SneakersDescription = "Description",
                SneakersType = SneakersType.SkateSneakers,
                SneakersSize = 43,
                SneakersPrice = 89,
                Image = "https://nb.scene7.com/is/image/NB/m1906rgr_nb_04_i?$pdpflexf2$&qlt=80&fmt=webp&wid=440&hei=440"
            };
            var context2 = await GetDbContext();
            var repo = new SneakersRepository(context2);
            var res = repo.Add(sn);
            res.Should().BeTrue();
        }
        [Fact]
        public async void SneakersRepository_GetByIdAsync_ReturnSneakers()
        {
            var id = 1;
            var dbContext = await GetDbContext();
            var sneakersRepo = new SneakersRepository(dbContext);
            var res = sneakersRepo.GetByIdAsync(id);
            res.Should().NotBeNull();
            res.Should().BeOfType<Task<Sneakers>>();
        }
        [Fact]
        public async void SneakersRepository_GetAll_ReturnsList()
        {
            var dbContext = await GetDbContext();
            var snRepo = new SneakersRepository(dbContext);

            var result = await snRepo.GetAll();

            result.Should().NotBeNull();
            result.Should().BeOfType<List<Sneakers>>();
        }
        [Fact]
        public async void SneakersRepository_SuccessfulDelete_ReturnsTrue()
        {
            var sn = new Sneakers()
            {
                SneakersName = "SneakersName",
                SneakersBrand = "SneakearsBrand",
                SneakersColor = "red",
                SneakersDescription = "Description",
                SneakersType = SneakersType.SkateSneakers,
                SneakersSize = 43,
                SneakersPrice = 89,
                Image = "https://nb.scene7.com/is/image/NB/m1906rgr_nb_04_i?$pdpflexf2$&qlt=80&fmt=webp&wid=440&hei=440"
            };
            var dbContext = await GetDbContext();
            var snRepo = new SneakersRepository(dbContext);

            snRepo.Add(sn);
            var res = snRepo.Delete(sn);

            res.Should().BeTrue();
        }


    }
}
