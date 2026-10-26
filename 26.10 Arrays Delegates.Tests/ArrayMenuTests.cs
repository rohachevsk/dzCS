public class ArrayMenuTests
{
    private static string Nl => Environment.NewLine;

    [Fact]
    public void Menu_Choice1_PrintsEvenNumbers()
    {
        string result = RunMenu("1 2 3 4 5 6" + Nl + "1" + Nl + "0");

        Assert.Contains("Парні: 2 4 6", result);
    }

    [Fact]
    public void Menu_Choice4_PrintsFibonacciNumbers()
    {
        string result = RunMenu("0 1 2 4 5" + Nl + "4" + Nl + "0");

        Assert.Contains("Фібоначчі: 0 1 2 5", result);
    }

    [Fact]
    public void Menu_Choice0_ExitsWithoutResult()
    {
        string result = RunMenu("1 2 3" + Nl + "0");

        Assert.DoesNotContain("Парні:", result);
        Assert.DoesNotContain("Непарні:", result);
        Assert.DoesNotContain("Прості:", result);
        Assert.DoesNotContain("Фібоначчі:", result);
    }

    [Fact]
    public void Menu_InvalidChoice_PrintsMessageAndContinues()
    {
        string result = RunMenu("1 2 3" + Nl + "9" + Nl + "2" + Nl + "0");

        Assert.Contains("Невірний вибір.", result);
        Assert.Contains("Непарні: 1 3", result);
    }

    [Fact]
    public void Menu_InvalidNumbers_AsksAgain()
    {
        string result = RunMenu("abc" + Nl + "1 2 3" + Nl + "0");

        Assert.Contains("Невірні числа. Спробуйте ще раз.", result);
        Assert.Contains("Масив: 1 2 3", result);
    }

    [Fact]
    public void Menu_EmptyResult_PrintsNoNumbersMessage()
    {
        string result = RunMenu("3 5" + Nl + "1" + Nl + "0");

        Assert.Contains("Таких чисел немає.", result);
    }

    [Fact]
    public void DelegateArray_IndexSelectsMethod()
    {
        ArrayMethod[] dg = { NumberArrays.GetEven, NumberArrays.GetOdd, NumberArrays.GetPrime, NumberArrays.GetFibonacci };
        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        Assert.Equal(new[] { 2, 4, 6 }, dg[0](numbers));
        Assert.Equal(new[] { 1, 3, 5 }, dg[1](numbers));
        Assert.Equal(new[] { 2, 3, 5 }, dg[2](numbers));
        Assert.Equal(new[] { 1, 2, 3, 5 }, dg[3](numbers));
    }

    private static string RunMenu(string input)
    {
        StringWriter output = new StringWriter();
        ArrayMenu menu = new ArrayMenu(new StringReader(input), output);

        menu.Run();

        return output.ToString();
    }
}
