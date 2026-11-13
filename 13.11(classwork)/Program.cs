using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        // Массив названий товаров для заполнения шаблона
        string[] products = { "Bread", "Milk", "Lemons" };
        int currentId = 1;
        int prodIndex = 0;

        // Открываем существующий файл для чтения и записи
        FileStream f;
        f = new FileStream("example.xml", FileMode.Open, FileAccess.ReadWrite);

        int prev = f.ReadByte();
        int b;

        // Побайтово сканируем файл
        while ((b = f.ReadByte()) != -1)
        {
            // 1. Поиск пустого id='' (две одинарные кавычки подряд)
            if (prev == '\'' && b == '\'')
            {
                // Позиция для вставки — ровно между кавычками (перед второй кавычкой)
                long insertPos = f.Position - 1;

                // Считываем весь остаток файла в буфер, чтобы сдвинуть его вправо
                f.Seek(insertPos, SeekOrigin.Begin);
                byte[] tail = new byte[f.Length - insertPos];
                f.Read(tail, 0, tail.Length);

                // Записываем номер id (например: '1', '2', '3')
                f.Seek(insertPos, SeekOrigin.Begin);
                f.WriteByte((byte)('0' + currentId));
                currentId++;

                // Дописываем хвост файла обратно
                f.Write(tail, 0, tail.Length);

                // Пропускаем вставленную цифру и закрывающую кавычку
                f.Seek(insertPos + 2, SeekOrigin.Begin);
                prev = '\'';
                continue;
            }

            // 2. Поиск пустого места между тегами >< (например, <product id='1'></product>)
            if (prev == '>' && b == '<')
            {
                if (prodIndex < products.Length)
                {
                    // Позиция для вставки названия — перед символом '<'
                    long insertPos = f.Position - 1;

                    // Считываем остаток файла
                    f.Seek(insertPos, SeekOrigin.Begin);
                    byte[] tail = new byte[f.Length - insertPos];
                    f.Read(tail, 0, tail.Length);

                    // Переводим название товара в байты
                    byte[] nameBytes = Encoding.UTF8.GetBytes(products[prodIndex]);
                    prodIndex++;

                    // Записываем название товара
                    f.Seek(insertPos, SeekOrigin.Begin);
                    f.Write(nameBytes, 0, nameBytes.Length);

                    // Дописываем хвост файла
                    f.Write(tail, 0, tail.Length);

                    // Смещаем позицию за вставленное название к символу '<'
                    f.Seek(insertPos + nameBytes.Length, SeekOrigin.Begin);
                    prev = nameBytes[nameBytes.Length - 1];
                    continue;
                }
            }

            prev = b;
        }

        // Закрываем поток
        f.Close();

        Console.WriteLine("Шаблон в example.xml успішно заповнено!");
    }
}
