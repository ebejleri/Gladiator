using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillList
{
    //Holds all the skils in the game and can returns the skill with information and all
    public static Skill NormalAttack()
    {
        Skill normalAttack = new Skill();
        normalAttack.setCritChance(0.05f);
        return normalAttack;
    }
    public static Skill Infel()
    {
        Skill infel = new Skill(30, 4, "Infel");
        infel.setElement(Skill.Element.Fire);
        infel.setDescription("The Hunter channels magic to ligthly slash the enemy with fire");
        return infel;
    }
    public static Skill HuntersSting()
    {
        Skill huntersSting = new Skill(40, 5, "Hunter's Sting");
        huntersSting.setDescription("A quick light stab that is a basic move all hunters learn");
        huntersSting.setCritChance(0.10f);
        return huntersSting;
    }

    //Enemy Skills
}
