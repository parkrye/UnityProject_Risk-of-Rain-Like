using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class CSVRW
{
    public static Dictionary<string, int> ReadCSV_Records()
    {
        Dictionary<string, int> answer = new Dictionary<string, int>();

        TextAsset data = GameManager.Resource.Load<Object>("CSV/Records") as TextAsset;
        string[] texts = data.text.Split("\n");

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].Length <= 1)
                break;
            string[] line = texts[i].Split(",");
            answer.Add(line[0], int.Parse(line[1]));
        }

        return answer;
    }

    public static void WriteCSV_Records(Dictionary<string, int> data)
    {
        StringBuilder sb = new();
        string delimiter = ",";
        foreach(KeyValuePair<string, int> pair in data)
        {
            string[] text = new string[2] { pair.Key, pair.Value.ToString() };
            sb.AppendLine(string.Join(delimiter, text));
        }
        Stream fileStream = new FileStream("Assets/Resources/CSV/Records.csv", FileMode.Create, FileAccess.Write);
        StreamWriter outStream = new(fileStream, Encoding.UTF8);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public static Dictionary<string, List<int>> ReadCSV_Achivements()
    {
        Dictionary<string, List<int>> answer = new Dictionary<string, List<int>>();

        TextAsset data = GameManager.Resource.Load<Object>("CSV/Achivements") as TextAsset;
        string[] texts = data.text.Split("\n");

        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i].Length <= 1)
                break;
            string[] line = texts[i].Split(",");
            List<int> list = new List<int>();
            for(int j = 1; j < line.Length; j++)
                list.Add(int.Parse(line[j]));
            answer.Add(line[0], list);
        }

        return answer;
    }

    public static void WriteCSV_Achivements(Dictionary<string, List<int>> data)
    {
        StringBuilder sb = new();
        string delimiter = ",";
        foreach(KeyValuePair<string, List<int>> pair in data)
        {
            string[] text = new string[pair.Value.Count + 1];
            text[0] = pair.Key;
            for(int i = 1; i < text.Length; i++)
                text[i] = pair.Value[i - 1].ToString();
            sb.AppendLine(string.Join(delimiter, text));
        }
        Stream fileStream = new FileStream("Assets/Resources/CSV/Achivements.csv", FileMode.Create, FileAccess.Write);
        StreamWriter outStream = new(fileStream, Encoding.UTF8);
        outStream.WriteLine(sb);
        outStream.Close();
    }
}
