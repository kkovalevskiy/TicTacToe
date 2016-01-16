namespace TicTacToe.DomainModel
{
    public struct Rectangle
    {
        public Position LeftTopPosition { get; }
        public Size Size { get; }

        public Rectangle(Position leftTopPosition, Size size)
        {
            LeftTopPosition = leftTopPosition;
            Size = size;
        }
    }
}