public interface IMyService
{
    string GetGreeting();
}

public class MyService : IMyService
{
    public string GetGreeting() => "Hello World Transient";
}


public interface IAnotherService
{
    string GetGreeting();
}

public class AnotherService : IAnotherService
{
    public string GetGreeting() => "Hello World Scoped";
}


public interface ISingletonService
{
    string GetGreeting();
}

public class SingletonService : ISingletonService
{
    public string GetGreeting() => "Hello World Singleton";
}