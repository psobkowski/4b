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
        public void GetMatchesTest_Check_Result_Type()
        {
            // arrange
            var matches = new List<MatchInfo>
            {
                new MatchInfo
                {
                    Id = "4",
                    Location = "Jeziorany",
                    Year = "2018",
                    ScoreInfo = new ScoreInfo
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
                    ScoreInfo = new ScoreInfo
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
            var result = controller.Get();

            //assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.IsInstanceOfType(result.Value, typeof(JsonMobileResult));
            Assert.IsInstanceOfType(((JsonMobileResult)result.Value).Data, typeof(List<MatchInfo>));
            Assert.IsTrue(((JsonMobileResult)result.Value).Success);
        }

        [TestMethod]
        public void GetMatch_Check_If_Player_Goals_Loaded()
        {
            //arrange
            var match = new MatchDetailsInfo
            {
                Id = "5",
                Players = new List<PlayerMatchInfo>
                {
                    new PlayerMatchInfo{
                        PlayerId = "2",
                        Goals = new List<GoalInfo> { new GoalInfo {  Minute = "65" } }
                    }
                }
            };

            var mockService = new Mock<IMatchesService>();
            mockService.Setup(x => x.GetMatch("5")).Returns(match);
            var controller = new MatchesController(mockService.Object);

            //act
            var result = controller.Get("5");

            //assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.IsInstanceOfType(result.Value, typeof(JsonMobileResult));
            Assert.IsInstanceOfType(((JsonMobileResult)result.Value).Data, typeof(MatchDetailsInfo));
            Assert.IsTrue(((JsonMobileResult)result.Value).Success);

            Assert.IsTrue(((MatchDetailsInfo)((JsonMobileResult)result.Value).Data).Players[0].Goals[0].Minute == "65");
        }
    }
}
