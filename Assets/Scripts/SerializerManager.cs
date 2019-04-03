using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DilmerGames.MagicLeap.Core;
using UnityEngine;

public class SerializerManager : MonoBehaviourSingleton<SerializerManager>
{
    [SerializeField]
    private string gameDataFileName = "GameData.json";

    public GameData LoadGameData()
    {
        GameData gameData = null;
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);
        if(!File.Exists(filePath))
        {
            gameData = mockUpGameData();
        }
        else 
        {
            string jsonData = File.ReadAllText (filePath);
            gameData = JsonUtility.FromJson<GameData> (jsonData);
        }
        return gameData;
    }

    public void SaveGameData(GameData gameData)
    {
        string dataAsJson = JsonUtility.ToJson (gameData);
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);
        File.WriteAllText (filePath, dataAsJson);
    }

    private GameData mockUpGameData()
    {
        GameData emptyGameData = new GameData();
        emptyGameData.Player.Name = "Test User";
        emptyGameData.Player.Email = "test@test.com";
        emptyGameData.Player.Score = 100;
        emptyGameData.Player.MinutesPlayed = 1;
        emptyGameData.Levels[0].Number = 1;
        emptyGameData.Levels[0].Name = "A cool level name";
        emptyGameData.Levels[0].StatusState =  Level.Status.Played;
        emptyGameData.Levels[0].DifficultyState =  Level.Difficulty.ExtraHard;
        return emptyGameData;
    }
}
