using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
public class SaveSystem : MonoBehaviour
{
    //public List<GameObject> saveObjects;
    public ISaveable[] saveables;
   
    public void SaveData()
    {
        saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToArray();
        Dictionary<string, Dictionary<string, object>> allData = new Dictionary<string, Dictionary<string, object>>();
        foreach (ISaveable save in saveables)
        {
            allData.Add(save.UniqueID, save.OnSave());
        }
        SaveManager.SaveData(allData);
    }
    public void LoadData()
    {
        //Get our data
        Dictionary<string, Dictionary<string, object>> allData = SaveManager.LoadData<Dictionary<string, Dictionary<string, object>>>();
        if (allData == null)
        {
            Debug.LogWarning("Save File NOT FOUND");
            return;
        }
        //Iterate and load onto our objects
        foreach (ISaveable go in saveables)
        {
            go.OnLoad(allData[go.UniqueID]);
        }
    }
}
