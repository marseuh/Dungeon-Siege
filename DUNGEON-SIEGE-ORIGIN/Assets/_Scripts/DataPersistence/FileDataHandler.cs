using System.Collections;
using UnityEngine.Android;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";
    private bool _IsUsingEncryption = false;
    private readonly string encryptionCodeWord = "AVeryLongAndWindingCodeWordThatIsActuallyUnbreakableTrustMeOnMeMumBruv";

    public FileDataHandler(string dataDirPath, string dataFileName, bool isUsingEncryption)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
        _IsUsingEncryption = isUsingEncryption;
    }   

    public GameData Load()
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                Debug.Log("Found Data To Load");
                //Load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (_IsUsingEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                //Deserialize the data from Json back into readable C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        try
        {
            //Create the directory the file will be written to
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize the c# game data object to Json
            string dataToStore = JsonUtility.ToJson(data, true);

            if (_IsUsingEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //Write the serialized data
            using (FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            Debug.Log("Data written to :" + fullPath + " without error");
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
            throw;
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
