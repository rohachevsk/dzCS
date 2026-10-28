public class ItemsTests
{
    [Fact]
    public void Book_HasDefaultCharacteristics()
    {
        Book book = new Book();

        Assert.Equal("Книга", book.Name);
        Assert.Equal(1.5, book.Volume, 3);
        Assert.Equal(0.8, book.Weight, 3);
    }

    [Fact]
    public void Laptop_HasDefaultCharacteristics()
    {
        Laptop laptop = new Laptop();

        Assert.Equal("Ноутбук", laptop.Name);
        Assert.Equal(4.0, laptop.Volume, 3);
        Assert.Equal(2.2, laptop.Weight, 3);
    }

    [Fact]
    public void WaterBottle_HasDefaultCharacteristics()
    {
        WaterBottle bottle = new WaterBottle();

        Assert.Equal("Пляшка води", bottle.Name);
        Assert.Equal(0.75, bottle.Volume, 3);
        Assert.Equal(0.6, bottle.Weight, 3);
    }

    [Fact]
    public void Notebook_HasDefaultCharacteristics()
    {
        Notebook notebook = new Notebook();

        Assert.Equal("Зошит", notebook.Name);
        Assert.Equal(0.5, notebook.Volume, 3);
        Assert.Equal(0.3, notebook.Weight, 3);
    }

    [Fact]
    public void Item_StoresCustomValues()
    {
        Book book = new Book("Словник", 2.5, 1.4);

        Assert.Equal("Словник", book.Name);
        Assert.Equal(2.5, book.Volume, 3);
        Assert.Equal(1.4, book.Weight, 3);
    }

    [Fact]
    public void ToString_ContainsNameAndUnits()
    {
        string text = new WaterBottle().ToString();

        Assert.Contains("Пляшка води", text);
        Assert.Contains("л", text);
        Assert.Contains("кг", text);
    }
}
