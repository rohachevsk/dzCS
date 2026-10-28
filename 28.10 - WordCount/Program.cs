string?[] samples =
{
    null,
    "",
    "   ",
    "Привіт",
    "Привіт світ",
    "  a   b  ",
    "Привіт, світ!",
};

foreach (string? sample in samples)
{
    int words = sample.CountWords();
    Console.WriteLine((sample ?? "null") + " -> " + words);
}
