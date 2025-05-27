using ApexSole_Sneakers.Controllers;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apex_Sole_Sneakers.Tests.CTest
{
    public class SneakersCTest
    {
        private SneakersController _sC;
        private ISneakersRepository _sRepo;
        private IPhotoService _pS;
        private IHttpContextAccessor _httpA;
        private IShoppingCartRepository _sCartRepo;
        public SneakersCTest()
        {
            _sRepo = A.Fake<ISneakersRepository>();
            _pS = A.Fake<IPhotoService>();
            _httpA = A.Fake<HttpContextAccessor>();
            _sC = new SneakersController(_sRepo, _pS,_httpA, _sCartRepo);
        }



        [Fact]
        public void SC_Detail_ReturnSuccess()
        {
            var id = 1;
            var sneakers = A.Fake<Sneakers>();
            A.CallTo(()=>_sRepo.GetByIdAsync(id)).Returns(sneakers);
            var res = _sC.Detail(id);
            res.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
