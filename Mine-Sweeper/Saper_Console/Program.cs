using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper_Console
{
    class Program
    {
        static void Main(string[] args)
        {

            string input = "5";
            while (!input.Equals("q"))
            {
                Console.WriteLine("Start new game by selecting a number:\n1 Easy\n2 Medium\n3 Expert\nq to Quit the game");

                BuildLevel bl = new BuildLevel();
                switch (input = Console.ReadLine())
                {
                    case "1":
                        bl.LevelSizeSelected = LevelSize.Easy;
                        break;
                    case "2":
                        bl.LevelSizeSelected = LevelSize.Medium;
                        break;
                    case "3":
                        bl.LevelSizeSelected = LevelSize.Expert;
                        break;
                    case "q":
                        input = "q";
                        break;
                    default:
                        Console.WriteLine("Please try again");
                        break;
                }
                if (!input.Equals("q"))
                {
                    bl.PrepareLevel();
                }

                //Console.Clear();
                while (!input.Equals("q"))
                {
                    try
                    {
                        Console.WriteLine("Horizontal number");
                        var x = Console.ReadLine();
                        Console.WriteLine("Vertical number");
                        var y = Console.ReadLine();
                        if (x.Equals("q") || y.Equals("q"))
                        {
                            input = "q";
                            break;
                        }
                        if (!bl.Update(Convert.ToInt32(x), Convert.ToInt32(y)))
                        {
                            break;
                        }
                        Console.WriteLine("\nPress 'q' to quit");
                    }
                    catch (SystemException s)
                    {
                        Console.WriteLine("Try again.\n" + s.Message);
                    }
                }
            }
            // bl.GameOver();

            /*Dictionary<string, Node> node = new Dictionary<string, Node>();
            node.Add("11", new Node { Value = 1, Revealed = false });
            node["11"].Revealed = true;
            Console.WriteLine("TEST " + node["11"].Revealed);*/
        }
        public void TestRecursion()
        {

        }
    }
}
