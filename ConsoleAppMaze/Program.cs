using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ConsoleAppMaze.Class;


namespace ConsoleAppMaze {
    class Program {
        static Stopwatch stopwach = new Stopwatch();
        static void Main() 
            {
            SearchPath puzzle = new SearchPath();
            stopwach.Start();
            puzzle.CreatedMaze();
            puzzle.PrintMaze();
            //stopwach.Start(); Desde aquí tarda 0 ms, por lo que analizo el tiempo desde puzzle.CreatedMaze();
            puzzle.BFS();
            puzzle.CreatePath();
            stopwach.Stop();
            puzzle.PrintSolution();
            Console.WriteLine("Time BFS: " + stopwach.ElapsedMilliseconds + " ms");
            Console.ReadKey();
        }
    }
}
