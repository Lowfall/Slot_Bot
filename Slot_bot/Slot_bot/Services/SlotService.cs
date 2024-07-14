using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_bot.Services
{
    public class SlotService
    {
        public int[,] slotScreen;
        private readonly string[] slotElements = { ":cherries:", ":trophy:", ":black_joker:", ":8ball:", ":hamster:" };

        public void SpinSlot()
        {
            var random = new Random();
            slotScreen = new int[,] {
                { random.Next(5),random.Next(5),random.Next(5),random.Next(5),random.Next(5)},
                { random.Next(5),random.Next(5),random.Next(5),random.Next(5),random.Next(5)},
                { random.Next(5),random.Next(5),random.Next(5),random.Next(5),random.Next(5)}
            };
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for(int i = 0; i < slotScreen.GetLength(0); i++)
            {
                stringBuilder.Append("\t\t\t\t");
                for (int j = 0; j < slotScreen.GetLength(1); j++)
                {
                    stringBuilder.Append(slotElements[slotScreen[i, j]] + "  ");
                }
                stringBuilder.Append("\n\n");
            }
            var result = stringBuilder.ToString();
            return stringBuilder.ToString();
        }


        public int IsWinningCombination(int bid)
        {
            for (int row = 0; row < slotScreen.GetLength(0); row++)
            {
                if (IsWinningLine(slotScreen, row, 0, 0, 1))
                {
                    bid += 10;
                }
            }

            for (int col = 0; col < slotScreen.GetLength(1); col++)
            {
                if (IsWinningLine(slotScreen, 0, col, 1, 0))
                {
                    bid += 20;
                }
            }

            if (IsWinningLine(slotScreen, 0, 0, 1, 1))
            {
                bid += 100;
            }

            if (IsWinningLine(slotScreen, 0, slotScreen.GetLength(1) - 1, 1, -1))
            {
                bid += 100;
            }

            if (IsJackpot(slotScreen, 0)){
                return bid * 1000000;
            }

            return bid;
        }

        private bool IsWinningLine(int[,] slotScreen, int startRow, int startCol, int rowStep, int colStep)
        {
            int firstValue = slotScreen[startRow, startCol];
            int row = startRow + rowStep;
            int col = startCol + colStep;

            while (row < slotScreen.GetLength(0) && col < slotScreen.GetLength(1) && col >= 0)
            {
                if (slotScreen[row, col] != firstValue)
                {
                    return false;
                }
                row += rowStep;
                col += colStep;
            }

            return true;
        }

        private bool IsJackpot(int[,] slotScreen,int bid)
        {
            var element = slotScreen[0, 0];
            foreach(var item in slotScreen)
            {
                if(element != item)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
