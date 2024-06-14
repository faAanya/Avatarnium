using System;
using System.ComponentModel;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirectoryPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string fileName)
    {
        dataDirectoryPath = dataDirPath;
        dataFileName = fileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirectoryPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error when load" + fullPath + "\n" + e.Message);
            }
        }
        return loadedData;
    }

    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(dataDirectoryPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(gameData, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Save data error" + fullPath + "\n" + e.Message);
        }

    }

}
