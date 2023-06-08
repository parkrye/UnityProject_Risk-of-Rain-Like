using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour
{
    protected Dictionary<string, RectTransform> transforms;
    protected Dictionary<string, Button> buttons;
    protected Dictionary<string, TMP_Text> texts;

    protected virtual void Awake()
    {
        BindingChildren();
    }

    protected virtual void BindingChildren()
    {
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>();
        texts = new Dictionary<string, TMP_Text>();

        RectTransform[] childrenRect = GetComponentsInChildren<RectTransform>();
        foreach (var child in childrenRect)
        {
            string key = child.name;
            if (!transforms.ContainsKey(key))
            {
                transforms[key] = child;

                if (child.GetComponent<Button>())
                {
                    if (!buttons.ContainsKey(key))
                        buttons[key] = child.GetComponent<Button>();
                }

                if (child.GetComponent<TMP_Text>())
                {
                    if (!buttons.ContainsKey(key))
                        texts[key] = child.GetComponent<TMP_Text>();
                }
            }
        }
    }

    public virtual void CloseUI()
    {

    }
}
