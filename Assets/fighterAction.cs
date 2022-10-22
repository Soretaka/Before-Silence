using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject hero;

    [SerializeField]
    private GameObject NormalAttackPreFab;

    [SerializeField]
    private GameObject SkillAttackPreFab;

    [SerializeField]
    private Sprite FaceIcon;

    private GameObject currentAttack;
    private GameObject normalAttack;
    private GameObject skillAttack;

    public void SelectAttack(string btn)
    {
        GameObject victim = hero;
        if(tag=="Hero"){
            currentAttack = normalAttack;
        }
        if (btn.CompareTo("normalAttack") == 0)
        {
            Debug.Log("normalAttack");
        } else if (btn.CompareTo("skillAttack") == 0)
        {
            Debug.Log("skillAttack");
        }else if (btn.CompareTo("item") == 0)
        {
            Debug.Log("item");
        }else if (btn.CompareTo("status") == 0)
        {
            Debug.Log("run");
        }else{
            Debug.Log("huh");
        }
    }
}
