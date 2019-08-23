using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : Fighter
{
    [SerializeField]
    int moneyDrop;
    [SerializeField]
    int experienceDrop;
    public Enemy() : base()
    {

    }
    public Enemy(string n, int h, int mp) : base(n, h, mp)
    {

    } 
    public void setMoneyDrop(int m)
    {
        moneyDrop = m;
    }
    public int getMoneyDrop()
    {
        return moneyDrop;
    }
    public void setExperieneceDrop(int xp)
    {
        experienceDrop = xp;
    }
    public int getExperienceDrop()
    {
        return experienceDrop;
    }

   
}
