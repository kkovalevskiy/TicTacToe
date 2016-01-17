using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using EnsureThat;

namespace TicTacToe.DomainModel
{
    public class Board
    {
        private readonly IDictionary<Position, PlayerSymbol> _busyCells = new Dictionary<Position, PlayerSymbol>();

        public Board()
        {
        }

        public Board(Size size)
        {
            Size = size;
        }
        
        public Size? Size { get; }

        public bool IsInfinite => !Size.HasValue;

        public void SetPlayerSymbol(Position position, PlayerSymbol value)
        {
            EnsurePositionIsCorrect(position);
            
            if (_busyCells.ContainsKey(position))
            {
                throw new InvalidOperationException($"Position {position} is already busy");
            }

            _busyCells[position] = value;
        }

        public PlayerSymbol? GetPlayerSymbol(Position position)
        {
            EnsurePositionIsCorrect(position);

            PlayerSymbol result;
            if (_busyCells.TryGetValue(position, out result))
            {
                return result;
            }
            
            return null;
        }

        public IEnumerable<Position> BusyCells => _busyCells.Keys;

        private void EnsurePositionIsCorrect(Position position)
        {
            if (!Size.HasValue) return;
            Ensure.That(position.X, () => position.X).IsInRange(0, Size.Value.Width - 1);
            Ensure.That(position.Y, () => position.Y).IsInRange(0, Size.Value.Height - 1);
        }
    }
}