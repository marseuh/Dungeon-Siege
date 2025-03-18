using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Android;
using System.Collections;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [BoxGroup("File Storage Config")]
    [SerializeField] private string _fileName;
    [BoxGroup("File Storage Config")]
    [SerializeField] private bool _isUsingEncryption;
    [SerializeField] GameConfigSO _gameConfigSO;
    [BoxGroup("Default setting")]
    [SerializeField] WeaponDataSO _defaultWeapon;
    [BoxGroup("Default setting")]
    [SerializeField] CharacterDataSO _defaultCharacter;
    public static DataPersistenceManager instance { get; private set; }

    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager");
        }
        instance = this;
        _dataPersistenceObjects = new List<IDataPersistence>();

        _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _isUsingEncryption);
        FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void Start()
    {
    }

    public void NewGame()
    {
        _gameData = new GameData();
        _gameData.weaponID = _gameConfigSO.GetId(_defaultWeapon);
        _gameData.characterID = _gameConfigSO.GetId(_defaultCharacter);
    }

    public void LoadGame()
    {
        _gameData = _dataHandler.Load();
        //If no data can be loaded, initialize to a new game
        if (_gameData == null)
        {
            Debug.Log("No data was found to be loaded. Initializing data to defaults.");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        //Doesn't work :(
        //maybe work :o
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref _gameData);
        }

        _dataHandler.Save(_gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        dataPersistenceObjects.ToList().ForEach(i => { _dataPersistenceObjects.Add(i); Debug.Log("Adding: " + i + " to dataPersistenceArray"); });
    }

    public GameData GetGameData()
    {
        return _gameData;
    }
}
