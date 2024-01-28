using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class ParsingTask
{
    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
    /// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
    /// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
    public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
    {
        int slideId;
        SlideType slideType;
        return lines.Select(s => s.Split(';'))
                .Where(s => s.Length == 3 && int.TryParse(s[0], out slideId)
                && Enum.TryParse(s[1], true, out slideType))
                .Select(s => new SlideRecord(int.Parse(s[0]),
                (SlideType)Enum.Parse(typeof(SlideType), s[1], true), s[2]))
                .Where(s => s != null)
                .ToDictionary(s => s.SlideId, s => s);
    }

    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
    /// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
    /// Такой словарь можно получить методом ParseSlideRecords</param>
    /// <returns>Список информации о посещениях</returns>
    /// <exception cref="FormatException">Если среди строк есть некорректные</exception>
    public static IEnumerable<VisitRecord> ParseVisitRecords(
        IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
    {
        DateTime dateTime;
        int id;
        return lines.Where(s => s != "UserId;SlideId;Date;Time")
                .Select(s => s.Split(';'))
                .Select(s =>
                {
                    try
                    {
                        return new VisitRecord(int.Parse(s[0]),
                                slides[int.Parse(s[1])].SlideId, DateTime.Parse($"{s[2]} {s[3]}"),
                                slides[int.Parse(s[1])].SlideType);
                    }
                    catch (Exception)
                    {
                        throw new FormatException($"Wrong line [{string.Join(';', s.Skip(0))}]");
                    }
                }
                );
    }
}