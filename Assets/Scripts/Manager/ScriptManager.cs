using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour, IInitializable
{
    Dictionary<string, string> scripts;

    public void Initialize()
    {
        scripts = new Dictionary<string, string>();
        LoadScripts();
    }

    void LoadScripts()
    {

    }

    public string GetScript(string key)
    {
        if(scripts.ContainsKey(key))
        {
            return scripts[key];
        }
        return "There is not key";
    }
}
