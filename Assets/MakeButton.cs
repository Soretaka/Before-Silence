using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private bool normalAttack;
    // Start is called before the first frame update
    
    private GameObject hero;
    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => AttachCallback(temp));
        hero = GameObject.FindGameObjectWithTag("Hero");
    }

    private void AttachCallback(string btn)
    {

        if (btn.CompareTo("AttackButton") == 0)
        {
            hero.GetComponent<fighterAction>().SelectAttack("normalAttack");
        } else if (btn.CompareTo("SkillButton") == 0)
        {
            hero.GetComponent<fighterAction>().SelectAttack("skillAttack");
        }else if (btn.CompareTo("ItemButton") == 0)
        {
            hero.GetComponent<fighterAction>().SelectAttack("item");
        }else if (btn.CompareTo("StatusButton") == 0)
        {
            hero.GetComponent<fighterAction>().SelectAttack("status");
        }
    }
}
