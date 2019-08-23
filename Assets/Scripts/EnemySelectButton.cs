using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public void SelectEnemy()
    {
        GameObject.Find("CombatManager").GetComponent<CombatManager>().Input3(EnemyPrefab);
    }

    public void HideSelector()
    {

        EnemyPrefab.transform.Find("EnemySelector").gameObject.SetActive(false);

    }
    public void ShowSelector()
    {

        EnemyPrefab.transform.Find("EnemySelector").gameObject.SetActive(true);

    }
}
