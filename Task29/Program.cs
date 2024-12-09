using MyLib;

namespace Task29
{
    class Project
    {

        static void Main()
        {
            MyGraph g = new MyGraph(5);

            g.AddEdge(1, 0);
            g.AddEdge(0, 2);
            g.AddEdge(2, 1);
            g.AddEdge(0, 3);
            g.AddEdge(3, 4);

            int[][] SCC = g.TarjanAlgoritm();
            for (int i = 0; i < SCC.Length; i++) { 
                for (int j = 0; j < SCC[i].Length; j++)
                {
                    Console.Write(SCC[i][j] + " ");
                }
                Console.WriteLine();
            }

            MyNetwork network = new MyNetwork(6);
            network.AddEdge(0, 1, 16);
            network.AddEdge(0, 2, 13);
            network.AddEdge(1, 2, 10);
            network.AddEdge(1, 3, 12);
            network.AddEdge(2, 1, 4);
            network.AddEdge(2, 4, 14);
            network.AddEdge(3, 2, 9);
            network.AddEdge(3, 5, 20);
            network.AddEdge(4, 3, 7);
            network.AddEdge(4, 5, 4);

            Console.WriteLine("Maximum flow " + network.MethodDinic(0, 5));

            MyGraph g2 = new MyGraph(5);
            g2.AddEdge(0, 1);
            g2.AddEdge(1, 2);
            g2.AddEdge(2, 0);
            g2.AddEdge(1, 3);
            g2.AddEdge(3, 4);

            int[] maxClique = g2.MaxClique();

            Console.WriteLine("Максимальная клика: ");
            for (int i = 0; i < maxClique.Length; i++) Console.WriteLine(maxClique[i]);
        }
    }
}