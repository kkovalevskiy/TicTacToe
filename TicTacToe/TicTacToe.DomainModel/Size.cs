using System;
using System.Diagnostics.Contracts;
using EnsureThat;

namespace TicTacToe.DomainModel
{
    public struct Size
    {
        public int Width { get; }

        public int Height { get; }

        public Size(int width, int height)
        {
            Ensure.That(width, nameof(width)).IsGt(0);
            Ensure.That(height, nameof(height)).IsGt(0);
            
            Width = width;
            Height = height;
        }
    }
}