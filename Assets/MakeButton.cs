using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private bool physical;

    private GameObject hero;
    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        hero = GameObject.FindGameObjectWithTag("Hero");
    }

    private void AttachCallback(string btn)
    {
        if (btn.CompareTo("AttackButton") == 0)
        {
            // Debug.Log(hero);
            hero.GetComponent<fighterAction>().SelectAttack("melee");
        } else if (btn.CompareTo("SkillButton") == 0)
        {
            hero.GetComponent<fighterAction>().SelectAttack("range");
        } else
        {
            Debug.Log("run");
            // hero.GetComponent<fighterAction>().SelectAttack("run");
        }
    }
}