using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System;

[Serializable]
public class SaveManager : MonoBehaviour
{
    public static SaveData data;

    public static SaveManager instance;

    // Use this for initialization
    void Awake()
    {
        SaveManager.instance = this;
        SaveManager.data = new SaveData();
    }

    public void save()
    {
        Debug.Log("Saving ... ");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Create);
        bf.Serialize(file, data);
        file.Close();

        //ApiConnection.updateFamily(Familly);
    }

    public void load()
    {
        Debug.Log("Start Loading ...");
        if (File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
            try
            {
                data = (SaveData)bf.Deserialize(file);
                file.Close();
                Debug.Log("Scene loaded ! ");
            }
            catch (SerializationException e)
            {
                file.Close();
                Debug.Log("Except : " + e.Data);
            }
        }
    }
}