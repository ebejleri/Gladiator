using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    //SMT CALCULATION

    /*
    PHYSICAL
    normal attack:
    damage = (Lvl + Str) *32 ÷15

    level base skill:
    damage = (Lvl + Str) *<skill power> ÷15

    max HP base skill:
    damage = (Max HP) *<skill power> *(7/20) ÷32
    */
    //IF CRIT MULTIPLY BY 1.5
    //MAGICAL 
    //Damage = 3 * Skill Power * {2 * Mag + 70 - (0.4 * Lv)} / 100
    //IF WEAK POINT MULTIPLY BY 1.4


}
