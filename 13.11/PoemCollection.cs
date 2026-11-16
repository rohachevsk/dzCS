namespace Primer;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class PoemCollection
{
    private readonly List<Poem> poems = new();

    public IReadOnlyList<Poem> Poems => poems;
    public int Count => poems.Count;

    public void Add(Poem poem)
    {
        poems.Add(poem);
    }

    public bool Remove(string title)
    {
        int index = poems.FindIndex(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (index >= 0)
        {
            poems.RemoveAt(index);
            return true;
        }
        return false;
    }

    public bool Edit(string title, string newTitle, string newAuthor, int newYear, string newText, string newTopic)
    {
        Poem? poem = FindByTitleExact(title);
        if (poem == null) return false;

        if (!string.IsNullOrWhiteSpace(newTitle)) poem.Title = newTitle;
        if (!string.IsNullOrWhiteSpace(newAuthor)) poem.Author = newAuthor;
        if (newYear > 0) poem.Year = newYear;
        if (!string.IsNullOrWhiteSpace(newText)) poem.Text = newText;
        if (!string.IsNullOrWhiteSpace(newTopic)) poem.Topic = newTopic;

        return true;
    }

    public Poem? FindByTitleExact(string title)
    {
        return poems.Find(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public List<Poem> SearchByTitle(string titlePart)
    {
        return poems.FindAll(p => p.Title.Contains(titlePart, StringComparison.OrdinalIgnoreCase));
    }

    public List<Poem> SearchByAuthor(string authorPart)
    {
        return poems.FindAll(p => p.Author.Contains(authorPart, StringComparison.OrdinalIgnoreCase));
    }

    public List<Poem> SearchByYear(int year)
    {
        return poems.FindAll(p => p.Year == year);
    }

    public List<Poem> SearchByTopic(string topicPart)
    {
        return poems.FindAll(p => p.Topic.Contains(topicPart, StringComparison.OrdinalIgnoreCase));
    }

    public List<Poem> SearchByWordInText(string word)
    {
        return poems.FindAll(p => p.Text.Contains(word, StringComparison.OrdinalIgnoreCase));
    }

    public void SaveToFile(string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(poems, options);
        File.WriteAllText(filePath, json);
    }

    public void LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Файл {filePath} не знайдено.");
        }

        string json = File.ReadAllText(filePath);
        var loaded = JsonSerializer.Deserialize<List<Poem>>(json);
        if (loaded != null)
        {
            poems.Clear();
            poems.AddRange(loaded);
        }
    }

    public void SeedDefaultPoems()
    {
        if (poems.Count > 0) return;

        poems.Add(new Poem(
            "Заповіт",
            "Тарас Шевченко",
            1845,
            "Як умру, то поховайте\nМене на могилі\nСеред степу широкого\nНа Вкраїні милій,\nЩоб лани широкополі,\nІ Дніпро, і кручі\nБуло видно, було чути,\nЯк реве ревучий.",
            "Патріотична лірика"
        ));

        poems.Add(new Poem(
            "Contra spem spero!",
            "Леся Українка",
            1890,
            "Гетьте, думи, ви хмари осінні!\nТо ж тепера весна золота!\nЧи то так у жалю, в голосінні\nПроминуть молодії літа?",
            "Філософська лірика"
        ));

        poems.Add(new Poem(
            "Каменярі",
            "Іван Франко",
            1883,
            "Я бачив дивний сон. Немов передо мною\nБезмірна, та пуста, і дика площина,\nІ я, прикований ланцем залізним, стою\nПід височенною гранітною скалою...",
            "Громадянська лірика"
        ));

        poems.Add(new Poem(
            "Крила",
            "Ліна Костенко",
            1958,
            "А й правда, крилатим ґрунту не треба.\nЗемлі немає, то буде небо.\nНемає поля, то буде воля.\nНемає пари, то будуть хмари.",
            "Філософська лірика"
        ));

        poems.Add(new Poem(
            "Мені тринадцятий минало",
            "Тарас Шевченко",
            1847,
            "Мені тринадцятий минало.\nЯ пас ягнята за селом.\nЧи то так сонечко сіяло,\nЧи так мені чого було?\nОбернулося небо і помарніло...",
            "Автобіографічна лірика"
        ));
    }
}

