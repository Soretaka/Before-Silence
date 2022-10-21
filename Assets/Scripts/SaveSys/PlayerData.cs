using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData 
{
    public int levelAt;
    public int bestTime;
    public int totalpp;
    public int maxlvlReach;
    public int standardTime;
    public PlayerData(int levelAt, int best, int totalpp, int maxlvlReach, int standard)
    {
        this.levelAt = levelAt;
        this.bestTime = best;
        this.totalpp = totalpp;
        this.maxlvlReach = maxlvlReach;
        this.standardTime = standard;
    }
}
