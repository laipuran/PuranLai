# A package used to parse int from a string, and get random number(s)
### This Package Contains:
#### struct ParsingResult
#### class Algorithm
- ParsingResult ParseIntFromString(string str, int max)
- int[] GetRandomNumbers(int count, int max)
- string GetRandomResult(int count, int max, char separator)
- string GetRandomResult(int count, int max, char separator, string prefix)
#### class CustomException : Exception
- CustomException(string message) : base(message)