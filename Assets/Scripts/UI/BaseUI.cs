using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour
{
    protected Dictionary<string, RectTransform> transforms;
    protected Dictionary<string, Button> buttons;
    protected Dictionary<string, TMP_Text> texts;
    protected Dictionary<string, Slider> sliders;
    protected Dictionary<string, Image> images;
    protected Dictionary<string, ToggleGroup> toggleGroups;
    protected Dictionary<string, Toggle> toggles;
    [SerializeField] protected AudioSource clickAudio;

    protected virtual void Awake()
    {
        BindingChildren();
        AddClickAudio();
    }

    protected virtual void BindingChildren()
    {
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>();
        texts = new Dictionary<string, TMP_Text>();
        sliders = new Dictionary<string, Slider>();
        images = new Dictionary<string, Image>();
        toggleGroups = new Dictionary<string, ToggleGroup>();
        toggles = new Dictionary<string, Toggle>();

        RectTransform[] childrenRect = GetComponentsInChildren<RectTransform>();
        for(int i = 0; i < childrenRect.Length; i++)
        {
            string key = childrenRect[i].name;
            if (!transforms.ContainsKey(key))
            {
                transforms[key] = childrenRect[i];

                if (childrenRect[i].GetComponent<Button>())
                {
                    if (!buttons.ContainsKey(key))
                        buttons[key] = childrenRect[i].GetComponent<Button>();
                }

                if (childrenRect[i].GetComponent<TMP_Text>())
                {
                    if (!texts.ContainsKey(key))
                        texts[key] = childrenRect[i].GetComponent<TMP_Text>();
                }

                if (childrenRect[i].GetComponent<Slider>())
                {
                    if(!sliders.ContainsKey(key))
                        sliders[key] = childrenRect[i].GetComponent<Slider>();
                }

                if (childrenRect[i].GetComponent<Image>())
                {
                    if (!images.ContainsKey(key))
                        images[key] = childrenRect[i].GetComponent<Image>();
                }

                if (childrenRect[i].GetComponent<ToggleGroup>())
                {
                    if (!toggleGroups.ContainsKey(key))
                        toggleGroups[key] = childrenRect[i].GetComponent<ToggleGroup>();
                }

                if (childrenRect[i].GetComponent<Toggle>())
                {
                    if (!toggles.ContainsKey(key))
                        toggles[key] = childrenRect[i].GetComponent<Toggle>();
                }
            }
        }
    }

    protected virtual void AddClickAudio()
    {
        GameObject audio = GameManager.Resource.Instantiate<GameObject>("Audio/SFX/UI");
        audio.transform.parent = transform;
        clickAudio = audio.GetComponent<AudioSource>();
        foreach (KeyValuePair<string, Button> button in buttons)
        {
            button.Value.onClick.AddListener(() => { clickAudio.Play(); });
        }
    }

    public virtual void CloseUI()
    {

    }
}
