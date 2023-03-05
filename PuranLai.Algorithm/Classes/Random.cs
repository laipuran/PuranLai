namespace PuranLai.Algorithms
{
    interface IRand
    {
        int[] GetInts();
    }

    public class Rand : IRand
    {
        public int Count;
        public int Max;

        public Rand(int count, int max)
        {
            Count = count;
            Max = max;
        }

        /// <summary>
        /// This method will get some random numbers within range and there is no reapeating
        /// </summary>
        /// <returns>An int array</returns>
        public int[] GetInts()
        {
            List<int> result = new List<int>();
            Dictionary<int, int> temp= new Dictionary<int, int>();

            Random random = new();
            while (temp.Count <= this.Count)
            {
                try
                {
                    temp.Add(random.Next(1, this.Max + 1), 0);
                }
                catch { }
            }

            foreach (var item in temp)
            {
                result.Add(item.Key);
            }
            result.Sort();
            return result.ToArray();
        }
    }

}
