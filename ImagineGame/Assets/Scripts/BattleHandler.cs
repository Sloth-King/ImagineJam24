using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private BattleEndUI battleEndUI;
    [SerializeField] private GameObject _player;
    private GameObject battleElements;

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
        battleEndUI.Hide();
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

    private CharacterBattler SpawnCharacter(bool isPlayerTeam)
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        if (isPlayerTeam)
        {
            spawnPosition = new Vector3(-4.2f, 0, 0);
        }
        else
        {
            spawnPosition = new Vector3(4.2f, 0, 0);
        }

        battleElements = GameObject.Find("BattleElements");
        if (battleElements == null)
        {
            Debug.LogError("BattleElements GameObject not found in the scene.");
            return null;
        }

        GameObject character = Instantiate(_player, spawnPosition, Quaternion.identity, battleElements.transform);
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
        if(playerCharacter.IsDead()){
            Debug.Log("Lost Battle");
            battleEndUI.ShowDefeatScreen();
            battleElements.SetActive(false);
            return true;
        }
        if(enemyCharacter.IsDead()){
            battleEndUI.ShowVictoryScreen();
            battleElements.SetActive(false);
            
            Debug.Log("Won Battle");
            return true;
        }
        return false;
    }
}
