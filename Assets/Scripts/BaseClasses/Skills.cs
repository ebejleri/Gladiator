using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public enum Type { Normal, Unblockable, Pierce, TrueHit };
    public enum Element { Physical, Fire, Ice, Electric, Force, Light, Dark };

    [SerializeField]
    string skillName;
    int MPCost;
    int baseDamage;
    float criticalChance;
    Type type;
    Element element;
    string attackDescription;

    public Skill()
    {
        type = Type.Normal;
        element = Element.Physical;
        MPCost = 0;
        baseDamage = 28;
        skillName = "Attack";
        criticalChance = 0;
    }
    public Skill(int d, int mp, string s)
    {
        type = Type.Normal;
        element = Element.Physical;
        MPCost = mp;
        baseDamage = d;
        skillName = s;
    }

    public void setMPCost(int mp)
    {
        MPCost = mp;
    }

    public int getMPCost()
    {
        return MPCost;
    }

    public void setDamage(int d)
    {
        baseDamage = d;
    }

    public int getDamage()
    {
        return baseDamage;
    }

    public void setType(Type t)
    {
        type = t;
    }

    public string getType()
    {
        return type.ToString();
    }

    public void setElement(Element e)
    {
        element = e;
    }

    public string getElement()
    {
        return element.ToString();
    }

    public void setDescription(string description)
    {
        attackDescription = description;

    }

    public string getDescription()
    {
        return attackDescription;
    }

    public void setCritChance(float crit)
    {
        criticalChance = crit;
    }

    public float getCritChance()
    {
        return criticalChance;
    }
    
    public void setSkillName(string s)
    {
        skillName = s;
    }

    public string getSkillName()
    {
        return skillName;
    }
}
