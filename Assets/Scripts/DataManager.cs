using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager;

    public GameData data;
    private Color m_selectedColor { get; set; }

    void Awake()
    {
        if (dataManager != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            dataManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void setSelectedColor(Color color)
    {
        this.m_selectedColor = color;
    }

    public Color getSelectedColor()
    {
        return m_selectedColor;
    }


}
