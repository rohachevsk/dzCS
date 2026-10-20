internal class Country
{
    private City[] _cities;



    public Country()
    {
        _cities = new City[3];
    }

    public Country(int size)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException("size is zero or <0");

        _cities = new City[size];
    }

    public Country(params string[] cities)
    {
        int n = cities.Length;
        if (n == 0)
            throw new ArgumentException("incorrect data");
        _cities = new City[n];
        for (int i = 0; i < n; i++)
        {
            _cities[i] = new City() { Name = cities[i] };
            int[] arr = new int[_cities[i].Data.Length];
            for (int j = 0; j < _cities[i].Data.Length; j++)
            {
                Console.WriteLine($"Enter data[{j}]: ");
                _cities[i].Data[j] = Convert.ToInt32(Console.ReadLine());
            }
        }

    }


    public string this[int index]
    {
        get
        {
            if (index >= 0 && index < _cities.Length)
            {
                return _cities[index].Name;
            }
            else
            {
                throw new Exception("Некорректный индекс! " + index);
            }
        }
        set
        {
            if (index >= 0 && index < _cities.Length)
            {
                _cities[index].Name = value;
            }
            else
            {
                throw new Exception("Некорректный индекс! " + index);
            }
        }
    }
    public int this[int city, int parameter]
    {
        get
        {
            if (city >= 0 && city < _cities.Length && parameter >= 0 && parameter < _cities[city].Data.Length)
            {
                return _cities[city].Data[parameter];
            }
            else
            {
                throw new Exception("Некорректный индекс! " + city + ", " + parameter);
            }
        }
        set
        {
            if (city >= 0 && city < _cities.Length && parameter >= 0 && parameter < _cities[city].Data.Length)
            {
                _cities[city].Data[parameter] = value;
            }
            else
            {
                throw new Exception("Некорректный индекс! " + city + ", " + parameter);
            }
        }
    }

    public override string ToString()
    {
        string result = string.Empty;
        for (int i = 0; i < _cities.Length; i++)
        {
            result += _cities[i].ToString() + '\t';
        }
        return result;
    }
}