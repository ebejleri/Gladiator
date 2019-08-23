using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectButton : MonoBehaviour
{
    public Skill skillChosen;
    public GameObject heroChosen;

    public void SelectSkill ()
    {
        GameObject.Find("CombatManager").GetComponent<CombatManager>().Input2(gameObject);
    }

}
