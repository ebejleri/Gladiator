using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental_Attributes
{
    public const int PHYSICAL = 0;
    public const int FIRE = 1;
    public const int ICE = 2;
    public const int ELECTRIC = 3;
    public const int FORCE = 4;
    public const int LIGHT = 5;
    public const int DARK = 6;

    public const int WEAK = 0;
    public const int NORMAL = 1;
    public const int STRONG = 2;
    public const int VOID = 3;
    public const int REPEL = 4;
    public const int DRAIN = 5;

    string[] resistances = new string[7] { "Normal", "Normal", "Normal", "Normal", "Normal", "Normal", "Normal"};
    string[] elements = new string[7] { "Physical", "Fire", "Ice", "Electric", "Force", "Light", "Dark" };

    public static readonly string[] RESISTANCE_ARRAY = new string[6] { "Weak", "Normal", "Strong", "Void", "Repel", "Drain" };

    public Elemental_Attributes()
    {
        
    }

    public Elemental_Attributes(string s1, string s2, string s3, string s4, string s5, string s6, string s7)
    {
        resistances[PHYSICAL] = s1;
        resistances[FIRE] = s2;
        resistances[ICE] = s3;
        resistances[ELECTRIC] = s4;
        resistances[FORCE] = s5;
        resistances[LIGHT] = s6;
        resistances[DARK] = s7;
    }

    public void setResistance(int elementIndex, int resistanceIndex)
    {
        resistances[elementIndex] = RESISTANCE_ARRAY[resistanceIndex];
    }

    //Returns what resistance it is (Weak, Normal, ect...)
    public string getResistance(int elementIndex)
    {
        return resistances[elementIndex];
    }
    public string getResistance(string elementIndex)
    {
        string temp = elementIndex.ToLower();
        switch (temp)
        {
            case ("physical"):
                return resistances[PHYSICAL];
            case ("fire"):
                return resistances[FIRE];
            case ("ice"):
                return resistances[ICE];
            case ("electric"):
                return resistances[ELECTRIC];
            case ("force"):
                return resistances[FORCE];
            case ("light"):
                return resistances[LIGHT];
            case ("dark"):
                return resistances[DARK];
            default:
                return null;
        }
    }

    public string[] getFullResistances()
    {
        return resistances;
    }
}
