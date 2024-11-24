using UnityEngine;
using System;
using System.Collections;

public class CharacterBattler : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private int attackDamage;
    [SerializeField] private int health;

    public HealthBarUI healthBarUI;
    public GameObject dicePrefab;

    public bool isPlayerTeam;
    private Action onSlideComplete;
    private GameObject selectionCircleObject;
    private HealthSystem healthSystem;

    private int diceResult;


    private State state = State.Idle;

    private enum State{
        Idle,
        Attacking,
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectionCircleObject = transform.Find("shadow").gameObject;

        HideSelectionCircle();
    }

    private void Update(){
        switch(state){
            case State.Idle:
                break;
            case State.Attacking:
                float slideSpeed = 15f;
                transform.position += (targetPosition - transform.position).normalized * slideSpeed * Time.deltaTime;

                float reachedDistance = 0.1f;
                if(Vector3.Distance(transform.position, targetPosition) < reachedDistance){
                    transform.position = targetPosition;
                    onSlideComplete();
                }
                break;
        }
    }

    public void Setup(bool isPlayerTeam){
        this.isPlayerTeam = isPlayerTeam;
        if(isPlayerTeam){
            health = 100;
            attackDamage = 15;
            spriteRenderer.sprite = BattleHandler.GetInstance().playerSprite;
        }else{
            switch(GameManager.instance.currentPart){
                case 0:
                    health = 50;
                    attackDamage = 5;
                    spriteRenderer.sprite = BattleHandler.GetInstance().enemySprite;
                    break;
                case 1:
                    health = 60;
                    attackDamage = 10;
                    spriteRenderer.sprite = BattleHandler.GetInstance().enemySprite;
                    break;
                case 2:
                    health = 70;
                    attackDamage = 15;
                    spriteRenderer.sprite = BattleHandler.GetInstance().enemySprite;
                    break;
                case 3:
                    health = 80;
                    attackDamage = 20;
                    spriteRenderer.sprite = BattleHandler.GetInstance().enemySprite;
                    break;
                case 4:
                    health = 90;
                    attackDamage = 25;
                    spriteRenderer.sprite = BattleHandler.GetInstance().enemySprite;
                    break;
                default:
                    spriteRenderer.sprite = BattleHandler.GetInstance().enemySprite;
                    break;
            }
            spriteRenderer.sprite = BattleHandler.GetInstance().enemySprite;
        }
        healthSystem = new HealthSystem(health);
        healthBarUI = GetComponentInChildren<HealthBarUI>();
        healthBarUI.SetMaxHealth(health);

        PlayIdleAnimation();
    }

    public void Attack(CharacterBattler target, Action onAttackComplete = null)
{
    StartCoroutine(AttackCoroutine(target, onAttackComplete));
}

private IEnumerator AttackCoroutine(CharacterBattler target, Action onAttackComplete)
{
    // Roll the dice
    yield return StartCoroutine(RollDiceCoroutine());

    // After the dice roll, proceed with the attack
    Vector3 targetPosition = target.transform.position + this.transform.position.normalized; 
    Vector3 startPosition = this.transform.position;

    // Slide to target position
    SlideToPosition(targetPosition, () => {
        // Attack target            
        state = State.Attacking;
        // TODO: When we have animations, play attack animation
        PlayAttackAnimation();
        target.Damage(attackDamage, diceResult);
        Heal(diceResult);

        // When done with the attack, go back to the start position
        SlideToPosition(startPosition, () => {
            state = State.Idle;
            onAttackComplete?.Invoke();
        });
    });
}


    public void Damage(int damageAmount, int diceResult){
        int realDamageAmount;
        switch(diceResult){
        case 1:
            realDamageAmount = damageAmount - (int)(attackDamage * 0.5f);
            break;
        case 2:
            realDamageAmount = damageAmount - (int)(attackDamage * 0.25f);
            break;
        case 3:
            realDamageAmount = damageAmount - (int)(attackDamage * 0.1f);
            break;
        case 4:
            realDamageAmount = damageAmount;
            break;
        case 5:
            realDamageAmount = damageAmount + (int)(attackDamage * 0.25f);
            break;
        case 6:
            realDamageAmount = damageAmount + (int)(attackDamage * 0.5f);
            break;
            default:
                Debug.Log("Broke i guess idk");
                realDamageAmount = damageAmount;
                break;
        }
        healthSystem.Damage(realDamageAmount);
        if(healthBarUI != null){
            healthBarUI.UpdateHealthBar(healthSystem.currentHealth);
        }
    }
    public void Heal(int diceResult){
        int healAmount;
        switch(diceResult){
            case 1:
            case 2:
            case 3:
                healAmount = 0;
                break;
            case 4:
                healAmount = (int)(healthSystem.maxHealth * 0.05f); //Heal 5% of max health
                break;
            case 5:
                healAmount = (int)(healthSystem.maxHealth * 0.07f); //Heal 7% of max health
                break;
            case 6:
                healAmount = (int)(healthSystem.maxHealth * 0.10f); //Heal 10% of max health
                break;
            default:
                Debug.Log("Broke i guess idk");
                healAmount = 0;
                break;
        }
        healthSystem.Heal(healAmount);
        if(healthBarUI != null){
            healthBarUI.UpdateHealthBar(healthSystem.currentHealth);
        }
    }

    public bool IsDead(){
        return healthSystem.IsDead();
    }

    private void SlideToPosition(Vector3 targetPosition, Action onSlideComplete){
        this.targetPosition = targetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Attacking;
    }

    public void HideSelectionCircle(){
        selectionCircleObject.SetActive(false);
    }

    public void ShowSelectionCircle(){
        selectionCircleObject.SetActive(true);
    }

    private IEnumerator RollDiceCoroutine()    {
        Vector3 diceOffset = new Vector3(0,-1.5f , 0);
        GameObject diceInstance = Instantiate(dicePrefab, transform.position + diceOffset, Quaternion.identity);
        Dice dice = diceInstance.GetComponent<Dice>();
        yield return dice.RollTheDice();
        diceResult = dice.GetFinalSide();
        yield return new WaitForSeconds(0.7f);
        Destroy(diceInstance);
    }

    //TODO : Implement this method 
    //WARNING: Could be in another file if you want to separate logic
    private void PlayAttackAnimation(){
        //TODO : Play attack animation
    }

    public void PlayIdleAnimation(){
        //TODO : Play idle animation
    }

}
