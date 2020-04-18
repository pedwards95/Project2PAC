using System;
using System.Linq;
using _2PAC.Domain.LogicModel;
using _2PAC.DataAccess.Context;

namespace _2PAC.DataAccess.Logic
{
    public static class Mapper
    {
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! ------  GAME DATA  ---------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        public static L_GameData MapGameData(D_GameData gameData)
        {
            return new L_GameData
            {
                DataId = gameData.DataId,
                GameId = gameData.GameId,
                GameName = gameData.Game.GameName,
                Difficulty = gameData.Difficulty,
                Question = gameData.Question,
                Answer = gameData.Answer
            };
        }

        public static D_GameData UnMapGameData(L_GameData gameData)
        {
            return new D_GameData
            {
                DataId = gameData.DataId,
                GameId = gameData.GameId,
                Difficulty = gameData.Difficulty,
                Question = gameData.Question,
                Answer = gameData.Answer
            };
        }
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! ---------  GAME  ------------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        public static L_Game MapGame(D_Game game)
        {
            return new L_Game
            {
                GameId = game.GameId,
                GameName = game.GameName,
                GameDescription = game.GameDescription,
                Reviews = game.Reviews.Select(MapReview).ToList(),
                Scores = game.Scores.Select(MapScore).ToList(),
                Data = game.Data.Select(MapGameData).ToList()
            };
        }

        public static D_Game UnMapGame(L_Game game)
        {
            return new D_Game
            {
                GameId = game.GameId,
                GameName = game.GameName,
                GameDescription = game.GameDescription
            };
        }

// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! --------  NOTICE  -----------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        public static L_Notice MapNotice(D_Notice notice)
        {
            return new L_Notice
            {
                NoticeId = notice.NoticeId,
                Description = notice.Description,
                Time = notice.Time
            };
        }

        public static D_Notice UnMapNotice(L_Notice notice)
        {
            return new D_Notice
            {
                NoticeId = notice.NoticeId,
                Description = notice.Description,
                Time = notice.Time
            };
        }
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! --------  REVIEW  -----------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        public static L_Review MapReview(D_Review review)
        {
            return new L_Review
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                Username = review.User.Username,
                GameId = review.GameId,
                GameName = review.Game.GameName,
                Rating = review.Rating,
                ReviewBody = review.ReviewBody
            };
        }

        public static D_Review UnMapReview(L_Review review)
        {
            return new D_Review
            {
                ReviewId = review.ReviewId,
                UserId = review.UserId,
                GameId = review.GameId,
                Rating = review.Rating,
                ReviewBody = review.ReviewBody
            };
        }
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! ---------  SCORE  -----------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        public static L_Score MapScore(D_Score score)
        {
            return new L_Score
            {
                ScoreId = score.ScoreId,
                UserId = score.UserId,
                Username = score.User.Username,
                GameId = score.GameId,
                GameName = score.Game.GameName,
                Score = score.Score
            };
        }

        public static D_Score UnMapScore(L_Score score)
        {
            return new D_Score
            {
                ScoreId = score.ScoreId,
                UserId = score.UserId,
                GameId = score.GameId,
                Score = score.Score
            };
        }
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
// ! ---------  USER  -----------
// ! XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        public static L_User MapUser(D_User user)
        {
            return new L_User
            {
                UserId = user.UserId,
                PictureId = user.PictureId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                Description = user.Description,
                Admin = user.Admin,
                Reviews = user.Reviews.Select(MapReview).ToList(),
                Scores = user.Scores.Select(MapScore).ToList()
            };
        }

        public static D_User UnMapUser(L_User user)
        {
            return new D_User
            {
                UserId = user.UserId,
                PictureId = user.PictureId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                Description = user.Description,
                Admin = user.Admin
            };
        }
    }
}
