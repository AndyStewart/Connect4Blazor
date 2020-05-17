using System;
using System.Linq;
using Xunit;

namespace Connect4.Tests
{
    public class GameTests
    {
        [Fact]
        public void PlayerRedTurnIsFirst()
        {
            var game = new Game();
            Assert.Equal("Red", game.CurrentTurn);
        }

        [Fact]
        public void PlayerRedPlacesCoinInCol0()
        {
            var game = new Game();
            game.Select(0);
            Assert.Equal("Red", game.Find(0, 0).Color);
        }

        [Fact]
        public void PlayerYellowGoesAfterPlayerRed()
        {
            var game = new Game();
            game.Select(0);
            Assert.Equal("Yellow", game.CurrentTurn);
        }

        [Fact]
        public void PlayReturnsToRedPlayerAfterYellowPlayer()
        {
            var game = new Game();
            game.Select(0);
            game.Select(0);
            Assert.Equal("Red", game.CurrentTurn);
        }

        [Fact]
        public void PositionIsYellowWhenYellowPlayerPlacesCoin()
        {
            var game = new Game();
            game.Select(0);
            game.Select(1);
            Assert.Equal("Yellow", game.Find(1, 0).Color);
        }

        [Fact]
        public void PlacingInColumnContainingCellPlacesCoinOntopOfThatCoin()
        {
            var game = new Game();
            game.Select(0);
            game.Select(0);
            Assert.Equal("Yellow", game.Find(0, 1).Color);
        }

        [Fact]
        public void GameDoesntChangeTurnForInvalidMove()
        {
            var game = new Game();
            game.Select(0);
            game.Select(0);
            game.Select(0);
            game.Select(0);
            game.Select(0);
            game.Select(0);
            game.Select(0);
            Assert.Equal("Red", game.CurrentTurn);
        }

        [Fact]
        public void FirstToGet4InARowWins()
        {
            var game = new Game();
            for (var i = 0; i < 4; i++)
            {
                game.Select(0);
                game.Select(1);
            }

            Assert.True(game.Winner);
            Assert.Equal("Red", game.CurrentTurn);
        }

        [Fact]
        public void NoCoinsInARowNoOneWins()
        {
            var game = new Game();
            Assert.False(game.Winner);
        }

        [Fact]
        public void LessThan4InARowNoOneWins()
        {
            var game = new Game();
            for (var i = 0; i < 3; i++)
            {
                game.Select(0);
                game.Select(1);
            }

            Assert.False(game.Winner);
        }
    }
}