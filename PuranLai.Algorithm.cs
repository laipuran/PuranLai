namespace PuranLai
{
    public struct ParsingResult
    {
        public int number = 0;
        public string message = "";

        public ParsingResult()
        {
        }
    }
    
    public class Algorithm
    {
        public static ParsingResult ParseIntFromString(string str, int max)
        {
            ParsingResult result = new();
            int number;
            try
            {
                number = int.Parse(str);
                if (number <= 0)
                {
                    throw new CustomException("输入的数字不合法！");
                }
                if (number > max)
                {
                    throw new CustomException("输入的数字超过最大值！");
                }
            }
            catch (Exception Ex)
            {
                result.message = Ex.Message;
                return result;
            }
            result.number = int.Parse(str);
            return result;
        }
        
        public static int[] GetRandomNumbers(int count, int max)
        {

            int[] array = new int[count];
            int[] check = new int[max];
            Array.Clear(array, 0, count);
            Array.Clear(check, 0, max);

            Random random = new();
            for (int i = 0; i < count; i++)
            {
                int nextRandom = random.Next(1, max + 1);
                check[nextRandom - 1]++;
                Thread.Sleep(5);
            }

            bool checkValue = true;
            while (checkValue == true)
            {
                for (int i = 0; i < max; i++)
                {
                    if (check[i] > 1)
                    {
                        check[i]--;
                        int temp = random.Next(1, max + 1);
                        check[temp - 1]++;
                    }
                }
                int index = 0;
                for (int i = 0; i < max; i++)
                {
                    if (check[i] == 1 || check[i] == 0)
                    {
                        if (check[i] == 1)
                        {
                            array[index] = i + 1;
                            index++;
                        }
                        if (i == max - 1)
                        {
                            checkValue = false;
                        }
                    }
                    else break;
                }

            }

            for (int i = count - 1; i > 0; i--)
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

        public static string GetRandomResult(int count, int max, char separator)
        {
            int[] array = GetRandomNumbers(count, max);
            return string.Join(separator, array);
        }

        public static string GetRandomResult(int count, int max, char separator, string prefix)
        {
            int[] array = GetRandomNumbers(count, max);
            return prefix + separator + string.Join(separator, array);
        }
    }
    
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}