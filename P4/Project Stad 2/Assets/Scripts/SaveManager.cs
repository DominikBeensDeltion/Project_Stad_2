using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SaveManager : MonoBehaviour 
{

    public SaveData saveData;

    public void Start()
    {
        if (System.IO.File.Exists(Application.dataPath + "/SaveData.xml"))
        {
            saveData = Load();
            GameManager.highScore = saveData.highScore;
        }
        else
        {
            saveData = new SaveData();
        }
    }

    public void Save(SaveData toSave)
    {
        var serializer = new XmlSerializer(typeof(SaveData));
        using (var stream = new FileStream(Application.dataPath + "/SaveData.xml", FileMode.Create))
        {
            serializer.Serialize(stream, toSave);
        }
    }

    public SaveData Load()
    {
        var serializer = new XmlSerializer(typeof(SaveData));
        using (var stream = new FileStream(Application.dataPath + "/SaveData.xml", FileMode.Open))
        {
            return serializer.Deserialize(stream) as SaveData;
        }
    }
}
