# PuranLai.Algorithm

## Contains
- Animation.cs
- Parse.cs
- Random.cs

## Namespaces
- PuranLai.Algorithms

## Classes
- Rand
- Parse
- Animation
- AnimationPool
- AnimationQueue(Not tested yet)

## Struct
- ParsingResult

## Exception
- CustomException

## Methods
- Task<bool>	Animation.StartAnimationAsync()
- double		GetLinearValue(double span, Animation animation)
- double		GetSineValue(double span, Animation animation)
- int			ParseFromString(string str, int max)
- int[]			Rand.GetInts();
