namespace PuranLai.Algorithms
{
    /// <summary>
    /// A structure of the Parse method
    /// </summary>
    public struct ParsingResult
    {
        public int number;
        public string message;

        public ParsingResult()
        {
            number = 0;
            message= string.Empty;
        }
    }

    public class Parse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">The string you want to parse int from.</param>
        /// <param name="max">The range you want to limit the parsed int.</param>
        /// <returns>A ParsingResult type struct</returns>
        /// <exception cref="CustomException"></exception>
        public static ParsingResult ParseFromString(string str, int max)
        {
            ParsingResult result = new();
            int number;
            try
            {
                number = int.Parse(str);
                if (number <= 0)
                {
                    throw new CustomException("Illegal input string!");
                }
                if (number > max)
                {
                    throw new CustomException("The input number is not within range!");
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

    }

    public class CustomException : Exception
    {
        /// <summary>
        /// An exception you can customize the message.
        /// </summary>
        /// <param name="message">The message you want to show.</param>
        public CustomException(string message) : base(message) { }
    }
}