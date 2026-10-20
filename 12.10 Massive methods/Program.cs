using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        WebSite site = new WebSite();
        site.Input();
        site.Print();

        Magazine magazine = new Magazine();
        magazine.Input();
        magazine.Print();

        Shop shop = new Shop();
        shop.Input();
        shop.Print();
    }
}
