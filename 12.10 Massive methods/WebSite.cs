using System;

public class WebSite
{
    private string _name;
    private string _url;
    private string _description;
    private string _ip;

    public WebSite()
    {
        _name = "default";
        _url = "default";
        _description = "default";
        _ip = "0.0.0.0";
    }

    public void Input()
    {
        Console.Write("Назва сайту: ");
        SetName(Console.ReadLine() ?? string.Empty);

        Console.Write("Шлях до сайту: ");
        SetUrl(Console.ReadLine() ?? string.Empty);

        Console.Write("Опис сайту: ");
        SetDescription(Console.ReadLine() ?? string.Empty);

        Console.Write("IP-адреса сайту: ");
        SetIp(Console.ReadLine() ?? string.Empty);
    }

    public void Print()
    {
        Console.WriteLine("--- Веб-сайт ---");
        Console.WriteLine($"Назва: {GetName()}");
        Console.WriteLine($"Шлях: {GetUrl()}");
        Console.WriteLine($"Опис: {GetDescription()}");
        Console.WriteLine($"IP-адреса: {GetIp()}");
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public string GetUrl()
    {
        return _url;
    }

    public void SetUrl(string url)
    {
        _url = url;
    }

    public string GetDescription()
    {
        return _description;
    }

    public void SetDescription(string description)
    {
        _description = description;
    }

    public string GetIp()
    {
        return _ip;
    }

    public void SetIp(string ip)
    {
        _ip = ip;
    }
}
