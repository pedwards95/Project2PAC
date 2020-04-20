using System;
using System.Collections.Generic;
using Xunit;
using _2PAC.DataAccess.Context;
using _2PAC.Domain.LogicModel;
using _2PAC.DataAccess.Logic;
using _2PAC.DataAccess.Repositories;


namespace _2PAC.Tests.DataAcces
{
    public class MapperTest
    {

// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! ------  GAME DATA  ---------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        [Fact]
        public void MapGameDataTest()
        {
            D_GameData sampleGameDataD = new D_GameData
            {
                DataId = 100,
                GameId = 100,
                Difficulty = 10,
                Question = "Test Question.",
                Answer = "Test Answer.",
                Game = new D_Game
                {
                    GameName = "Test Game."
                }
            };

            L_GameData sampleGameDataL = new L_GameData
            {
                DataId = 100,
                GameId = 100,
                GameName = "Test Game.",
                Difficulty = 10,
                Question = "Test Question.",
                Answer = "Test Answer."
            };

            L_GameData resultGameDataL = Mapper.MapGameData(sampleGameDataD);

            Assert.True(compareGameDataL(resultGameDataL,sampleGameDataL));
        }
        [Fact]
        public void UnMapGameDataTest()
        {
            L_GameData sampleGameDataL = new L_GameData
            {
                DataId = 100,
                GameId = 1,
                GameName = "Memory Bingo",
                Difficulty = 10,
                Question = "Test Question.",
                Answer = "Test Answer."
            };

            D_GameData sampleGameDataD = new D_GameData
            {
                DataId = 100,
                GameId = 1,
                Difficulty = 10,
                Question = "Test Question.",
                Answer = "Test Answer."
            };

            D_GameData resultGameDataD = Mapper.UnMapGameData(sampleGameDataL);

            Assert.True(compareGameDataD(resultGameDataD,sampleGameDataD));
        }

        private bool compareGameDataL(L_GameData x, L_GameData y)
        {
            if(
                x.Answer != y.Answer ||
                x.DataId != y.DataId ||
                x.Difficulty != y.Difficulty ||
                x.GameId != y.GameId ||
                x.GameName != y.GameName ||
                x.Question != y.Question
            )
            {
                return false;
            }
            return true;
        }

        private bool compareGameDataD(D_GameData x, D_GameData y)
        {
            if(
                x.Answer != y.Answer ||
                x.DataId != y.DataId ||
                x.Difficulty != y.Difficulty ||
                x.GameId != y.GameId ||
                x.Question != y.Question
            )
            {
                return false;
            }
            return true;
        }

// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! ---------  GAME  ------------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        [Fact]
        public void MapGameTest()
        {
            D_Game sampleGameD = new D_Game
            {
                GameId = 100,
                GameName = "Test Game Name.",
                GameDescription = "Test Game Description",
                Reviews = new List<D_Review> {},
                Scores = new List<D_Score> {},
                Data = new List<D_GameData> {}
            };

            L_Game sampleGameL = new L_Game
            {
                GameId = 100,
                GameName = "Test Game Name.",
                GameDescription = "Test Game Description",
                Reviews = new List<L_Review> {},
                Scores = new List<L_Score> {},
                Data = new List<L_GameData> {}
            };

            L_Game resultGameL = Mapper.MapGame(sampleGameD);

            Assert.True(compareGameL(resultGameL,sampleGameL));
        }

        [Fact]
        public void UnMapGameTest()
        {
            L_Game sampleGameL = new L_Game
            {
                GameId = 100,
                GameName = "Test Game Name.",
                GameDescription = "Test Game Description",
            };

            D_Game sampleGameD = new D_Game
            {
                GameId = 100,
                GameName = "Test Game Name.",
                GameDescription = "Test Game Description",
            };

            D_Game resultGameD = Mapper.UnMapGame(sampleGameL);

            Assert.True(compareGameD(resultGameD,sampleGameD));
        }

        private bool compareGameL(L_Game x, L_Game y)
        {
            if(
                x.GameId != y.GameId ||
                x.GameDescription != y.GameDescription ||
                x.GameName != y.GameName ||
                x.Data.Count != y.Data.Count ||
                x.Reviews.Count != y.Reviews.Count ||
                x.Scores.Count != y.Scores.Count
            )
            {
                return false;
            }
            return true;
        }

        private bool compareGameD(D_Game x, D_Game y)
        {
            if(
                x.GameId != y.GameId ||
                x.GameDescription != y.GameDescription ||
                x.GameName != y.GameName
            )
            {
                return false;
            }
            return true;
        }

// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! --------  NOTICE  -----------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        [Fact]
        public void MapNoticeTest()
        {
            DateTime myTime = new DateTime(1000,10,10);
            D_Notice sampleNoticeD = new D_Notice
            {
                NoticeId = 100,
                Description = "Test Notice.",
                Time = myTime
            };

            L_Notice sampleNoticeL = new L_Notice
            {
                NoticeId = 100,
                Description = "Test Notice.",
                Time = myTime
            };

            L_Notice resultNoticeL = Mapper.MapNotice(sampleNoticeD);

            Assert.True(compareNoticeL(resultNoticeL,sampleNoticeL));
        }

        [Fact]
        public void UnMapNoticeTest()
        {
            DateTime myTime = new DateTime(1000,10,10);
            L_Notice sampleNoticeL = new L_Notice
            {
                NoticeId = 100,
                Description = "Test Notice.",
                Time = myTime
            };

            D_Notice sampleNoticeD = new D_Notice
            {
                NoticeId = 100,
                Description = "Test Notice.",
                Time = myTime
            };

            D_Notice resultNoticeD = Mapper.UnMapNotice(sampleNoticeL);

            Assert.True(compareNoticeD(resultNoticeD,sampleNoticeD));
        }

        private bool compareNoticeL(L_Notice x, L_Notice y)
        {
            if(
                x.NoticeId != y.NoticeId ||
                x.Description != y.Description ||
                x.Time != y.Time
            )
            {
                return false;
            }
            return true;
        }

        private bool compareNoticeD(D_Notice x, D_Notice y)
        {
            if(
                x.NoticeId != y.NoticeId ||
                x.Description != y.Description ||
                x.Time != y.Time
            )
            {
                return false;
            }
            return true;
        }

// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! --------  REVIEW  -----------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        [Fact]
        public void MapReviewTest()
        {
            D_Review sampleReviewD = new D_Review
            {
                ReviewId = 100,
                UserId = 100,
                GameId = 100,
                Rating = 5,
                ReviewBody = "Test Review.",
                User = new D_User
                {
                    Username = "Test Username."
                },
                Game = new D_Game
                {
                    GameName = "Test Game Name."
                }
            };

            L_Review sampleReviewL = new L_Review
            {
                ReviewId = 100,
                UserId = 100,
                Username = "Test Username.",
                GameId = 100,
                GameName = "Test Game Name.",
                Rating = 5,
                ReviewBody = "Test Review."
            };

            L_Review resultReviewL = Mapper.MapReview(sampleReviewD);

            Assert.True(compareReviewL(resultReviewL,sampleReviewL));
        }

        [Fact]
        public void UnMapReviewTest()
        {
            L_Review sampleReviewL = new L_Review
            {
                ReviewId = 100,
                UserId = 100,
                GameId = 100,
                Rating = 5,
                ReviewBody = "Test Review.",
                Username = "Test Username.",
                GameName = "Test Game Name."
            };

            D_Review sampleReviewD = new D_Review
            {
                ReviewId = 100,
                UserId = 100,
                GameId = 100,
                Rating = 5,
                ReviewBody = "Test Review."
            };

            D_Review resultReviewD = Mapper.UnMapReview(sampleReviewL);

            Assert.True(compareReviewD(resultReviewD,sampleReviewD));
        }

        private bool compareReviewL(L_Review x, L_Review y)
        {
            if(
                x.GameId != y.GameId ||
                x.GameName != y.GameName ||
                x.Rating != y.Rating ||
                x.ReviewBody != y.ReviewBody ||
                x.ReviewId != y.ReviewId ||
                x.UserId != y.UserId ||
                x.Username != y.Username
            )
            {
                return false;
            }
            return true;
        }

        private bool compareReviewD(D_Review x, D_Review y)
        {
            if(
                x.GameId != y.GameId ||
                x.Rating != y.Rating ||
                x.ReviewBody != y.ReviewBody ||
                x.ReviewId != y.ReviewId ||
                x.UserId != y.UserId
            )
            {
                return false;
            }
            return true;
        }

// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! ---------  SCORE  -----------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        [Fact]
        public void MapScoreTest()
        {
            D_Score sampleScoreD = new D_Score
            {
                ScoreId = 100,
                UserId = 100,
                GameId = 100,
                Score = 5566,
                User = new D_User
                {
                    Username = "Test Username."
                },
                Game = new D_Game
                {
                    GameName = "Test Game Name."
                }
            };

            L_Score sampleScoreL = new L_Score
            {
                ScoreId = 100,
                UserId = 100,
                Username = "Test Username.",
                GameId = 100,
                GameName = "Test Game Name.",
                Score = 5566
            };

            L_Score resultScoreL = Mapper.MapScore(sampleScoreD);

            Assert.True(compareScoreL(resultScoreL,sampleScoreL));
        }

        [Fact]
        public void UnMapScoreTest()
        {
            L_Score sampleScoreL = new L_Score
            {
                ScoreId = 100,
                UserId = 100,
                GameId = 100,
                Score = 5566,
                Username = "Test Username.",
                GameName = "Test Game Name."
            };

            D_Score sampleScoreD = new D_Score
            {
                ScoreId = 100,
                UserId = 100,
                GameId = 100,
                Score = 5566
            };

            D_Score resultScoreD = Mapper.UnMapScore(sampleScoreL);

            Assert.True(compareScoreD(resultScoreD,sampleScoreD));
        }

        private bool compareScoreL(L_Score x, L_Score y)
        {
            if(
                x.GameId != y.GameId ||
                x.GameName != y.GameName ||
                x.Score != y.Score ||
                x.ScoreId != y.ScoreId ||
                x.UserId != y.ScoreId ||
                x.Username != y.Username
            )
            {
                return false;
            }
            return true;
        }

        private bool compareScoreD(D_Score x, D_Score y)
        {
            if(
                x.GameId != y.GameId ||
                x.Score != y.Score ||
                x.ScoreId != y.ScoreId ||
                x.UserId != y.ScoreId
            )
            {
                return false;
            }
            return true;
        }

// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! ---------  USER  -----------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        [Fact]
        public void MapUserTest()
        {
            D_User sampleUserD = new D_User
            {
                UserId = 100,
                PictureId = 100,
                FirstName = "Test First.",
                LastName = "Test Last.",
                Username = "Test Username.",
                Password = "Test Password.",
                Description = "Test Description.",
                Admin = false,
                Scores = new List<D_Score> {},
                Reviews = new List<D_Review> {}
            };

            L_User sampleUserL = new L_User
            {
                UserId= 100,
                PictureId = 100,
                FirstName = "Test First.",
                LastName = "Test Last.",
                Username = "Test Username.",
                Password = "Test Password.",
                Description = "Test Description.",
                Admin = false,
                Scores = new List<L_Score> {},
                Reviews = new List<L_Review> {}
            };

            L_User resultUserL = Mapper.MapUser(sampleUserD);

            Assert.True(CompareUserL(resultUserL,sampleUserL));
        }

        [Fact]
        public void UnMapUserTest()
        {
            L_User sampleUserL = new L_User
            {
                UserId = 100,
                PictureId = 100,
                FirstName = "Test First.",
                LastName = "Test Last.",
                Username = "Test Username.",
                Password = "Test Password.",
                Description = "Test Description.",
                Admin = false
            };

            D_User sampleUserD = new D_User
            {
                UserId= 100,
                PictureId = 100,
                FirstName = "Test First.",
                LastName = "Test Last.",
                Username = "Test Username.",
                Password = "Test Password.",
                Description = "Test Description.",
                Admin = false
            };

            D_User resultUserD = Mapper.UnMapUser(sampleUserL);

            Assert.True(CompareUserD(resultUserD,sampleUserD));
        }

        private bool CompareUserL(L_User x, L_User y)
        {
            if(
                x.Admin != y.Admin ||
                x.Description != y.Description ||
                x.FirstName != y.FirstName ||
                x.LastName != y.LastName ||
                x.Password != y.Password ||
                x.PictureId != y.PictureId ||
                x.Reviews.Count != y.Reviews.Count ||
                x.Scores.Count != y.Scores.Count
            )
            {
                return false;
            }
            return true;
        }

        private bool CompareUserD(D_User x, D_User y)
        {
            if(
                x.Admin != y.Admin ||
                x.Description != y.Description ||
                x.FirstName != y.FirstName ||
                x.LastName != y.LastName ||
                x.Password != y.Password ||
                x.PictureId != y.PictureId
            )
            {
                return false;
            }
            return true;
        }

    }
}