using UnityEngine;
using System;
using System.Collections;

public class CharacterBattler : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private int health = 100;

    public HealthBarUI healthBarUI;

    public bool isPlayerTeam;
    private Action onSlideComplete;
    private GameObject selectionCircleObject;
    private HealthSystem healthSystem;

    private State state = State.Idle;

    private enum State{
        Idle,
        Attacking,
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectionCircleObject = transform.Find("shadow").gameObject;

        healthSystem = new HealthSystem(health);
        healthBarUI = GetComponentInChildren<HealthBarUI>();
        healthBarUI.SetMaxHealth(health);

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
            spriteRenderer.sprite = BattleHandler.GetInstance().playerSprite;
        }else{
            spriteRenderer.sprite = BattleHandler.GetInstance().enemySprite;
        }
        healthSystem = new HealthSystem(health);

        PlayIdleAnimation();
    }

    public void Attack(CharacterBattler target , Action onAttackComplete = null){
        Vector3 targetPosition = target.transform.position + this.transform.position.normalized; 
        Vector3 startPosition = this.transform.position;

        //Slide to target position
        SlideToPosition(targetPosition, () => {
            //Attack target            
            state = State.Attacking;
            //TODO : When we have animations : Play attack animation
            PlayAttackAnimation();
            target.Damage(attackDamage);
            //When done with attack, go back to start position
            SlideToPosition(startPosition, () => {
                state = State.Idle;
                onAttackComplete();
            });
        });
    }

    public void Damage(int damageAmount){
        healthSystem.Damage(damageAmount);
        if(healthBarUI != null){
            healthBarUI.UpdateHealthBar(healthSystem.currentHealth);
        }
        if(healthSystem.IsDead()){
            //Die();
        }
    }

    public void Heal(int healAmount){
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

    //TODO : Implement this method 
    //WARNING: Could be in another file if you want to separate logic
    private void PlayAttackAnimation(){
        //TODO : Play attack animation
    }

    public void PlayIdleAnimation(){
        //TODO : Play idle animation
    }

}
