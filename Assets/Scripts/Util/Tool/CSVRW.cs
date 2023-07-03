using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class CSVRW
{
    public static Dictionary<string, int> ReadCSV_Achievements()
    {
        Dictionary<string, int> answer = new Dictionary<string, int>();

        TextAsset data = GameManager.Resource.Load<Object>("CSV/Achievements") as TextAsset;
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

    public static void WriteCSV_Achievements(Dictionary<string, int> data)
    {
        StringBuilder sb = new();
        string delimiter = ",";
        foreach(KeyValuePair<string, int> pair in data)
        {
            string[] text = new string[2] { pair.Key, pair.Value.ToString() };
            sb.AppendLine(string.Join(delimiter, text));
        }
        Stream fileStream = new FileStream("Assets/Resources/CSV/Achievements.csv", FileMode.Create, FileAccess.Write);
        StreamWriter outStream = new(fileStream, Encoding.UTF8);
        outStream.WriteLine(sb);
        outStream.Close();
    }
}
