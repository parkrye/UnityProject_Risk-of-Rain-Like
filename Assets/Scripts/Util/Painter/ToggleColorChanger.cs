using UnityEngine;
using UnityEngine.UI;

public class ToggleColorChanger : MonoBehaviour
{
    Image image;

    void OnEnable()
    {
        image = GetComponent<Image>();
    }

    public void ColorChange(bool isOn)
    {
        if (isOn)
        {
            image.color = new Color(1f, 1f, 1f);
        }
        else
        {
            image.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }
}
