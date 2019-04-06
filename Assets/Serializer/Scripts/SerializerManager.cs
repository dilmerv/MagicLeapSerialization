using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DilmerGames.MagicLeap.Core;
using MagicLeapSerialization.Assets.Scripts.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class SerializerManager : MonoBehaviourSingleton<SerializerManager>
{
    [SerializeField]
    private string gameDataFileName = "GameData.json";

    public GameData LoadGameData()
    {
        GameData gameData = null;
        string filePath = Path.Combine(Application.persistentDataPath, gameDataFileName);
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
        string filePath = Path.Combine(Application.persistentDataPath, gameDataFileName);
        File.WriteAllText (filePath, dataAsJson);
    }

    public void ClearGameData()
    {

    }

    private GameData mockUpGameData()
    {
        string[] randomIds = Guid.NewGuid().GetParts();
        GameData emptyGameData = new GameData();
        emptyGameData.Player.Name = randomIds.First();
        emptyGameData.Player.Email = $"{randomIds.First()}@{randomIds.Last()}.com";
        emptyGameData.Player.Score = Random.Range(1, 100);
        emptyGameData.Player.MinutesPlayed = Random.Range(1, 100);
        emptyGameData.Levels[0].Number = Random.Range(1, 50);
        emptyGameData.Levels[0].Name = $"Mock Level Name {randomIds.First()}";
        emptyGameData.Levels[0].StatusState =  Level.Status.Played;
        emptyGameData.Levels[0].DifficultyState =  Level.Difficulty.ExtraHard;
        return emptyGameData;
    }
}
