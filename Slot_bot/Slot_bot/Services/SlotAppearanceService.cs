using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_bot.Services
{
    public class SlotAppearanceService
    {
        public int[,] slotScreen;
        private readonly string[] slotElements = { ":cherries:", ":trophy:", ":black_joker:", ":8ball:", ":hamster:" };

        public void SpinSlot()
        {
            var random = new Random();
            slotScreen = new int[,] {
                { random.Next(5),random.Next(5),random.Next(5),random.Next(5) },
                { random.Next(5),random.Next(5),random.Next(5),random.Next(5) },
                { random.Next(5),random.Next(5),random.Next(5),random.Next(5) },
                { random.Next(5),random.Next(5),random.Next(5),random.Next(5) }
            };
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for(int i = 0; i < slotScreen.GetLength(0); i++)
            {
                stringBuilder.Append("\t\t\t\t");
                for (int j = 0; j < slotScreen.GetLength(0); j++)
                {
                    stringBuilder.Append(slotElements[slotScreen[i, j]] + "  ");
                }
                stringBuilder.Append("\n\n");
            }
            var result = stringBuilder.ToString();
            return stringBuilder.ToString();
        }
    }
}
