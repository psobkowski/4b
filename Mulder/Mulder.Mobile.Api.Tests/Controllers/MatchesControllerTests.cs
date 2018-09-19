using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mulder.Mobile.Api.Controllers;
using Mulder.Mobile.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Domain;

namespace Mulder.Mobile.Api.Tests.Controllers
{
    [TestClass]
    public class MatchesControllerTests
    {
        [TestMethod]
        public void GetTest_CheckResultType()
        {
            // arrange
            var matches = new List<MatchInfo>
            {
                new MatchInfo
                {
                    Id = "4",
                    Location = "Jeziorany",
                    Year = "2018",
                    SocreInfo = new ScoreInfo
                    {
                        Team1Id = "1", Team1HalfTimeScore = "2", Team1Score = "6",
                        Team2Id = "2", Team2HalfTimeScore = "1", Team2Score = "2"
                    }
                },
                new MatchInfo
                {
                    Id = "5",
                    Location = "Olsztyn",
                    Year = "2012",
                    SocreInfo = new ScoreInfo
                    {
                        Team1Id = "1", Team1HalfTimeScore = "3", Team1Score = "5",
                        Team2Id = "2", Team2HalfTimeScore = "2", Team2Score = "3"
                    }
                }
            };

            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetMatches()).Returns(matches);
            var controller = new MatchesController(mockService.Object);

            //act
            var results = controller.Get();

            //assert
            Assert.IsInstanceOfType(results, typeof(JsonResult));
            Assert.IsInstanceOfType(results.Value, typeof(JsonMobileResult));
            Assert.IsInstanceOfType(((JsonMobileResult)results.Value).Data, typeof(List<MatchInfo>));
            Assert.IsTrue(((JsonMobileResult)results.Value).Success);
        }
    }
}
