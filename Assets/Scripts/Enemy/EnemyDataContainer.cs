using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataContainer : MonoBehaviour
{
    Dictionary<string, EnemyDataBase> dictionary;

    void Awake()
    {
        dictionary = new Dictionary<string, EnemyDataBase>();
    }

    public EnemyDataBase GetData(string name)
    {
        if(dictionary.ContainsKey(name))
            return dictionary[name];
        return null;
    }

    public void AddData(string name, EnemyDataBase data)
    {
        dictionary.Add(name, data);
    }
}
