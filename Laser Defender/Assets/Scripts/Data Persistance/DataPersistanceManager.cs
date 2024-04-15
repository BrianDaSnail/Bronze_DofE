using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager instace { get; private set; }

    private GameData gameData;

    private void Awake()
    {
        if(instace != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene.");
        }
        instace = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //

        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }
    }

    public void SaveGame()
    {
        //
    }
}
