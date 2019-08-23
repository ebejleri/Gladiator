using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//TODO UI FOR SKILLS AND HEALTH/MP
public class CombatManager : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
    }
    public PerformAction combatStates;

    public List<HandleTurn> PerformList = new List<HandleTurn>();
    public List<GameObject> HeroesInBattle = new List<GameObject>();
    public List<GameObject> EnemiesInBattle = new List<GameObject>();
    public List<GameObject> SkillsInBattle = new List<GameObject>();

    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        INPUT3,
        DONE
    }
    public HeroGUI HeroInput;

    public List<GameObject> HeroesToManage = new List<GameObject>();
    private HandleTurn HeroChoice;
    public GameObject enemyButton;
    public GameObject skillButton;
    public Transform EnemySpacer;
    public Transform SkillSpacer;

    public GameObject AttackPanel;
    public GameObject EnemySelectPanel;
    public GameObject SkillPanel;

    public Text HealthText;
    public Text ManaText;
    public Text FlowText;
    // Start is called before the first frame update
    void Start()
    {
        combatStates = PerformAction.WAIT;
        EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HeroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        int currentHealth = HeroesInBattle[0].GetComponent<PlayerStateMachine>().Ashus.getCurrentHealth();
        int currentMana = HeroesInBattle[0].GetComponent<PlayerStateMachine>().Ashus.getCurrentMP();
        int currentAether = HeroesInBattle[0].GetComponent<PlayerStateMachine>().Ashus.getCurrentAether();
        int maxHealth = HeroesInBattle[0].GetComponent<PlayerStateMachine>().Ashus.getMaxHealth();
        int maxMana = HeroesInBattle[0].GetComponent<PlayerStateMachine>().Ashus.getMaxMP();
        int maxAether = HeroesInBattle[0].GetComponent<PlayerStateMachine>().Ashus.getMaxAether();
        HealthText.text = "Health: " + currentHealth + "/" + maxHealth;
        ManaText.text = "Mana: " + currentMana + "/" + maxMana;
        FlowText.text = "Aether: " + currentAether + "/" + maxAether;
        HeroInput = HeroGUI.ACTIVATE;

        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);
        SkillPanel.SetActive(false);

        EnemyButtons();
        SetUpSkills();

    }

    // Update is called once per frame
    void Update()
    {
        switch (combatStates)
        {
            case (PerformAction.WAIT):
                if(PerformList.Count>0)
                {
                    combatStates = PerformAction.TAKEACTION;
                }
                break;

            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(PerformList[0].Attacker);
                if(PerformList[0].Type.Equals("Enemy"))
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    ESM.HeroToAttack = PerformList[0].TargetGameObject;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                    ESM.HeroToAttack.GetComponent<PlayerStateMachine>().Ashus.doDamageToHealth(calculatedDamage(PerformList[0]));
                    int currentHealth = ESM.HeroToAttack.GetComponent<PlayerStateMachine>().Ashus.getCurrentHealth();
                    int currentMana = ESM.HeroToAttack.GetComponent<PlayerStateMachine>().Ashus.getCurrentMP();
                    int currentAether = ESM.HeroToAttack.GetComponent<PlayerStateMachine>().Ashus.getCurrentAether();
                    int maxHealth = ESM.HeroToAttack.GetComponent<PlayerStateMachine>().Ashus.getMaxHealth();
                    int maxMana = ESM.HeroToAttack.GetComponent<PlayerStateMachine>().Ashus.getMaxMP();
                    int maxAether = ESM.HeroToAttack.GetComponent<PlayerStateMachine>().Ashus.getMaxAether();
                    HealthText.text = "Health: " + currentHealth + "/" + maxHealth;
                    ManaText.text = "Mana: " + currentMana + "/" + maxMana;
                    FlowText.text = "Aether: " + currentAether + "/" + maxAether;
                }
                if (PerformList[0].Type.Equals("Hero"))
                {
                    PlayerStateMachine PSM = performer.GetComponent<PlayerStateMachine>();
                    PSM.EnemyToAttack = PerformList[0].TargetGameObject;
                    PSM.currentState = PlayerStateMachine.TurnState.ACTION;
                    PSM.EnemyToAttack.GetComponent<EnemyStateMachine>().thisEnemy.doDamageToHealth(calculatedDamage(PerformList[0]));
                }
                combatStates = PerformAction.PERFORMACTION;
                break;

            case (PerformAction.PERFORMACTION):

                break;
        }
        switch (HeroInput)
        {
            case (HeroGUI.ACTIVATE):
                if (HeroesToManage.Count > 0)
                {
                    HeroChoice = new HandleTurn();
                    AttackPanel.SetActive(true);
                    HeroInput = HeroGUI.WAITING;
                }
                break;
            case (HeroGUI.WAITING):

                break;
            case (HeroGUI.DONE):
                HeroInputDone();
                break;
            case (HeroGUI.INPUT1):

                break;
            case (HeroGUI.INPUT2):

                break;
            case (HeroGUI.INPUT3):

                break;

        }
    }

    public void CollectActions(HandleTurn input)
    {
        PerformList.Add(input);
    }
    void EnemyButtons()
    {

        foreach(GameObject enemy in EnemiesInBattle)
        {
            Debug.Log(enemy.GetComponent<EnemyStateMachine>().thisEnemy.getName());
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine currentEnemy = enemy.GetComponent<EnemyStateMachine>();

            Text buttonText = newButton.GetComponentInChildren<Text>();
            buttonText.text = currentEnemy.thisEnemy.getName();

            button.EnemyPrefab = enemy;

            newButton.transform.SetParent(EnemySpacer,false);
        }
    }
    public void Input1(Button actionChoice)//Action Panel
    {
        HeroChoice.Attacker = HeroesToManage[0].name;
        HeroChoice.AttackerGameObject = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        if(actionChoice.name== "Attack Button")
        {
            SkillPanel.SetActive(false);
            EnemySelectPanel.SetActive(true);
            HeroChoice.choosenAttack=SkillList.NormalAttack();
        }
        else if(actionChoice.name == "Skills Button")
        {
            SkillPanel.SetActive(true);
            EnemySelectPanel.SetActive(false);
        }
        else if(actionChoice.name == "Item Button")
        {
            SkillPanel.SetActive(false);
            EnemySelectPanel.SetActive(false);
        }
        else if(actionChoice.name == "Defend Button")
        {
            SkillPanel.SetActive(false);
            EnemySelectPanel.SetActive(false);
        }
    }
    public void Input2(GameObject skillButton)//Skills Panel
    {
        HeroChoice.choosenAttack = skillButton.GetComponent<SkillSelectButton>().heroChosen.GetComponent<PlayerStateMachine>().Ashus.getSkill(skillButton.GetComponent<SkillSelectButton>().skillChosen.getSkillName());
        EnemySelectPanel.SetActive(true);

    }
    public void Input3(GameObject enemyChoice)//Enemy Panel
    {
        HeroChoice.TargetGameObject = enemyChoice;
        HeroChoice.Target = enemyChoice.GetComponent<EnemyStateMachine>().thisEnemy.getName();
        HeroInput = HeroGUI.DONE;
    }
    public void HeroInputDone()
    {
        PerformList.Add(HeroChoice);
        EnemySelectPanel.SetActive(false);
        SkillPanel.SetActive(false);
        AttackPanel.SetActive(false);
        HeroesToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
    }

    public int calculatedDamage(HandleTurn damageStep)
    {
        /*
        SMT CALCULATION

        
        PHYSICAL
        normal attack:
        damage = (Lvl + Str) *32 ÷15

        level base skill:
        damage = (Lvl + Str) *<skill power> ÷15

        max HP base skill:
        damage = (Max HP) *<skill power> *(7/20) ÷32
        
        IF CRIT MULTIPLY BY 1.5
        MAGICAL 
        Damage = 3 * Skill Power * {2 * Mag + 70 - (0.4 * Lv)} / 100
        IF WEAK POINT MULTIPLY BY 1.4
        */

        double damageDone = 0;
        Skill skillInUse = damageStep.choosenAttack;
        GameObject AttackerObject = GameObject.Find(damageStep.Attacker);
        GameObject TargetObject = GameObject.Find(damageStep.Target);

        if (damageStep.Type.Equals("Enemy"))
        {
            EnemyStateMachine ESM = AttackerObject.GetComponent<EnemyStateMachine>();
            PlayerStateMachine PSM = TargetObject.GetComponent<PlayerStateMachine>();

            Enemy Attacker = ESM.thisEnemy;
            Player Target = PSM.Ashus;

            Debug.Log(Target.getResistances().getResistance(skillInUse.getElement()));

            if (skillInUse.getElement().Equals(Skill.Element.Physical))
            {
                damageDone = ((Attacker.getLevel() + Attacker.getStats().getStatValue(StatList.STRENGTH)) * skillInUse.getDamage()) / 15;
            }
            else
            {
                damageDone = 2 * skillInUse.getDamage() * ((2 * Attacker.getStats().getStatValue(StatList.MAGIC)) + 70 - (0.4 * Attacker.getLevel())) / 100;
            }
            if (Target.getResistances().getResistance(skillInUse.getElement()).Equals(Elemental_Attributes.RESISTANCE_ARRAY[Elemental_Attributes.WEAK]))
            {
                damageDone = damageDone * 1.4;
            }
            else if (Target.getResistances().getResistance(skillInUse.getElement()).Equals(Elemental_Attributes.RESISTANCE_ARRAY[Elemental_Attributes.NORMAL]))
            {

            }
            else if (Target.getResistances().getResistance(skillInUse.getElement()).Equals(Elemental_Attributes.RESISTANCE_ARRAY[Elemental_Attributes.VOID]))
            {
                damageDone = 0;
            }
            else if (Target.getResistances().getResistance(skillInUse.getElement()).Equals(Elemental_Attributes.RESISTANCE_ARRAY[Elemental_Attributes.DRAIN]))
            {
                damageDone = damageDone * -1;
            }
            //ADD REPEL


        }
        if (damageStep.Type.Equals("Hero"))
        {
            PlayerStateMachine PSM = AttackerObject.GetComponent<PlayerStateMachine>();
            EnemyStateMachine ESM = TargetObject.GetComponent<EnemyStateMachine>();

            Player Attacker = PSM.Ashus;
            Enemy Target = ESM.thisEnemy;

            if (skillInUse.getElement().Equals(Skill.Element.Physical))
            {
                damageDone = ((Attacker.getLevel() + Attacker.getStats().getStatValue(StatList.STRENGTH)) * skillInUse.getDamage()) / 15;
            }
            else
            {
                damageDone = 3 * skillInUse.getDamage() * ((2 * Attacker.getStats().getStatValue(StatList.MAGIC)) + 70 - (0.4 * Attacker.getLevel())) / 100;
            }
            if (Target.getResistances().getResistance(skillInUse.getElement()).Equals(Elemental_Attributes.RESISTANCE_ARRAY[Elemental_Attributes.WEAK]))
            {
                damageDone = damageDone * 1.4;
            }
            else if (Target.getResistances().getResistance(skillInUse.getElement()).Equals(Elemental_Attributes.RESISTANCE_ARRAY[Elemental_Attributes.NORMAL]))
            {

            }
            else if (Target.getResistances().getResistance(skillInUse.getElement()).Equals(Elemental_Attributes.RESISTANCE_ARRAY[Elemental_Attributes.VOID]))
            {
                damageDone = 0;
            }
            else if (Target.getResistances().getResistance(skillInUse.getElement()).Equals(Elemental_Attributes.RESISTANCE_ARRAY[Elemental_Attributes.DRAIN]))
            {
                damageDone = damageDone * -1;
            }
            //ADD REPEL
        }

        int tempCrit = Random.Range(0, 100)+1;
        double critToHit = skillInUse.getCritChance() * 100;
        if (tempCrit <= critToHit)
        {
            damageDone = damageDone * 1.5;
        }

        Debug.Log("Damage Done is " + damageDone);

        return (int)damageDone;
    }

    public void SetUpSkills()//Needs a destructor
    {
        foreach (Skill skill in HeroesInBattle[0].GetComponent<PlayerStateMachine>().Ashus.getSkills())
        {
            Debug.Log(skill.getSkillName());
            GameObject newButton = Instantiate(skillButton) as GameObject;
            SkillSelectButton button = newButton.GetComponent<SkillSelectButton>();

            Text buttonText = newButton.GetComponentInChildren<Text>();
            button.skillChosen = skill;
            button.heroChosen = HeroesInBattle[0];
            buttonText.text = skill.getSkillName();

            newButton.transform.SetParent(SkillSpacer, false);
            if (HeroesInBattle[0].GetComponent<PlayerStateMachine>().Ashus.getCurrentMP() < skill.getMPCost())
            {
                newButton.GetComponent<Image>().color = new Color32(125, 125, 125, 255);
            }
        }
    }
    public void DestroySkills()//Fix This
    {
        SkillsInBattle.AddRange(GameObject.FindGameObjectsWithTag("Skill"));
        foreach(GameObject skill in SkillsInBattle)
        {
            Destroy(skill);
        }
        SkillsInBattle.Clear();

    }
}
