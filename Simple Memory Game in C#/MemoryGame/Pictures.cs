using System;

namespace MemoryGame
{
    class Pictures
    {
        Random random;  

        public Pictures()
        {
            random = new Random();  
        }

         
        internal string[] shuffleArray(string[] items)
        {
            int number;  
            string temp;  

            for (int y = 0; y < items.Length; y++)
            {
                number = random.Next(23);  

                 
                 
                temp = items[y];
                items[y] = items[number];
                items[number] = temp;
            }

            return items;
        }
    }
}
