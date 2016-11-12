# ComCalc V1.0 - 11/12/2016

ComCalc is a .NET API build to generally evaluate a math expression from strings. In a nutshell, it can do ....

```csharp
// Returns 15
Console.WriteLine( "5+4^2-!3".Evaluate() );
// Note: Evaluate() function don't exist in .NET, it is basically an extension from ComCalc API
```
## Main Features

This API doesn't just do basic math operations. ComCalc can handle :
- Binary operators `addition (+)`, `multiplication (*)`, `exponentials (^)`, `booleans (|) (&)`, etc.
- Unary operators  `negation (-)`, `inversion (~)`, `factorial (!)`, etc.
- Functions `sqrt()`, `abs()`, `sign()`, `sin()`, `cos()`, `tan()`, `log()`, `exp()`, etc.
- Multi-parametered Functions `min(,,,)`, `max(,,,)`, etc.
- Constants `pi`, `2pi`, `e`, `true`, `false`, etc.

ComCalc also are designed to make it possible to be reusable over a long run. Just keep to scroll down ...

## How it works

We don't have to generally tells everything here. simply put the attention into this code ...

```csharp
// Returns nodes (path of which have to calculated first) in form of string, and pop it into console
Console.WriteLine( "5+4^2-!3".ParseExpression().TracedPath );
```
... and this is what the console output looks like ...

```
Formula
|	Binary Op : Add
|	|	Digit : 5
|	|	Binary Op : Subtract
|	|	|	Binary Op : Power
|	|	|	|	Digit : 4
|	|	|	|	Digit : 2
|	|	|	Unary Op : Factorial
|	|	|	|	Digit : 3
```

## Installation

Simply put the scripts into your project and you're ready to go!. If you need to use it, don't forget this main step ...

```csharp
// Put this onto top of your script files
// This will make things like Evaluate() can show up in intellisense
using ComCalcLib;
```
### The Library

Without Library, there's nothing to type in ComCalc. If you take a look at `ComCalcLib\ComLibrary.cs` there's a bunch of function that you are absolutely welcome to edit into what you need. To make the Library implementation easy, we use `Reflection` and `Attributes`. These powerful C# feature are used so you can, for example, adding more kind of operator or functions.

```csharp
  
  public class DefaultComLibrary
    {
        // To create a binary/unary operator (state it in attribute), 
        // simply add ComOperator and choose the char in the attribute 
        // (have to be non-alphanumeric), and it's level-of-importance
        // Binary must have two arguments, while Unary just have only one
        [ComOperator('+', 1)]
        public static double Add(double a, double b)
        {
            return a + b;
        }
        
        //Functions, Multi-Functions, Constant is simple, there's none
		//Args in the attribute needed, except if want different name for the desired function
		 [ComFunction]        public static double sqr(double v)   { return v*v; }
		 [ComMultiFunction]        public static double mult(List<double> v)   { return v[0]*v[1]; }
		 [ComConstant]        public static double e()   { return v*v; }
```

Remember that the function have to be public static to make it discorable. Anyway, if you want to write it outside of this `DefaultComLibrary` instead, then do it anyway!, because at program initialization (or somewhere else), you can call ...

```csharp
		// Find and Load relevant libraries into ComCalcLib (globally) (this doesn't kill the old library)
		ComHelper.LoadLibrary(typeof(YourClass));
```

##### what if duplicates are found over different Library?
Then it'll just be overwrited with newer library. 

## Usage License
It's all covered in License section. Basically it is on MIT license, and you can modify, distribute, even use it for commercial things as long as you keep to credit author in your software binaries.

## Feedback
This API is fairly new so everyone can make some feedback through issues page or [direct mail](mailto:wildanmubarok22@gmail.com)

Have a look into [our other projects](http://wellosoft.wordpress.com)






















		 
