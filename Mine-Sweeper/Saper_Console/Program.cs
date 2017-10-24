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
            BuildLevel bl = new BuildLevel();
            bl.LevelSizeSelected = LevelSize.Easy;
            bl.PrepareLevel();
            //Console.Clear();
            while (true)
            {
                try
                {
                    int x = Convert.ToInt32(Console.ReadLine());
                    int y = Convert.ToInt32(Console.ReadLine());
                    if (!bl.Update(x, y))
                    {
                        break;
                    }
                }
                catch(SystemException s)
                {
                    Console.WriteLine("Try again.\n" + s.Message);
                }
            }
            Console.ReadLine();





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
