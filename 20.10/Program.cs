Country u = new Country("Dnipro", "Odesa", "Kyiv");
Console.WriteLine(u[1]);
u[1] = "Odessa";
u[1, 0] = 1000000;
Console.WriteLine(u[1, 0]);
Console.WriteLine(u[1]);
Console.WriteLine(u);
