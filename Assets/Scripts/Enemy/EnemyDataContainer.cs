using System.Collections.Generic;
using UnityEngine;

public class EnemyDataContainer : MonoBehaviour
{
    Dictionary<string, EnemyData> dictionary;

    void Awake()
    {
        dictionary = new Dictionary<string, EnemyData>();
        dictionary.Add("Bat", GameManager.Resource.Load<EnemyData>("Enemy/Bat"));
    }

    public EnemyData GetData(string name)
    {
        if(dictionary.ContainsKey(name))
            return dictionary[name];
        return null;
    }

    public void AddData(string name, EnemyData data)
    {
        dictionary.Add(name, data);
    }
}
