using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * In order to play the game you need to set  .LevelSizeSelected to desired level size and then call PrepareLevel() method.
 * To reveal position in the matrix call Update(<int value>, <int value>). Method takes x and y position from the user.
 */
namespace Saper_Console
{
    public enum LevelSize
    {
        Easy, Medium, Expert, Custom
    }
    class BuildLevel
    {
        const int easy = 10;
        const int medium = 20;
        const int expert = 40;
        private int custom = 2;
        private int levelSize = 0;
        private bool gameOver = false;
        public int Custom
        {
            get
            {
                return custom;
            }
            set
            {
                custom = value;
            }
        }
        public LevelSize LevelSizeSelected { get; set; }
        private int[] Sizes
        {
            get
            {
                return new int[] { easy, medium, expert, custom };
            }
        }
        //  <position, neighbour bombs>
        private Dictionary<string, Node> node = new Dictionary<string, Node>();
        /// <summary>
        /// Call this method to initialize the game. By default game will initialize to Easy 10x10
        /// </summary>
        public void PrepareLevel()
        {
            node.Clear();
            gameOver = false;
            for (int i = 0; i < Sizes.Length; i++)
            {
                if (LevelSizeSelected.Equals((LevelSize)i))
                {
                    levelSize = Sizes[i];
                    CreateLevel();
                    // CustomLevel();
                    DrawGame(0, 0);
                }
            }
        }
        /// <summary>
        /// Generates level by selected size and adds nodes to dictionary. Later we update the nodes by adding a number of bombs they are connected to.
        /// </summary>
        private void CreateLevel()
        {
           // Random rand = new Random();
            int maxBombs = levelSize;

            if(LevelSizeSelected.Equals(LevelSize.Medium))
            {
                maxBombs *= 2;
            }
            else if (LevelSizeSelected.Equals(LevelSize.Expert))
            {
                maxBombs *= 3;
            }
            for (int x = 1; x < levelSize + 1; x++)
            {
                for (int y = 1; y < levelSize + 1; y++)
                {
                    // Note: the ':' is used to avoid duplicate keys as 1:11 and 11:1 would be the same
                    node.Add(x + ":" + y, new Node { Value = 0, Revealed = false });
                }
            }
            GenerateBombs(maxBombs);
            GenerateMineProximity();
            DrawGame(0, 0);
        }
        /// <summary>x + ":" + y
        /// Each node checks surrounding nodes for bombs, if neighbor node contains a bomb this node will increase its value by 1.
        /// </summary>
        private void GenerateMineProximity()
        {
            string tempNode = null;
            for (int x = 1; x < levelSize + 1; x++)
            {
                for (int y = 1; y < levelSize + 1; y++)
                {
                    if (!node[x + ":" + y].Value.Equals(-1))
                    {
                        // Check Top-Left
                        tempNode = (x - 1) + ":" + (y - 1);
                        if (node.ContainsKey(tempNode))
                        {
                            if (node[tempNode].Value.Equals(-1))
                            {
                                node[x + ":" + y].Value += 1;
                            }
                        }
                        // Check Top
                        tempNode = (x - 1) + ":" + y;
                        if (node.ContainsKey(tempNode))
                        {
                            if (node[tempNode].Value.Equals(-1))
                            {
                                node[x + ":" + y].Value += 1;
                            }
                        }
                        // Check Top-Right
                        tempNode = (x - 1) + ":" + (y + 1);
                        if (node.ContainsKey(tempNode))
                        {
                            if (node[tempNode].Value.Equals(-1))
                            {
                                node[x + ":" + y].Value += 1;
                            }
                        }
                        // Check Right
                        tempNode = x + ":" + (y + 1);
                        if (node.ContainsKey(tempNode))
                        {
                            if (node[tempNode].Value.Equals(-1))
                            {
                                node[x + ":" + y].Value += 1;
                            }
                        }
                        // Check Bottom-Right
                        tempNode = (x + 1) + ":" + (y + 1);
                        if (node.ContainsKey(tempNode))
                        {
                            if (node[tempNode].Value.Equals(-1))
                            {
                                node[x + ":" + y].Value += 1;
                            }
                        }
                        // Check Bottom
                        tempNode = (x + 1) + ":" + y;
                        if (node.ContainsKey(tempNode))
                        {
                            if (node[tempNode].Value.Equals(-1))
                            {
                                node[x + ":" + y].Value += 1;
                            }
                        }
                        // Check Bottom-Left
                        tempNode = (x + 1) + ":" + (y - 1);
                        if (node.ContainsKey(tempNode))
                        {
                            if (node[tempNode].Value.Equals(-1))
                            {
                                node[x + ":" + y].Value += 1;
                            }
                        }
                        // Check Left
                        tempNode = x + ":" + (y - 1);
                        if (node.ContainsKey(tempNode))
                        {
                            if (node[tempNode].Value.Equals(-1))
                            {
                                node[x + ":" + y].Value += 1;
                            }
                        }
                    }
                }
            }
            //DictionaryVerify();
        }
        /*private void DictionaryVerify()
        {
            int index = 0;
            foreach (KeyValuePair<string, Node> dt in node)
            {
                index++;
                Console.Write(dt.Key + " " + dt.Value + "\t");
                if (index == 10)
                {
                    Console.WriteLine();
                    index = 0;
                }
            }
        }*/
        /// <summary>
        /// Places bombs in random positions. Bomb is indicated by value -1. Node that contains a bomb is updated with -1 value.
        /// </summary>
        /// <returns>Max bombs is an indication of how many bombs are left</returns>
        
        // TODO: Make sure that there is always enough bombs, if necessary re-run bomb generation to get maxBombs = 0;
        private int GenerateBombs(int maxBombs)
        {
            Random rand = new Random();
            int x, y;
            while(maxBombs-- > 0)
            {
                do
                {
                    x = rand.Next(1, levelSize+1);
                    y = rand.Next(1, levelSize+1);
                }
                while(node[x + ":" + y].Value == -1);

                node[x + ":" + y].Value = -1;
            }

            return maxBombs;
        }

        // Takes player's input and reveals desired node
        // TODO: Implement method for marking bombs
        public bool Update(int inputX, int inputY)
        {
            string key = inputX + ":" + inputY;
            if (node.ContainsKey(key) && !node[key].Revealed)
            {
                if (node[key].Value.Equals(-1))
                {
                    GameOver();
                    // TODO: Implement a method to show the whole game + include player input
                    return false;
                }
                else if (node[key].Value.Equals(0))
                {
                    // Check for the node's key and if the position is empty (0) reveal the node and its neighbor
                    CheckNeighbourNodes(inputX, inputY, 0);
                }
                else
                {
                    node[key].Revealed = true;
                    DrawGame(inputX, inputY);
                }
            }
            else
            {
                GameText("Please try again, invalid position");
            }
            DrawGame(0, 0);
            return true;
        }

        // Render the game in the console
        private void DrawGame(int inputX, int inputY)
        {
            Console.Clear();
            for (int x = 0; x < levelSize + 1; x++)
            {
                for (int y = 0; y < levelSize + 1; y++)
                {
                    // Draw outer table
                    if ((x == 0) || (y == 0))
                    {
                        Console.Write("{0},{1}\t", x, y);
                    }
                    else
                    {
                        // Draw inner table
                        if (gameOver)
                        {
                            Console.Write("   {0}\t", node[x + ":" + y]);
                        }
                        else if ((x == inputX) && (y == inputY))
                        {
                            Console.Write("   {0}\t", node[inputX + ":" + inputY]);
                        }
                        else if (node[x + ":" + y].Revealed)
                        {
                            Console.Write("   {0}\t", node[x + ":" + y]);
                        }
                        else
                        {
                            Console.Write("   *   \t");
                        }
                    }
                }
                Console.WriteLine("\n");
            }
        }

        private void GameText(string text)
        {
            Console.WriteLine(text);
        }
        /// <summary>
        /// If the node is empty revel its position and other neighbor nodes that are also empty + taken nodes that are in exact contact with empty nodes
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="condition"></param>
        private void CheckNeighbourNodes(int x, int y, int condition)
        {
            string tempNode = null;
            int takenOrEmpty = condition;

            // Reveal this node as its empty
            node[x + ":" + y].Revealed = true;

            // Check Top
            tempNode = (x - 1) + ":" + y;
            if (node.ContainsKey(tempNode))
            {
                if (node[tempNode].Value.Equals(0) && !node[tempNode].Revealed)
                {
                    CheckNeighbourNodes((x - 1), y, 0);
                }
                else if(!node[tempNode].Value.Equals(0) && !node[tempNode].Revealed && takenOrEmpty == 0)
                {
                    CheckNeighbourNodes((x - 1), y, 1);
                }
            }
            // Check Right
            tempNode = x + ":" + (y + 1);
            if (node.ContainsKey(tempNode))
            {
                if (node[tempNode].Value.Equals(0) && !node[tempNode].Revealed)
                {
                    CheckNeighbourNodes(x, (y + 1), 0);
                }
                else if (!node[tempNode].Value.Equals(0) && !node[tempNode].Revealed && takenOrEmpty == 0)
                {
                    CheckNeighbourNodes(x, (y + 1), 1);
                }
            }
            // Check Bottom
            tempNode = (x + 1) + ":" + y;
            if (node.ContainsKey(tempNode))
            {
                if (node[tempNode].Value.Equals(0) && !node[tempNode].Revealed)
                {
                    CheckNeighbourNodes((x + 1), y, 0);
                }
                else if (!node[tempNode].Value.Equals(0) && !node[tempNode].Revealed && takenOrEmpty == 0)
                {
                    CheckNeighbourNodes((x + 1), y, 1);
                }
            }
            // Check Left
            tempNode = x + ":" + (y - 1);
            if (node.ContainsKey(tempNode))
            {
                if (node[tempNode].Value.Equals(0) && !node[tempNode].Revealed)
                {
                    CheckNeighbourNodes(x, (y - 1), 0);
                }
                else if (!node[tempNode].Value.Equals(0) && !node[tempNode].Revealed && takenOrEmpty == 0)
                {
                    CheckNeighbourNodes(x, (y - 1), 1);
                }
            }
        }

        public void GameOver()
        {
            GameText("Game Over");
            gameOver = true;
            DrawGame(0, 0);
        }
    }
}
