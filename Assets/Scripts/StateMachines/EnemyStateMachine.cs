using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private CombatManager CM;
    public Enemy thisEnemy;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;
    private bool actionStarted = false;
    public bool animationStart;
    public GameObject HeroToAttack;
    private Vector3 startPosition;
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    private float animSpeed = 10f;

    public GameObject Selector;
    // Start is called before the first frame update
    void Start()
    {
        thisEnemy = EnemyList.Skilla();
        startPosition = transform.position;
        currentState = TurnState.PROCESSING;
        Selector.SetActive(false);
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

            case (TurnState.CHOOSEACTION):
                if (thisEnemy.getCurrentHealth() > 0)
                {
                    ChooseAction();
                    currentState = TurnState.WAITING;
                    currentState = TurnState.DEAD;
                }
                else
                {
                    currentState = TurnState.DEAD;
                    Debug.Log("Enemy is Dead :)");
                }                        
                break;

            case (TurnState.WAITING):
                break;

            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;

            case (TurnState.DEAD):
                //Get out of combat
                break;

        }
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = thisEnemy.getName();
        myAttack.Type = "Enemy";
        myAttack.AttackerGameObject = this.gameObject;
        myAttack.TargetGameObject = CM.HeroesInBattle[Random.Range(0, CM.HeroesInBattle.Count)];
        HeroToAttack = myAttack.TargetGameObject;
        myAttack.Target = HeroToAttack.GetComponent<PlayerStateMachine>().Ashus.getName();

        int tempNum = Random.Range(0, thisEnemy.getSkills().Length);
        myAttack.choosenAttack = thisEnemy.getSkill(tempNum);

        Debug.Log(thisEnemy.getName() + " has chosen " + myAttack.choosenAttack.getSkillName());
        Debug.Log("The number rolled was " + tempNum);
        CM.CollectActions(myAttack);
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 0.5f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.x);
        while (MoveTowardsEnemy(heroPosition))
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

    }
    void UpgradeProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        if (cur_cooldown >= max_cooldown)
        {
            currentState = TurnState.CHOOSEACTION;
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

}
