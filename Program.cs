using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;


namespace Snake{

public partial class Program
{
    public static void Main()
    {
        char symbol = '#';
        Poly poly = new Poly(new Coordinate(1, 1), new Coordinate(20, 20), symbol);
        poly.Draw();

        Snake snake = new Snake(new Coordinate(5, 5), 3, '0');
        snake.Draw();

        while (true)
        {
            Console.Clear();
            snake.Move();
            snake.Draw();
            
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        snake.ChangeDirection(Direction.up);
                        break;
                    case ConsoleKey.DownArrow:
                        snake.ChangeDirection(Direction.down);
                        break;
                    case ConsoleKey.LeftArrow:
                        snake.ChangeDirection(Direction.left);
                        break;
                    case ConsoleKey.RightArrow:
                        snake.ChangeDirection(Direction.right);
                        break;
                }
            }

            snake.Move();
            snake.Draw();
            Thread.Sleep(150);
        }
        
    }

    public enum Direction
    {
        up,
        down,
        left,
        right
    }
    class Snake : Figure
    {
        private Direction direction;
        public Snake(Coordinate tail, int lenght, char symbol)
        {
            points = new List<Snake_point>();
            direction = Direction.up;
            for (int i = 0; i < lenght; i++)
            {
                var point = new Snake_point(tail.X, tail.Y, symbol);
                point.Move(i, direction);
                points.Add(point);

            }
        }

        public void Move()
        {
            Snake_point tail = points[points.Count - 1];
            Console.SetCursorPosition(tail.X, tail.Y);
            Console.Write(" ");
            Snake_point head = points[0];
            Snake_point NewHead = new Snake_point(head.X, head.Y, head.symbol);

            switch (direction)
            {
                case Direction.up:
                    NewHead = new Snake_point(head.X, head.Y - 1, head.symbol);
                    break;
                case Direction.down:
                    NewHead = new Snake_point(head.X, head.Y + 1, head.symbol);
                    break;
                case Direction.left:
                    NewHead = new Snake_point(head.X - 1, head.Y, head.symbol);
                    break;
                case Direction.right:
                    NewHead = new Snake_point(head.X + 1, head.Y, head.symbol);
                    break;
            }
            points.Insert(0, NewHead);
            points.RemoveAt(points.Count - 1);
        }

        public void ChangeDirection(Direction newDirection)
        {
            direction = newDirection;
        }

    }
}
}