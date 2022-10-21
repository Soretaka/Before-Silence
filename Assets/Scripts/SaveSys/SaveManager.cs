
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    private const string fileExtension = ".save";
    public static void SaveData(object data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player"+fileExtension;
        FileStream stream = new FileStream(path, FileMode.Create);

       
        formatter.Serialize(stream, data);
        Debug.Log("Progress saved!!");
        stream.Close();
    }

    
   
    public static T LoadData<T>()
    {
        object data = null;
        string path = Application.persistentDataPath + "/player" + fileExtension;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            data = formatter.Deserialize(stream);
            stream.Close();
            
        }
        else
        {
            Debug.Log("file not found" + path);
           
        }
        return (T)data;
    }


}
