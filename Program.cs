using System;

Shtuka summa = (x, y) => Console.WriteLine($"{x} + {y} = {x+y}");
summa(1, 2);
delegate void Shtuka (int x, int y);