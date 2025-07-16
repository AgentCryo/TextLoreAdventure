using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TextLoreAdventure.Data;

public class DataTypes
{
    
}
public class SceneStory
{
    public List<Line> Lines { get; set; } = new();
    public List<Choice> Choices { get; set; } = new();
}

public class Choice
{
    public Line Line { get; set; } = new();
    public List<string> Scenes { get; set; } = new();
}

public class Line
{
    public string Text { get; set; }
    public int FontSize { get; set; }
    public byte R { get; set; } = 50;
    public byte G { get; set; } = 205;
    public byte B { get; set; } = 50;
    public int PaddingBelow { get; set; } = 0;
    public bool SendByPlayer { get; set; }
    public Color GetColor() => new Color(R, G, B);
    
    public Line() { }
    public Line(string text, int fontSize, Color color, int paddingBelow = 0, bool sendByPlayer = false)
    {
        Text = text;
        FontSize = fontSize;
        R = color.R;
        G = color.G;
        B = color.B;
        SendByPlayer = sendByPlayer;
        PaddingBelow = paddingBelow;
    }
    public string GetCssColor() => GetColor().ToCss();
    public string FontSizePxpx => $"{FontSize}px";
    public string PaddingBelowPxpx => $"{PaddingBelow}px";
}

public class Color
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }

    public Color(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    public string ToCss() => $"rgb({R},{G},{B})";

    public static Color DarkGreen => new Color(0, 100, 0);
    public static Color Green => new Color(0, 255, 0);
    public static Color White => new Color(255, 255, 255);
    public static Color Black => new Color(0, 0, 0);
    public static Color Red => new Color(255, 0, 0);
    public static Color DarkGray => new Color(64, 64, 64);
}

public static class ReadJson
{
    public static async Task<SceneStory> SceneStoryAsync(HttpClient http, string jsonFileName)
    {
        string url = $"Assets/Scenes/Story/{jsonFileName}.json";
        string json = await http.GetStringAsync(url);
        return JsonSerializer.Deserialize<SceneStory>(json)!;
    }
}
