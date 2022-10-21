using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveableBehaviour : MonoBehaviour
{
    // Inheritors have to implement this (just like with an interface), tp ternyata gabisa grgr cmn bisa double inherit dr interface AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
    public string UniqueID { get; set; }
    
    public abstract Dictionary<string, object> OnSave();
    public abstract void OnLoad(Dictionary<string, object> data);



    private static Dictionary<string, Dictionary<string, object>> allData = new Dictionary<string, Dictionary<string, object>>();

    public static Dictionary<string, Dictionary<string, object>> Instances => new Dictionary<string, Dictionary<string, object>>(allData);

    protected virtual void Awake()
    {
        // simply register yourself to the existing instances
        allData.Add(nameof(this.gameObject),this.OnSave());
    }

    protected virtual void OnDestroy()
    {
        // don't forget to also remove yourself at the end of your lifetime
        allData.Remove(nameof(this.gameObject));
    }
}
