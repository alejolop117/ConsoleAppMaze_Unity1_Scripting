using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMaze.Class {
    class SearchPath {
        Node startingPoint = null;
        Node endingPoint = null;
        

        Dictionary<Vector2Int, Node> block = new Dictionary<Vector2Int, Node>();
        Vector2Int[] directions = { new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1),
            new Vector2Int(-1, 0) }; // Up = <0,1> || right = <1,0> || down = <0,-1> || left = <-1,0>

        Queue<Node> queue = new Queue<Node>();
        Node searchingPoint;
        bool isExploring = true;
        List<Node> path = new List<Node>();
        public void CreatedMaze() {  
            
          for (ushort i = 0; i < 12; i++) {  //Creo un mapa 10x10 || i = X values j = Y values
                for (ushort j = 0; j< 12; j++) {
                    Node nodeA = new Node(i, j);
                    Vector2Int vectorNodeA = new Vector2Int(i, j);
                    if (nodeA.X == 0 && nodeA.Y == 3) {
                        startingPoint = nodeA;
                        block.Add(vectorNodeA, startingPoint);
                    }
                    else if(nodeA.X == 11 && nodeA.Y == 10) {
                        endingPoint = nodeA;
                        block.Add(vectorNodeA, endingPoint);
                    }
                    else block.Add(vectorNodeA, nodeA);
                }
          }
            

            //  *** Remuevo los nodos para crear los espcios vacíos ***
            block.Remove(new Vector2Int(3, 1));
            block.Remove(new Vector2Int(1, 5));

            for (ushort i = 0; i < 3; i++) { 
                block.Remove(new Vector2Int(0, i + 6));
            }
            
            for (ushort i = 0; i < 3; i++) {
                block.Remove(new Vector2Int(3, i + 3));
            }

            for (ushort i = 0; i < 3; i++) {
                block.Remove(new Vector2Int(6, i + 2));
            }

            for (ushort i = 0; i < 4; i++) {
                block.Remove(new Vector2Int(9, i));
            }

            for (ushort i = 0; i < 4; i++) {
                block.Remove(new Vector2Int(i+2, 8));
            }

            for (ushort i = 0; i < 3; i++) {
                block.Remove(new Vector2Int(i + 6, 6));
            }
            //-------------------------------------------------------------------------------------//
        }
        public void BFS() { // Creo el Breadth First Search
            queue.Enqueue(startingPoint); //Pongo en la cola el nodo inicial
            while (queue.Count >0 && isExploring) {
                searchingPoint = queue.Dequeue();  // Saco el nodo inicial de la cola y la salvo en searchingPoint
                OnReachingEnd(); //Comprobamos si hemos llegado al final
                ExploreNeighbourNodes();
            }
        }

        void OnReachingEnd() {
            if (searchingPoint == endingPoint) isExploring = false;
            else isExploring = true;
        }

        public void ExploreNeighbourNodes() {
            if (!isExploring) { return; }
            foreach (Vector2Int direction in directions) {
                Vector2Int neightbourPos =
                    (new Vector2Int(direction.X + searchingPoint.X, direction.Y + searchingPoint.Y));
                if (block.ContainsKey(neightbourPos)) {
                    Node node = block[neightbourPos];
                    if (!node.IsExplored) {
                        queue.Enqueue(node);
                        node.IsExplored = true;
                        node.IsExploredFrom = searchingPoint;
                    }
                }
            }
        }
        
        public void CreatePath() {

            SetPath(endingPoint);
            Node previousNode = endingPoint.IsExploredFrom; 

            while (previousNode != startingPoint) {
                
                SetPath(previousNode);
                previousNode = previousNode.IsExploredFrom;
            }

            SetPath(startingPoint);
            path.Reverse();
        }
        void SetPath(Node node) {
            path.Add(node);
        }
        public void PrintSolution() {
            Console.WriteLine("Solution Path: ");
            for(ushort i = 0; i < path.Count; i++) {
                Console.WriteLine("<" + path[i].X + "," + path[i].Y + ">");
            }
        }
        public void PrintMaze() {
            Console.WriteLine("MAZE");
            for (ushort i = 0; i < 12; i++) {
                for (ushort j = 0; j < 12; j++) {
                    Vector2Int maze = new Vector2Int(j, i);
                    if (block.ContainsKey(maze)) {
                        Console.Write("O");
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                Console.WriteLine(" ");
            }
        }
    }
     //  ******************   OTRAS CLASES NECESARIAS   *********************************
    class Node {
        public bool IsExplored = false;
        public Node IsExploredFrom;

        public int X { set; get;}
        public int Y { set; get;}

        public Node(int x, int y) {
            X = x;
            Y = y;
        }
    }

    struct Vector2Int {

        public int X { set; get; }
        public int Y { set; get; }

        public Vector2Int(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
