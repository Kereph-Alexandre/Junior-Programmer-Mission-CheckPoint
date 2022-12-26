using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager;

    public GameData data;
    // REste a faire : Remplacer data<GameData> de MainManager par dataManager.data<GameData> de DataManager

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
}
