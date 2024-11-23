using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void Start()
    {
        SpawnCharacter(true);
        SpawnCharacter(false);
    }

    private void SpawnCharacter(bool isPlayerTeam){
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        if(isPlayerTeam){
            spawnPosition = new Vector3(-45, 0, 0);
        }else{
            spawnPosition = new Vector3(45, 0, 0);
        }
        Instantiate(_player, spawnPosition, Quaternion.identity);
    }
}
