using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private CombatManager CM;
    public Player Ashus;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    //progress bar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    //IEnumerator
    private bool actionStarted = false;
    public GameObject EnemyToAttack;
    private Vector3 startPosition;
    private float animSpeed = 10f;

    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Ashus = new Player();
        cur_cooldown = Random.Range(0, 6-Ashus.getStats().getStatValue(0));
        currentState = TurnState.PROCESSING;
        CM = GameObject.Find("CombatManager").GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;

            case (TurnState.ADDTOLIST):
                CM.HeroesToManage.Add(this.gameObject);
                currentState = TurnState.SELECTING;
                break;

            case (TurnState.SELECTING):
                if (Ashus.getCurrentHealth() <= 0)
                {
                    currentState = TurnState.DEAD;
                    Debug.Log("This Hero is dead :(");
                }
                //TODO MAKE ALL THE ANMIATIONS GO AND GO BACK TO ADD TO LIST
                break;

            case (TurnState.ACTION):
                if (Ashus.getCurrentHealth() > 0)
                {
                    StartCoroutine(TimeForAction());
                }
                else
                {
                    currentState = TurnState.DEAD;
                    Debug.Log("This Hero is dead :(");
                }
                break;

            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else
                {
                    //Change tag
                    this.gameObject.tag = "Dead Hero";
                    //Not attackable by enemy
                    CM.HeroesInBattle.Remove(this.gameObject);
                    //Not manageable
                    CM.HeroesToManage.Remove(this.gameObject);
                    //Reset GUI
                    CM.AttackPanel.SetActive(false);
                    CM.EnemySelectPanel.SetActive(false);
                    CM.SkillPanel.SetActive(false);
                    //Remove Item from perform list
                    for(int i = 0; i<CM.PerformList.Count; i++)
                    {
                        if(CM.PerformList[i].AttackerGameObject == this.gameObject)
                        {
                            CM.PerformList.Remove(CM.PerformList[i]);
                        }
                    }
                    //Change Color/play animation
                    this.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
                    CM.HeroInput = CombatManager.HeroGUI.ACTIVATE;
                    alive = false;
                    StartCoroutine(transitionToDeathScreen());
                    //Reset Hero input
                }
                //Get out of combat
                break;
                
        }
    }
    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;
        Vector3 enemyPosition = new Vector3(EnemyToAttack.transform.position.x - 0.5f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.x);
        while (MoveTowardsEnemy(enemyPosition))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Vector2 firstPosition = startPosition;
        while (MoveTowardsStart(firstPosition))
        {
            yield return null;
        }
        CM.PerformList.RemoveAt(0);
        CM.combatStates = CombatManager.PerformAction.WAIT;
        actionStarted = false;
        cur_cooldown = 0f;
        currentState = TurnState.PROCESSING;
        CM.AttackPanel.SetActive(false);
        CM.DestroySkills();
        //CM.SetUpSkills();
   
    }
    void UpgradeProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        if(cur_cooldown>=max_cooldown)
        {
            currentState = TurnState.ADDTOLIST;
            CM.AttackPanel.SetActive(true);
        }
    }
    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private IEnumerator transitionToDeathScreen()
    {

        yield return null;
    }
}
