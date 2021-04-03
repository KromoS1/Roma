## Chapter 1. Basic CLI arguments parser
### Motivation:
To create great console application we need to build foundation for handling cli (Command Line Interface) arguments.
So when you call commands like this: 
```
todo add --priority 1 --due tomorrow --tags development "Code review"
```
it will not make for our any problems to parse and execute following command.
So basic command structure is:
```
ProgramName CommandName [Options] Subject
```
* ProgramName: for our case it will be **todo**
* CommandName: usually it's verb like: **add**, **remove**, **list**, **help**, etc. Required
* Options: optional pairs of key/value. Value can be optional or required. Also can be validated by some rules. Key always has long name like **--name** prefixed with double dash (--) and can have optional short name prefixed with single dash like (-)
* Subject: can happen only in the end of the command. Can be required or optional
### Goal:
Implement **CliCommandParser** so tests defined in **CliCommandParserTests** pass
### Take a look:
#### Nullable reference types
In projects enabled nullable reference types. That means you can define reference type that can or cannot be null. For example:
```
string a = "Some string"; // ok. string means it should always have value
string b = null; // error! b cannot be null
string? c = "Another string"; // ok.
string? d = null; // ok. string? means it can be null
```

More about it you can read [here (RU)](https://temofeev.ru/info/articles/chto-novogo-v-c-8/)
#### Builder pattern / fluent syntax
If you often need to create objects with big amount of parameters you can use **builder pattern**. Take a look at **CommandDefinitionBuilder**. It helps to create **CommandDefinition** object via chain of methods that returns builder itself and then **Build** method that actually creates object

More about it you can read [here (RU)](https://metanit.com/sharp/patterns/6.1.php)
#### using static
Take a look at **using block** of **CliCommandParserTests**:
```
using static Todo.CliArguments.CommandDefinitionBuilder;
using static Todo.CliArguments.ArgumentDefinitionBuilder;
```
This definition allows us to use static methods of these classes without class name. **NewCommand** is static method.
```
NewCommand("list")
    .WithHelpText("Shows all elements")
    .Build(),
```

More about it you can read [here (EN)](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-static)
#### Custom exceptions:
Take a look at types in folder **Todo.CliArguments/Exceptions** like **ExpectedKeyException**. It is example of custom exception.
It is simple class that should be inherited from **System.Exception**. You can throw them as usual ones and pass any information you want to catch.