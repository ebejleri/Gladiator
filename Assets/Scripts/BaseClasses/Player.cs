using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Fighter
{
    [SerializeField]
    int maxAetherPoints;
    [SerializeField]
    int currentAetherPoints;
    [SerializeField]
    int moneyAmount=0;
    [SerializeField]
    int expAmount=0;

    public Player() : base()
    {
        Elemental_Attributes baseResistance = new Elemental_Attributes("Normal", "Normal", "Normal", "Normal", "Normal", "Void", "Void");
        base.setResistances(baseResistance);
        maxAetherPoints = 20;
        currentAetherPoints = 0;
        moneyAmount = 20;

        addSkillToList(SkillList.HuntersSting());
        addSkillToList(SkillList.Infel());

        base.setName("Ashus");
    }

    public void changeResistance(int index, int resistanceChange) //Get resistance and replace it
    {
        Elemental_Attributes tempEA = base.getResistances();
        tempEA.setResistance(index, resistanceChange);
        base.setResistances(tempEA);
    }


    public void addAether(int ap)
    {
        if (currentAetherPoints < maxAetherPoints)
        {
            currentAetherPoints = currentAetherPoints + ap;
        }
        if (currentAetherPoints > maxAetherPoints)
        {
            currentAetherPoints = maxAetherPoints;
        }
    }
    public void subtractAether(int ap)
    {
        if(currentAetherPoints < ap)
        {
            currentAetherPoints = 0;
        }
        if(currentAetherPoints >= ap)
        {
            currentAetherPoints = currentAetherPoints - ap;
        }
    }
    public void resetAether()
    {
        currentAetherPoints = maxAetherPoints;
    }

    public void setMaxAether(int ap)
    {
        maxAetherPoints = ap;
    }

    public int getCurrentAether()
    {
        return currentAetherPoints;
    }

    public int getMaxAether()
    {
        return maxAetherPoints;
    }
    
    public void addMoney(int m)
    {
        moneyAmount += m;
    }

    public bool removeMoney(int m)
    {
        if (m > moneyAmount)
        {
            return false;
        }
        else
        {
            moneyAmount -= m;
            return true;
        }
    }

    public void addExp(int xp)
    {
        expAmount += xp;
    }

    public void levelUp()
    {
        Level++;
    }

}
