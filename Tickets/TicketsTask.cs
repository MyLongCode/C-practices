using System.Numerics;

namespace Tickets
{
    internal class TicketsTask
    {
        private const int Summ = 2000;
        private const int Length = 100;

        public static BigInteger Solve(int halfLen, int totalSum)
        {
            if (totalSum % 2 != 0)
                return 0;

            BigInteger[,] happyTicketsArray = InitializeTicketsContainerArray();
            BigInteger halfCount = CountHappyTickets(happyTicketsArray, halfLen, totalSum / 2);
            return halfCount * halfCount;
        }

        private static BigInteger[,] InitializeTicketsContainerArray()
        {
            var happyTicketsArray = new BigInteger[Length + 1, Summ + 1];
            for (var i = 0; i < Length; i++)
                for (var j = 0; j < Summ; j++)
                    happyTicketsArray[i, j] = -1;
            return happyTicketsArray;
        }

        private static BigInteger CountHappyTickets(BigInteger[,] happyTickets, int len, int sum)
        {
            if (happyTickets[len, sum] >= 0) return happyTickets[len, sum];
            if (sum == 0) return 1;
            if (len == 0) return 0;

            happyTickets[len, sum] = 0;
            for (var i = 0; i < 10; i++)
            {
                if (sum - i >= 0)
                {
                    happyTickets[len, sum] += CountHappyTickets(happyTickets, len - 1, sum - i);
                }
            }

            return happyTickets[len, sum];
        }
    }
}
