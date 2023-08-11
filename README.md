# CSharpFunctionalExtensions.FluentAssertions

- [![NuGet Package](https://img.shields.io/nuget/v/CSharpFunctionalExtensions.FluentAssertions.svg)](https://www.nuget.org/packages/CSharpFunctionalExtensions.FluentAssertions) **CSharpFunctionalExtensions.FluentAssertions**

A small set of extensions to make test assertions more fluent when using CSharpFunctionalExtensions! Wow!

## Learn More

* [Announcing CSharpFunctionalExtensions.FluentAssertions](https://www.kylemcmaster.com/blog/announcing-csharpfunctionalextensions-fluentassertions)

## Dependencies

This library is compatible with .NET 6+. It requires the following minimum package versions:

CSharpFunctionalExtensons >= 2.37.0

FluentAssertions >= 6.10.0

## Installation

This library is available on Nuget and can be installed with the .NET CLI using the following command:

```bash
dotnet add package CSharpFunctionalExtensions.FluentAssertions
```

## Usage

### Maybe Assertions

```csharp
var maybe = Maybe.From("foo");

maybe.Should().HaveSomeValue(); // passes
maybe.Should().HaveValue("foo"); // passes
maybe.Should().HaveValue("bar"); // throws
maybe.Should().HaveNoValue(); // throws
```

```csharp
Maybe<string> maybe = null;

maybe.Should().HaveNoValue(); // passes
maybe.Should().HaveValue("foo"); // throws
```

### Result Assertions 

```csharp
var result = Result.Success();

result.Should().Succeed(); // passes
result.Should().Fail() // throws
```

```csharp
var result = Result.Failure("error");

result.Should().Fail() // passes
result.Should().FailWith("error"); // passes
result.Should().FailWith("some other error"); // throws
result.Should().Succeed(); // throws
```

### Generic Result of T Assertions

```csharp
var result = Result.Success(420);

result.Should().Succeed(); // passes
result.Should().SucceedWith(420); // passes
result.Should().SucceedWith(69); // throws
result.Should().Fail(); // throws
```

```csharp
var result = Result.Failure<string>("error");

result.Should().Fail() // passes
result.Should().FailWith("error"); // passes
result.Should().FailWith("some other error"); // throws
result.Should().Succeed(); // throws
```

### Generic Result of T:Value and E:Error Assertions

```csharp
var result = Result.Success<int, Exception>(420);

result.Should().Succeed(); // passes
result.Should().SucceedWith(420); // passes
result.Should().SucceedWith(69); // throws
result.Should().Fail(); // throws
result.Should().FailWith(new Exception("error")); // throws
```

```csharp
var result = Result.Failure<int, Exception>(new Exception("error"));

result.Should().Fail(); // passes
result.Should().FailWith(new Exception("error")); // passes
result.Should().FailWith(new Exception("some other error")); // throws
result.Should().Succeed(); // throws
result.Should().SucceedWith(4680); // throws
```

### UnitResult Assertions

```csharp
var result = UnitResult.Success<string>();

result.Should().Succeed(); // passes
result.Should().Fail(); // throws
result.Should().FailWith("error"); // throws
```

```csharp
var result = UnitResult.Failure("error");

result.Should().Fail(); // passes
result.Should().FailWith("error"); // passes
result.Should().Succeed(); // throws
```

## Related Projects

* [CSharpFunctionalExtensions](https://github.com/vkhorikov/CSharpFunctionalExtensions)
* [FluentAssertions.CSharpFunctionalExtensions](https://github.com/pedromtcosta/FluentAssertions.CSharpFunctionalExtensions)
* [Typescript Functional Extensions](https://github.com/seangwright/typescript-functional-extensions)

## Acknowledgements

Special thanks to [Sean Wright](https://github.com/seangwright) for all his guidance and contributions over the design, development, and release of this project. His insights are invaluable! :smile: 
