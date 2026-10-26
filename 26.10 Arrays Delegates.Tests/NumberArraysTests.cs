public class NumberArraysTests
{
    [Fact]
    public void GetEven_ReturnsEvenNumbersInOriginalOrder()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        Assert.Equal(new[] { 2, 4, 6 }, NumberArrays.GetEven(numbers));
    }

    [Fact]
    public void GetEven_KeepsZeroAndNegativeEvenNumbers()
    {
        int[] numbers = { -4, -3, 0, 7 };

        Assert.Equal(new[] { -4, 0 }, NumberArrays.GetEven(numbers));
    }

    [Fact]
    public void GetOdd_ReturnsOddNumbersInOriginalOrder()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };

        Assert.Equal(new[] { 1, 3, 5 }, NumberArrays.GetOdd(numbers));
    }

    [Fact]
    public void GetOdd_DoesNotTreatZeroAsOdd()
    {
        int[] numbers = { 0, -3, 8 };

        Assert.Equal(new[] { -3 }, NumberArrays.GetOdd(numbers));
    }

    [Fact]
    public void GetPrime_ReturnsOnlyPrimeNumbers()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 9, 11, 25, 97, -7, 0 };

        Assert.Equal(new[] { 2, 3, 5, 11, 97 }, NumberArrays.GetPrime(numbers));
    }

    [Fact]
    public void GetPrime_IncludesTwo_AndExcludesOne()
    {
        Assert.Equal(new[] { 2 }, NumberArrays.GetPrime(new[] { 1, 2 }));
    }

    [Fact]
    public void GetFibonacci_ReturnsFibonacciNumbers()
    {
        int[] numbers = { 0, 1, 2, 3, 4, 5, 8, 13, 21, 22 };

        Assert.Equal(new[] { 0, 1, 2, 3, 5, 8, 13, 21 }, NumberArrays.GetFibonacci(numbers));
    }

    [Fact]
    public void GetFibonacci_HandlesLargeValues()
    {
        int[] numbers = { 832040, 832041 };

        Assert.Equal(new[] { 832040 }, NumberArrays.GetFibonacci(numbers));
    }

    [Fact]
    public void GetFibonacci_ExcludesNegativeNumbers()
    {
        int[] numbers = { -1, -2, -3, 5 };

        Assert.Equal(new[] { 5 }, NumberArrays.GetFibonacci(numbers));
    }

    [Fact]
    public void EmptyArray_AllMethodsReturnEmptyArray()
    {
        int[] empty = Array.Empty<int>();

        Assert.Empty(NumberArrays.GetEven(empty));
        Assert.Empty(NumberArrays.GetOdd(empty));
        Assert.Empty(NumberArrays.GetPrime(empty));
        Assert.Empty(NumberArrays.GetFibonacci(empty));
    }

    [Fact]
    public void Methods_DoNotModifyInputArray()
    {
        int[] numbers = { 3, 1, 2 };

        NumberArrays.GetEven(numbers);
        NumberArrays.GetOdd(numbers);
        NumberArrays.GetPrime(numbers);
        NumberArrays.GetFibonacci(numbers);

        Assert.Equal(new[] { 3, 1, 2 }, numbers);
    }
}
