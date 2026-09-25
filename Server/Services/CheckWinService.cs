using Server.Entities;
using Server.Enum;

namespace Server.Services
{
    public class CheckWinService(List<Cell> Maps)
    {
        bool StepsCheck(Cell data, int step)
        {
            var clickedIndex = Maps.IndexOf(data);
            var LeftnRightIndex = clickedIndex;
            int countLine = 1;
            // right
            while (LeftnRightIndex >= 0)
            {
                LeftnRightIndex = LeftnRightIndex + step;
                if ((LeftnRightIndex < Maps.Count && LeftnRightIndex >= 0) && Maps[clickedIndex].Value != Status.None)
                {
                    if (Maps[LeftnRightIndex].Value == Maps[clickedIndex].Value)
                    {
                        countLine++;
                    }
                    else break;
                }
                else break;

            }
            LeftnRightIndex = clickedIndex;
            while (LeftnRightIndex >= 0)
            {
                LeftnRightIndex = LeftnRightIndex - step;
                if ((LeftnRightIndex < Maps.Count && LeftnRightIndex >= 0) && Maps[clickedIndex].Value != Status.None)
                {
                    if (Maps[LeftnRightIndex].Value == Maps[clickedIndex].Value)
                    {
                        countLine++;
                    }
                    else break;
                }
                else break;

            }
            if (countLine >= 5)
            {
                return true;
            }
            return false;
        }
        bool CheckDiagonalRight(Cell data, int step)
        {
            var clickedIndex = Maps.IndexOf(data);
            var LeftnRightIndex = clickedIndex;
            int countLine = 1;
            // top right
            while (LeftnRightIndex >= 0)
            {
                LeftnRightIndex = LeftnRightIndex - step + 1;
                if ((LeftnRightIndex < Maps.Count && LeftnRightIndex >= 0) && Maps[clickedIndex].Value != Status.None)
                {
                    if (Maps[LeftnRightIndex].Value == Maps[clickedIndex].Value)
                    {
                        countLine++;
                    }
                    else break;
                }
                else break;

            }
            // bottom left
            LeftnRightIndex = clickedIndex;
            while (LeftnRightIndex >= 0)
            {
                LeftnRightIndex = LeftnRightIndex + step - 1;
                if ((LeftnRightIndex < Maps.Count && LeftnRightIndex >= 0) && Maps[clickedIndex].Value != Status.None)
                {
                    if (Maps[LeftnRightIndex].Value == Maps[clickedIndex].Value)
                    {
                        countLine++;
                    }
                    else break;
                }
                else break;

            }
            if (countLine >= 5)
            {
                return true;
            }
            return false;
        }
        bool CheckDiagonalLeft(Cell data, int step)
        {
            var clickedIndex = Maps.IndexOf(data);
            var LeftnRightIndex = clickedIndex;
            int countLine = 1;
            // top left
            while (LeftnRightIndex >= 0)
            {
                LeftnRightIndex = LeftnRightIndex - step - 1;
                if ((LeftnRightIndex < Maps.Count && LeftnRightIndex >= 0) && Maps[clickedIndex].Value != Status.None)
                {
                    if (Maps[LeftnRightIndex].Value == Maps[clickedIndex].Value)
                    {
                        countLine++;
                    }
                    else break;
                }
                else break;

            }
            // bottom right
            LeftnRightIndex = clickedIndex;
            while (LeftnRightIndex >= 0)
            {
                LeftnRightIndex = LeftnRightIndex + step + 1;
                if ((LeftnRightIndex < Maps.Count && LeftnRightIndex >= 0) && Maps[clickedIndex].Value != Status.None)
                {
                    if (Maps[LeftnRightIndex].Value == Maps[clickedIndex].Value)
                    {
                        countLine++;
                    }
                    else break;
                }
                else break;

            }
            if (countLine >= 5)
            {
                return true;
            }
            return false;

        }
        public bool CheckWin(Cell data)
        {
            if (StepsCheck(data, 1) || StepsCheck(data, 25) || CheckDiagonalRight(data, 25) || CheckDiagonalLeft(data, 25)) return true;
            return false;

        }
    }
}
