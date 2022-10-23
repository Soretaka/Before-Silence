using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackScript : MonoBehaviour
{
    public GameObject owner;

    public TMP_Text battleText;
    public TMP_Text HPText;

    [SerializeField]
    private string animationName;

    [SerializeField]
    private bool magicAttack;

    [SerializeField]
    private float magicCost;

    [SerializeField]
    private float minAttackMultiplier;

    [SerializeField]
    private float maxAttackMultiplier;

    [SerializeField]
    private float minDefenseMultiplier;

    [SerializeField]
    private float maxDefenseMultiplier;

    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;
    
    public void Attack(GameObject victim)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();
        if (attackerStats.magic >= magicCost)
        {
            float multiplier = Random.Range(minAttackMultiplier, maxAttackMultiplier);

            damage = multiplier * attackerStats.melee;
            if (magicAttack)
            {
                damage = multiplier * attackerStats.magicRange;
            }

            float defenseMultiplier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
            damage = Mathf.Max(0, damage - (defenseMultiplier * targetStats.defense));
            damage = Mathf.Round(damage);
            if(damage > 0)
            {
                battleText.gameObject.SetActive(true);
                battleText.text = damage.ToString();
                Invoke("turnoffBattleText", 1f);
            }
            owner.GetComponent<Animator>().Play(animationName);
            targetStats.ReceiveDamage(Mathf.CeilToInt(damage));
            if(targetStats.healthNow <= 0){
                targetStats.healthNow = 0;
            }
            HPText.text = targetStats.healthNow.ToString() + " / " + targetStats.health.ToString();
            attackerStats.updateMagicFill(magicCost);
        } else
        {
            Invoke("SkipTurnContinueGame", 2);
        }
    }
    void turnoffBattleText()
    {
        battleText.gameObject.SetActive(false);
    }
    void SkipTurnContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn();
    }
}