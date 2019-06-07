using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardArg1

{
    //奖励的道具数组
    private Item[] rewards;
    public Item[]Rewards
    {
        get
        {
            return rewards;
        }

    }

    private int exp;
    public int Exp { get { return exp; } }

    private int money;
    public int Money { get { return money; } }

    public RewardArg1(Item[] items, int exp, int money)
    {
        rewards = items;
        this.exp = exp;
        this.money = money;
    }

    public RewardArg1() : this(null, 0, 0)
    {

    }
}


