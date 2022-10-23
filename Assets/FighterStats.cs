using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class FighterStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject magicFill;

    [Header("Stats")]
    public float health;
    public float magic;
    public float melee;
    public float magicRange;
    public float defense;
    public float speed;
    public float experience;

    private float startHealth;
    private float startMagic;

    public float healthNow;
    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    // Resize health and magic bar
    private Transform healthTransform;
    private Transform magicTransform;

    private Vector2 healthScale;
    private Vector2 magicScale;

    private float xNewHealthScale;
    private float xNewMagicScale;

    private GameObject GameControllerObj;


    void Awake()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;
        healthNow = health;
        startHealth = health;
        Debug.Log("Health: " + health + "startHealth" + startHealth);
        magicTransform = magicFill.GetComponent<RectTransform>();
        magicScale = magicFill.transform.localScale;
        startMagic = magic;

        GameControllerObj = GameObject.Find("GameControllerObject");
    }

    public void ReceiveDamage(float damage)
    {
        healthNow = healthNow - damage;
        animator.Play("Damage");
        // Set damage text

        if(healthNow <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";
            Destroy(healthFill);
            Destroy(gameObject);
        } else if (damage > 0)
        {
            xNewHealthScale = healthScale.x * (healthNow / startHealth);
            // Debug.Log(xNewHealthScale);
            // Debug.Log(healthScale.x);
            // Debug.Log(healthNow);
            // Debug.Log(startHealth);
            // var tmp = healthFill.transform.position;
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
            // Debug.Log(xNewHealthScale);
            // tmp= new Vector3(tmp.x - (tmp.x*(xNewHealthScale-1)), tmp.y,0);
            // healthFill.transform.position = tmp;
        }
        Invoke("ContinueGame", 2);
    }

    public void updateMagicFill(float cost)
    {
        if(cost > 0)
        {
            magic = magic - cost;
            xNewMagicScale = magicScale.x * (magic / startMagic);
            magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);
        }
    }

    public bool GetDead()
    {
        return dead;
    }

    void ContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }
    public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
    }

    public int CompareTo(object otherStats)
    {
        int nex = nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
        return nex;
    }

}

