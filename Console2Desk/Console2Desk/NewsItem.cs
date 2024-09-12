using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public class NewsItem
{
    public string Title { get; set; }
    public string Icon { get; set; }
    public string Status { get; set; }
}

public class NewsLoader
{
    public static List<NewsItem> LoadNews(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<NewsItem>(); // If the file does not exist, return an empty list
        }

        string jsonData = File.ReadAllText(filePath);

        try
        {
            return JsonConvert.DeserializeObject<List<NewsItem>>(jsonData);
        }
        catch (Exception ex)
        {
            //For Debug MessageBox.Show($"Errore durante la lettura del file JSON: {ex.Message}");
            return new List<NewsItem>(); // On error, return an empty list
        }
    }
}
