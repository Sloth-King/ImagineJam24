using UnityEngine;

public class CharacterBattler : MonoBehaviour
{
    private BaseCharacter _characterBase;
    private void Awake()
    {
        _characterBase = GetComponent<BaseCharacter>();
    }

    public void Setup(){

    }
}
