namespace DotnetStarter.Logic;
public class HelloWorld
 {
    public static string Hello() => "World!";
    public static string Hello(string name) => $"Hello {name}!";
    public static string Hello(string name, string surname) => $"Hello {name} {surname}!";        
    public static string Bye() => "World!";
 }
