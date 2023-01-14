using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuranLai.Algorithms
{
    interface IRand
    {
        int[] GetInts();
    }

    internal class Rand : IRand
    {
        public static int Count;
        public static int Max;

        public Rand(int count, int max) {
            Count = count;
            Max = max;
        }

        /// <summary>
        /// This method will get some random numbers within range and there is no reapeating
        /// </summary>
        /// <returns>An int array</returns>
        public int[] GetInts()
        {

            int[] array = new int[Count];
            int[] check = new int[Rand.Max];
            Array.Clear(array, 0, Count);
            Array.Clear(check, 0, Rand.Max);

            Random random = new();
            for (int i = 0; i < Count; i++)
            {
                int nextRandom = random.Next(1, Rand.Max + 1);
                check[nextRandom - 1]++;
                Thread.Sleep(5);
            }
            
            bool checkValue = true;
            while (checkValue == true)
            {
                for (int i = 0; i < Rand.Max; i++)
                {
                    if (check[i] > 1)
                    {
                        check[i]--;
                        int temp = random.Next(1, Rand.Max + 1);
                        check[temp - 1]++;
                    }
                }
                int index = 0;
                for (int i = 0; i < Max; i++)
                {
                    if (check[i] == 1 || check[i] == 0)
                    {
                        if (check[i] == 1)
                        {
                            array[index] = i + 1;
                            index++;
                        }
                        if (i == Max - 1)
                        {
                            checkValue = false;
                        }
                    }
                    else break;
                }

            }

            for (int i = Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp;
                        temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }

            return array;
        }
    }

}
