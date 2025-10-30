using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

public class Snake_point
{   
    public int X;
    public int Y;
    public char symbol;


    public Snake_point(int x, int y, char symbol)
    {
        this.X = x;
        this.Y = y;
        this.symbol = symbol;
    }

    public void Draw()
    {
        Console.SetCursorPosition(X, Y);
        Console.Write(symbol);
    }
}
public class Figure
{
    protected List<Snake_point> points = new List<Snake_point>();

    public void Draw()
    {
        foreach (Snake_point point in points)
        {
            point.Draw();
        }
    }

    public List<Snake_point> GetPoints()
    {
        return points;
    }
}
public class HorizontalLine : Figure
{



    public HorizontalLine(int x_start, int x_end, int y, char symbol)
    {
        points = new List<Snake_point>();

        for (int x = x_start; x <= x_end; x++)
        {
            points.Add(new Snake_point(x, y, symbol));
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }



        
            
        
    }
}
public class VerticalLine : Figure
{



    public VerticalLine(int y_start, int y_end, int x, char symbol)
    {
        points = new List<Snake_point>();

        for (int y = y_start; y <= y_end; y++)
        {

            points.Add(new Snake_point(x, y, symbol));
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }






    }
}

public class Coordinate
{
    public int X;
    public int Y;
    public Coordinate(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

public class Poly : Figure
{
    private HorizontalLine up;
    private HorizontalLine bottom;
    private VerticalLine left;
    private VerticalLine right;

    public Poly(Coordinate up_left, Coordinate bottom_right, char symbol)
    {
        up = new HorizontalLine(x_start: up_left.X, x_end: bottom_right.X, y: up_left.Y, symbol: symbol);
        bottom = new HorizontalLine(x_start: up_left.X, x_end: bottom_right.X, y: bottom_right.Y, symbol: symbol);
        left = new VerticalLine(y_start: up_left.Y, y_end: bottom_right.Y, x: up_left.X, symbol: symbol);
        right = new VerticalLine(y_start: up_left.Y, y_end: bottom_right.Y, x: bottom_right.X, symbol: symbol);
    }

    public new void Draw()
    {
        up.Draw();
        bottom.Draw();
        left.Draw();
        right.Draw();
    }
}




public class Program
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
