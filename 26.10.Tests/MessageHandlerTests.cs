using System.Reflection;

public class MessageHandlerTests
{
    [Fact]
    public void Delegate_ReturnsVoid()
    {
        MethodInfo invoke = typeof(MessageHandler).GetMethod("Invoke")!;

        Assert.Equal(typeof(void), invoke.ReturnType);
    }

    [Fact]
    public void Delegate_TakesOneStringParameter()
    {
        ParameterInfo[] parameters = typeof(MessageHandler).GetMethod("Invoke")!.GetParameters();

        ParameterInfo parameter = Assert.Single(parameters);
        Assert.Equal(typeof(string), parameter.ParameterType);
    }

    [Fact]
    public void Delegate_CanPointToPrinterMethod_AndWritesText()
    {
        StringWriter output = new StringWriter();
        MessagePrinter printer = new MessagePrinter(output);

        MessageHandler handler = printer.WritePlain;
        handler("Привіт");

        Assert.Equal("Привіт" + Environment.NewLine, output.ToString());
    }
}
