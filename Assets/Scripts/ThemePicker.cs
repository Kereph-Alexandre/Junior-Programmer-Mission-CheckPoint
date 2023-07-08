using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ThemePicker : MonoBehaviour
{
    public Color[] themeColors;
    public Button themeButtonPrefab;

    List<Button> m_ThemeButtons = new List<Button>();

    public void Start()
    {
        foreach (var theme in themeColors)
        {
            var newButton = Instantiate(themeButtonPrefab, transform);
            newButton.GetComponent<Image>().color = theme;

            newButton.onClick.AddListener(() =>
            {
                DataManager.dataManager.setSelectedColor(theme);

                foreach (var button in m_ThemeButtons)
                {
                    button.interactable = true;
                }

                newButton.interactable = false;
            }
            );

            m_ThemeButtons.Add(newButton);


        }
    }
}
