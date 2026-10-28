public class BackpackTests
{
    private static Backpack CreateBackpack(double volume = 10)
    {
        Backpack backpack = new Backpack();
        backpack.SetColor("Чорний");
        backpack.SetBrand("Nike (Vietnam)");
        backpack.SetFabric("Нейлон");
        backpack.SetWeight(1.2);
        backpack.SetVolume(volume);
        return backpack;
    }

    [Fact]
    public void SetCharacteristics_StoreValues()
    {
        Backpack backpack = CreateBackpack();

        Assert.Equal("Чорний", backpack.Color);
        Assert.Equal("Nike (Vietnam)", backpack.Brand);
        Assert.Equal("Нейлон", backpack.Fabric);
        Assert.Equal(1.2, backpack.Weight, 3);
        Assert.Equal(10.0, backpack.Volume, 3);
    }

    [Fact]
    public void AddItem_AddsToContentsAndUpdatesVolume()
    {
        Backpack backpack = CreateBackpack(10);
        Book book = new Book("Книга", 2, 1);

        backpack.AddItem(book);

        Assert.Single(backpack.Items);
        Assert.Same(book, backpack.Items[0]);
        Assert.Equal(2.0, backpack.UsedVolume, 3);
    }

    [Fact]
    public void AddItem_ExactlyFits_Succeeds()
    {
        Backpack backpack = CreateBackpack(5);
        backpack.AddItem(new Book("Книга", 5, 1));

        Assert.Single(backpack.Items);
        Assert.Equal(5.0, backpack.UsedVolume, 3);
    }

    [Fact]
    public void AddItem_Null_Throws()
    {
        Backpack backpack = CreateBackpack();

        Assert.Throws<ArgumentNullException>(() => backpack.AddItem(null!));
    }

    [Fact]
    public void AddItem_Overflow_ThrowsAndKeepsContents()
    {
        Backpack backpack = CreateBackpack(5);
        backpack.AddItem(new Book("Книга", 3, 1));

        InvalidOperationException error = Assert.Throws<InvalidOperationException>(
            () => backpack.AddItem(new Laptop("Ноутбук", 3, 2.2)));

        Assert.Contains("Недостатньо місця", error.Message);
        Assert.Single(backpack.Items);
        Assert.Equal(3.0, backpack.UsedVolume, 3);
    }

    [Fact]
    public void ItemAdded_HandlerRunsAfterItemIsStored()
    {
        Backpack backpack = CreateBackpack(10);
        Book book = new Book("Книга", 2, 1);

        int calls = 0;
        Backpack? sender = null;
        BackpackItem? added = null;
        double usedAtEvent = -1;

        backpack.ItemAdded += delegate (Backpack source, BackpackItem item)
        {
            calls++;
            sender = source;
            added = item;
            usedAtEvent = source.UsedVolume;
        };

        backpack.AddItem(book);

        Assert.Equal(1, calls);
        Assert.Same(backpack, sender);
        Assert.Same(book, added);
        Assert.Equal(2.0, usedAtEvent, 3);
    }

    [Fact]
    public void ItemAdded_NotRaised_WhenOverflow()
    {
        Backpack backpack = CreateBackpack(5);
        backpack.AddItem(new Book("Книга", 4, 1));

        int calls = 0;
        backpack.ItemAdded += delegate { calls++; };

        Assert.Throws<InvalidOperationException>(
            () => backpack.AddItem(new Laptop("Ноутбук", 2, 2.2)));
        Assert.Equal(0, calls);
    }
}
