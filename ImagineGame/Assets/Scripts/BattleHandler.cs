using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    public Sprite playerSprite;
    public Sprite enemySprite;

    private CharacterBattler playerCharacter;
    private CharacterBattler enemyCharacter;
    private CharacterBattler currentCharacter;
    private BattleState battleState;

    private static BattleHandler instance;

    public static BattleHandler GetInstance(){
        return instance;
    }


    private enum BattleState{
        WaitingForInput,
        Busy,
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        playerCharacter = SpawnCharacter(true);
        enemyCharacter = SpawnCharacter(false);
        SetActiveCharacter(playerCharacter);
        battleState = BattleState.WaitingForInput;
    }

    private void Update()
    {
        if(battleState == BattleState.WaitingForInput){
            if(Input.GetKeyDown(KeyCode.Space)){
                battleState = BattleState.Busy;
                playerCharacter.Attack(enemyCharacter, () => {
                    //battleState = BattleState.WaitingForInput;
                    ChooseNextCharacter();
                });
            }
        }
    }

    private CharacterBattler SpawnCharacter(bool isPlayerTeam){
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        if(isPlayerTeam){
            spawnPosition = new Vector3(-4.2f, 0, 0);
        }else{
            spawnPosition = new Vector3(4.2f, 0, 0);
        }
        GameObject character = Instantiate(_player, spawnPosition, Quaternion.identity);
        CharacterBattler characterBattler = character.GetComponent<CharacterBattler>();
        characterBattler.Setup(isPlayerTeam);

        return characterBattler;
    }

    private void SetActiveCharacter(CharacterBattler character){
        if(currentCharacter != null){
            currentCharacter.HideSelectionCircle();
        }
        currentCharacter = character;
        currentCharacter.ShowSelectionCircle();
    }

    private void ChooseNextCharacter(){
        if(IsBattleOver()){
            Debug.Log("Battle Over");
            return;
        }
        if(currentCharacter == playerCharacter){
            SetActiveCharacter(enemyCharacter);
            currentCharacter.Attack(playerCharacter, () => {
                    ChooseNextCharacter();
            });
        }else{
            SetActiveCharacter(playerCharacter);
            battleState = BattleState.WaitingForInput;
        }
    }

    private bool IsBattleOver(){
        if(playerCharacter.IsDead() || enemyCharacter.IsDead()){
            return true;
        }
        return false;
    }
}
