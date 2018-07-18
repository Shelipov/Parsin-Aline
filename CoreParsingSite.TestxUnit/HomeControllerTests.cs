using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using CoreParsingSite.Controllers;
using Xunit;
using CoreParsingSite.Models;
using Moq;
using System.Linq;

namespace CoreParsingSite.TestxUnit
{
    public class HomeControllerTests
    {
        

        [Fact]
        public void AboutReturnsAViewResultWithAListOfGoods()
        {
            // Arrange
            var mock = new Mock<IGoodsRepository>();
            mock.Setup(repo => repo.GetGoods()).Returns(GetTestGoods());
            var controller = new HomeController(mock.Object);

            // Act
            var result = controller.About();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Goods>>(viewResult.Model);
            Assert.Equal(GetTestGoods().Count, model.Count());
        }
        
        private List<Goods> GetTestGoods()
        {
            var goods = new List<Goods>
            {
                new Goods { Id=1,Foto=null,Description="Test Description = 1", Name="BD-0033", Stock="Test Stock =1", Price=100.01},
                new Goods { Id=2,Foto=null,Description="Test Description = 2", Name="BD-0058" , Stock="Test Stock =2", Price=200.02},
                new Goods { Id=3,Foto=null,Description="Test Description = 3", Name="BD-0181", Stock="Test Stock =3", Price=685.22},
                new Goods { Id=4,Foto=null,Description="Test Description = 4", Name="BD-0183", Stock="Test Stock =4", Price=729.43},
            };
            return goods;
        }
    }
}
