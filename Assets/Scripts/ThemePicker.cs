using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ThemePicker : MonoBehaviour
{
    public Color[] themeColor;
    public Button themeButtonPrefab;

    List<Button> m_ThemeButtons = new List<Button>();

    public void Start()
    {
        foreach (var theme in themeColor)
        {
            var newButton = Instantiate(themeButtonPrefab, transform);
            newButton.GetComponent<Image>().color = theme;

            m_ThemeButtons.Add(newButton);
        }
    }
}
