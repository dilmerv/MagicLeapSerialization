using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

[RequireComponent(typeof(ControllerConnectionHandler))]
public class TestSerializer : MonoBehaviour
{
    [SerializeField]
    private Text serializedData = null;

    [SerializeField]
    private Text serializerStatus = null;
    
    private const string DATA_NOT_LOADED = "Game Data could not be loaded...";

    private ControllerConnectionHandler _controllerConnectionHandler;
    
    [SerializeField]
    private GameData gameData = null;

    private SerializerManager serializerManager = null;

    void Start()
    {
        // get serialized data
        serializerManager = SerializerManager.Instance;      
        gameData = serializerManager.LoadGameData();

        PopulateUIGameData();

        // get controller
        _controllerConnectionHandler = GetComponent<ControllerConnectionHandler>();
        MLInput.OnTriggerDown += HandleOnTriggerDown;
    }

    void Update()
    {
        #if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.S))
            {
                HandleOnTriggerDown(byte.MinValue, float.MinValue);
            }
        #endif
    }

    void PopulateUIGameData()
    {
        if(gameData != null)
        {
            serializedData.text = string.Empty;
            serializedData.text += $"Player Name: {gameData.Player.Name}\n";
            serializedData.text += $"Player Age: {gameData.Player.Age}\n";
            serializedData.text += $"Player Email: {gameData.Player.Email}\n";
            serializedData.text += $"Player Minutes Played: {gameData.Player.MinutesPlayed}\n";
            serializedData.text += $"Player Score: {gameData.Player.Score}\n";
            serializerStatus.text = "Completed";
        }
        else {
            Debug.Log(DATA_NOT_LOADED);
            serializerStatus.text = "Error";
        }
    }

     /// <summary>
    /// Handles the event for trigger down.
    /// </summary>
    /// <param name="controller_id">The id of the controller.</param>
    /// <param name="value">The value of the trigger button.</param>
    private void HandleOnTriggerDown(byte controllerId, float value)
    {
        bool isEditor = false;
        #if UNITY_EDITOR
            isEditor = true;
        #endif

        MLInputController controller = _controllerConnectionHandler?.ConnectedController;
        if ((controller != null && controller.Id == controllerId) || isEditor)
        {
            // add 100 to score / 1 min to time played then save serialized data
            gameData.Player.Score += 100;
            gameData.Player.MinutesPlayed += 1;
            try
            {
                serializerManager.SaveGameData(gameData);
                // reload JSON data from filesystem
                gameData = serializerManager.LoadGameData();
                PopulateUIGameData();
            }
            catch(System.Exception e)
            {
                serializedData.text = e.Message;
                serializerStatus.text = "Error";
            }
        }
    }
}
