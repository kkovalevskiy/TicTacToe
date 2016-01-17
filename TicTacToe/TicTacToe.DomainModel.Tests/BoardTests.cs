using System;
using System.Linq;
using MoreLinq;
using NUnit.Framework;

namespace TicTacToe.DomainModel.Tests
{
    [TestFixture]
    public class BoardTests
    {
        private readonly Size _defaultSize = new Size(3, 4);

        [Test]
        public void ParameterlessCtor_ShouldCreateInfiniteBoard()
        {
            var board = CreateInifiniteBoard();
            Assert.That(board.Size, Is.Null);
            Assert.That(board.IsInfinite);
        }

        [Test]
        public void CtorWithSize_ShouldCreateLimitedBoard()
        {
            var board = CreateLimitedBoard();
            Assert.That(board.Size, Is.EqualTo(_defaultSize));
            Assert.That(!board.IsInfinite);
        }

        [Test]
        public void SetPlayerSymbol_ShouldSetValue()
        {
            var board = CreateInifiniteBoard();
            var position = new Position(1, 1);

            board.SetPlayerSymbol(position, PlayerSymbol.Cross);

            var value = board.GetPlayerSymbol(position);
            Assert.That(value, Is.EqualTo(PlayerSymbol.Cross));
        }

        [Test]
        [TestCase(-1, 0)]
        [TestCase(4, 0)]
        [TestCase(0, -1)]
        [TestCase(0, 4)]
        public void SetPlayerSymbol_BoardIsLimitedAndPositionIsOutOfBoundary_ShouldThrow(int x, int y)
        {
            var board = CreateLimitedBoard();
                      
            Assert.Throws<ArgumentException>(() => board.SetPlayerSymbol(new Position(x, y), PlayerSymbol.Zero));            
        }
        
        [Test]
        public void SetPlayerSymbol_CellAlreadyBusy_ShouldThrow()
        {
            var board = CreateInifiniteBoard();
            board.SetPlayerSymbol(new Position(1, 1), PlayerSymbol.Zero);

            Assert.Throws<InvalidOperationException>(
                () => board.SetPlayerSymbol(new Position(1, 1), PlayerSymbol.Cross));
        }

        [Test]
        public void GetPlayerSymbol_CellIsEmpty_ShouldReturnNull()
        {
            var board = CreateInifiniteBoard();

            var result = board.GetPlayerSymbol(new Position(1, 1));

            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase(-1, 0)]
        [TestCase(4, 0)]
        [TestCase(0, -1)]
        [TestCase(0, 4)]
        public void GetPlayerSymbol_BoardIsLimitedAndPositionIsOutOfBoundary_ShouldThrow(int x, int y)
        {
            var board = CreateLimitedBoard();

            Assert.Throws<ArgumentException>(() => board.GetPlayerSymbol(new Position(x, y)));
        }

        [Test]
        public void GetBusyCells_ShouldReturnBusyCells()
        {
            var board = CreateInifiniteBoard();
            var positions = new[] {new Position(1, 2), new Position(3, 4), new Position(5, 6)};
            positions.ForEach(p => board.SetPlayerSymbol(p, PlayerSymbol.Cross));

            var busyCells = board.BusyCells.ToArray();
            
            Assert.That(busyCells, Is.EquivalentTo(positions));
        }

        private Board CreateInifiniteBoard()
        {
            return new Board();
        }

        private Board CreateLimitedBoard()
        {
            return new Board(_defaultSize);
        }
    }
}