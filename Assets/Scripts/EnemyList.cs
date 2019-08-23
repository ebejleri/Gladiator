using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyList
{
    public static Enemy Skilla()
    {
        Enemy enemy = new Enemy();
        enemy.setMaxHealth(100);
        enemy.setCurrentHealth(100);
        enemy.setCurrentMP(20);
        enemy.setMaxMP(20);
        enemy.setExperieneceDrop(100);
        enemy.setMoneyDrop(50);
        enemy.setName("Skilla");
        enemy.addSkillToList(SkillList.NormalAttack());
        enemy.addSkillToList(SkillList.Infel());
        return enemy;
    }
    public static Enemy Dergon()
    {
        Enemy enemy = new Enemy();
        return enemy;
    }
    public static Enemy Jebol()
    {
        Enemy enemy = new Enemy();
        return enemy;
    }
    public static Enemy Billy()
    {
        Enemy enemy = new Enemy();
        return enemy;
    }
    public static Enemy Cleo()
    {
        Enemy enemy = new Enemy();
        return enemy;
    }
}
