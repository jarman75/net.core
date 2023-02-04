namespace DotnetStarter.Logic.Tests;

public class HelloWorldTest
{
    [Fact]
    public void Hello_ReturnsWorld() => Assert.Equal("World!", HelloWorld.Hello());
    [Fact]
    public void Hello_ReturnsHelloName() => Assert.Equal("Hello John!", HelloWorld.Hello("John"));
    [Fact]
    public void Hello_ReturnsHelloNameSurname() => Assert.Equal("Hello John Doe!", HelloWorld.Hello("John", "Doe"));
    [Fact]
    public void Bye_ReturnsWorld() => Assert.Equal("World!", HelloWorld.Bye());
    
}