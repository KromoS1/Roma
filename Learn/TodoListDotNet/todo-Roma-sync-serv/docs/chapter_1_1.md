## Chapter 1.1. CLI polishing. Validation
### Motivation
We need to add validation for arguments and subject and throw exceptions if the validation fails.
### Goal
Add arguments and subject validation and write unit tests for it
### Take a look:
#### Test-driven development and unit-tests
At first we write tests for new functionality we want to add, then implement it (make unit-tests green) and after it we're free for refactoring without a chance of breaking unit-tests
#### Unit-tests naming convention:
We already have these **unit** tests:
* `Parse_WhenArgumentsAreEmpty_ShouldThrowException`
* `Parse_WhenDontMatchAnyCommand_ShouldThrowException`
* `Parse_WhenMatchArgumentsByLongNameWithoutValue_ShouldFillArguments`

As you can see all of them are named in such manner: `%TestedMethodName%_When%Condition%_Should%ExpectedResult%`

Write new tests for validation of key value and subject with this naming convention.

#### Named tuples (C# 7.0 feature):
Sometimes you need to return more than 1 value from method. In order to do that you always could use **out** argument modifier or just return complex object like **class** or **struct**.

In C# 7.0 there were introduced new feature called **named tuples**

Take a look at this code:
```
private (TokenType type, string value) ExtractToken(string text)
```
how this method is called (deconstruction):
```
var (type, value) = ExtractToken(cliArguments[index]);
```
It's how you can return multible methods using it.

More about it you can read [here (RU)](https://habr.com/ru/post/345376/)

#### Ranges (C# 8.0 feature)
You can return ranges of arrays by using dotted syntax. Take a look at this code:
```
return (TokenType.LongKey, text[2..]);
```
It's an example of creating subarray that skips two first elements from source array.

More about it you can read [here (RU)](https://andrey.moveax.ru/post/csharp-features-v8-0-indexes-and-ranges)

#### Local functions (C# 7.0 feature)
Take a look at this code:
```
void FlushCurrentArgument(string? v)
{
    arguments.Add(currentArgument.Name, v);
    currentArgument = null;
}
```
This method is defined inside another method and has vision over all variables of parent method. It can be convenient for writing small pieces of reusable code that makes sense only for parent method.

More about it you can read [here (RU)](https://metanit.com/sharp/tutorial/2.20.php)

#### Discards (C# 7.0 feature)
Take a look at this code:
```
WithValueExpected(x => int.TryParse(x, out _))
```
In this case we don't care about int value of string, we just need to know whether **x** can be parsed to **int** or not. So we just discard the value by naming it as **_**.
Also it works with deconstructing of named tuples:
```
(_, _, area) = city.GetCityInformation(cityName);
```
More about it you can read [here (EN)](https://docs.microsoft.com/en-us/dotnet/csharp/discards)

#### String interpolation:
Take a look at this code:
```
Console.WriteLine($"Input arguments: [{string.Join(", ", args)}]");
```
More about it you can read [here (RU)](https://csharp.net-tutorials.com/ru/414/%D0%BE%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%BE%D1%80%D1%8B/%D0%BE%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%BE%D1%80-%D0%B8%D0%BD%D1%82%D0%B5%D1%80%D0%BF%D0%BE%D0%BB%D1%8F%D1%86%D0%B8%D0%B8-%D1%81%D1%82%D1%80%D0%BE%D0%BA/)

### Some useful standard methods you should be aware of:
* https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trygetvalue?view=netcore-3.1
* https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=netcore-3.1
* https://docs.microsoft.com/en-us/dotnet/api/system.string.join?view=netcore-3.1
* https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=netcore-3.1
* https://docs.microsoft.com/en-us/dotnet/api/system.string.isnullorempty?view=netcore-3.1
* https://docs.microsoft.com/en-us/dotnet/api/system.string.isnullorwhitespace?view=netcore-3.1