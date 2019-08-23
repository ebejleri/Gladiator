using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter 
{
    [SerializeField]
    protected Skill[] skills = new Skill[10];
    [SerializeField]
    protected Elemental_Attributes resistances;
    [SerializeField]
    protected string FighterName;
    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    protected int currentHealth;
    [SerializeField]
    protected int maxManaPoints;
    [SerializeField]
    protected int currentManaPoints;
    [SerializeField]
    protected StatList statList;
    [SerializeField]
    protected int skillIterator = 0;
    [SerializeField]
    protected bool fullSkillList = false;
    [SerializeField]
    protected int Level = 1; //TODO Make own class for this

    protected Fighter()
    {
        resistances = new Elemental_Attributes();
        FighterName = "Gladiator";
        maxHealth = 100;
        currentHealth = 100;
        maxManaPoints = 20;
        currentManaPoints = 20;
        statList = new StatList();

    }

    protected Fighter(string n, int h, int mp)
    {
        resistances = new Elemental_Attributes();
        FighterName = n;
        maxHealth = h;
        currentHealth = h;
        maxManaPoints = mp;
        currentManaPoints = mp;
    }

    public void setMaxHealth(int h)
    {
        maxHealth = h;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void setCurrentHealth(int h)
    {
        currentHealth = h;
    } 
    public int getCurrentHealth()
    {
        return currentHealth;
    }
    public void doDamageToHealth(int damage)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public string getName()
    {
        return FighterName;
    }
    public void setName(string n)
    {
        FighterName = n;
    }

    public void setMaxMP(int mp)
    {
        maxManaPoints = mp;
    }
    public int getMaxMP()
    {
        return maxManaPoints;
    }

    public void setCurrentMP(int mp)
    {
        currentManaPoints = mp;
    }
    public int getCurrentMP()
    {
        return currentManaPoints;
    }


    public void addSkillToList(Skill s, int index)
    {
        if (isSkillListFull())
        {
            skills[index] = s;
        }
        else
        {
            addSkillToList(s);
        }
    }

    public void addSkillToList(Skill s)
    {
        if (skillIterator < 10)
        {
            skills[skillIterator] = s;
            skillIterator++;
        }
        else
        {
            fullSkillList = true;
        }
    }

    public bool isSkillListFull()
    {
        return fullSkillList;
    }

    public Skill getSkill(int index)
    {
        int num = index;
        if (index>skillIterator+1)
        {
           num = Random.Range(0, skillIterator+1);

        }
        return skills[num];
    }

    public Skill getSkill(string skillName)
    {
        for(int i = 0; i<skillIterator+1; i++)
        {
            if (skillName.Equals(skills[i].getSkillName()))
            {
                return skills[i];
            }
        }
        return null;
    }
    public void setSkills(Skill[] s)
    {
        skills = s;
    }
    public Skill[] getSkills()
    {
        return skills;
    }

    public Elemental_Attributes getResistances()
    {
        return resistances;
    }
    public void setResistances(Elemental_Attributes ea)
    {
        resistances = ea;
    }

    public StatList getStats()
    {
        return statList;
    }
    public void setStats(StatList sl)
    {
        statList = sl;
    }

    public int getLevel()
    {
        return Level;
    }
    public void setLevel(int l)
    {
        Level = l;
    }


}
