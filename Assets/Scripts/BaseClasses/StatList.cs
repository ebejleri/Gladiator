using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatList
{
    public const int STRENGTH = 0;
    public const int MAGIC = 1;
    public const int AGILITY = 2;
    public const int LUCK = 3;
    string[] statNames = new string[4] { "Strength", "Magic", "Agility", "Luck" };
    int[] statValues = new int[4] { 0, 0, 0, 0 };

    public StatList()
    {
        statValues[STRENGTH] = 3;
        statValues[MAGIC] = 3;
        statValues[AGILITY] = 3;
        statValues[LUCK] = 3;
    }
    public StatList(int s, int m, int a, int l)
    {
        statValues[STRENGTH] = s;
        statValues[MAGIC] = m;
        statValues[AGILITY] = a;
        statValues[LUCK] = l;
    }

    public void setStatValue(int statIndex, int value)
    {
        statValues[statIndex] = value;
    }

    public int getStatValue(int statIndex)
    {
        return statValues[statIndex];
    }

    public void incrementStat(int statIndex)
    {
        statValues[statIndex]++;
    }

}
