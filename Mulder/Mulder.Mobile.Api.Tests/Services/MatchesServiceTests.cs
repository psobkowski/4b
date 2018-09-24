using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mulder.DataAccess.Models;
using Mulder.Mobile.Api.Services;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.Tests.Services
{
    [TestClass]
    public class MatchesServiceTests
    {
        [TestMethod]
        public void GetMatchesTest_Check_Score()
        {
            //arrange
            var matches = new List<Matches>
            {
                new Matches
                {
                    Id = 4,
                    Location = "Jeziorany",
                    Address = "ul. Marii Konopnickiej",
                    Year = 2018,
                    MatchesScore = new List<MatchesScore>
                    {
                        new MatchesScore { TeamId = 1, HalfTimeScore = 2, FullTimeScore = 6 },
                        new MatchesScore { TeamId = 2, HalfTimeScore = 1, FullTimeScore = 2 }
                    }
                },
                new Matches
                {
                    Id = 5,
                    Location = "Olsztyn",
                    Address = "ul. Jagielończyka",
                    Year = 2012,
                    MatchesScore = new List<MatchesScore>
                    {
                        new MatchesScore { TeamId = 1, HalfTimeScore = 3, FullTimeScore = 2 },
                        new MatchesScore { TeamId = 2, HalfTimeScore = 2, FullTimeScore = 4 }
                    }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Matches>>();
            mockSet.As<IQueryable<Matches>>().Setup(m => m.Provider).Returns(matches.Provider);
            mockSet.As<IQueryable<Matches>>().Setup(m => m.Expression).Returns(matches.Expression);
            mockSet.As<IQueryable<Matches>>().Setup(m => m.ElementType).Returns(matches.ElementType);
            mockSet.As<IQueryable<Matches>>().Setup(m => m.GetEnumerator()).Returns(matches.GetEnumerator());
            var mockContext = new Mock<MulderContext>();
            mockContext.Setup(c => c.Matches).Returns(mockSet.Object);

            ///act
            var service = new MatchesService(mockContext.Object);
            var result = service.GetMatches();

            //assert
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result[0].ScoreInfo.Team1Score == "6");
            Assert.IsTrue(result[0].ScoreInfo.Team2Score == "2");
            Assert.IsTrue(result[1].ScoreInfo.Team1Score == "2");
            Assert.IsTrue(result[1].ScoreInfo.Team2Score == "4");
        }

        [TestMethod]
        public void GetMatchTest_Check_Player_Name()
        {
            //arrange
            var matches = new List<Matches>
            {
                new Matches
                {
                    Id = 5,
                    MatchesScore = new List<MatchesScore>
                    {
                        new MatchesScore { TeamId = 1, HalfTimeScore = 3, FullTimeScore = 2 },
                        new MatchesScore { TeamId = 2, HalfTimeScore = 2, FullTimeScore = 4 }
                    },
                    MatchesLineUp = new List<MatchesLineUp>
                    {
                        new MatchesLineUp {
                            Player = new Players { NickName = "Sopel" },
                            PlayersScore = new List<PlayersScore>()
                        }
                    },
                    MatchesSpectators = new List<MatchesSpectators>(),
                    
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Matches>>();
            mockSet.As<IQueryable<Matches>>().Setup(m => m.Provider).Returns(matches.Provider);
            mockSet.As<IQueryable<Matches>>().Setup(m => m.Expression).Returns(matches.Expression);
            mockSet.As<IQueryable<Matches>>().Setup(m => m.ElementType).Returns(matches.ElementType);
            mockSet.As<IQueryable<Matches>>().Setup(m => m.GetEnumerator()).Returns(matches.GetEnumerator());
            var mockContext = new Mock<MulderContext>();
            mockContext.Setup(c => c.Matches).Returns(mockSet.Object);

            ///act
            var service = new MatchesService(mockContext.Object);
            var result = service.GetMatch("5");

            //assert
            Assert.IsTrue(result.Players[0].PlayerNick == "Sopel");
        }
    }
}
