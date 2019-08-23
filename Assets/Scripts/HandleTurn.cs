using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurn
{
    public string Attacker;//name of attacker
    public string Target;
    public string Type; //Enemy or Hero
    public GameObject AttackerGameObject;//Who attacks
    public GameObject TargetGameObject;//Who is the target
    public Skill choosenAttack;

    //which attack is performed 
}
