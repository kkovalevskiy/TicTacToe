using System;
using System.Diagnostics.Contracts;

namespace TicTacToe.DomainModel
{
    public struct Size
    {
        private readonly int _width;
        private readonly int _height;

        public int Width
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > 0);
                return _width;
            }
        }

        public int Height
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > 0);
                return _height;
            }
        }

        public Size(int width, int height)
        {
            Contract.Requires<ArgumentException>(width > 0, "Width should be non negative");
            Contract.Requires<ArgumentException>(height > 0, "Height should be non negative");

            _width = width;
            _height = height;
        }
    }
}