using UnityEngine;

[CreateAssetMenu(fileName = "CharactersData", menuName = "CharactersData")]
public class CharactersData : ScriptableObject
{
    public string characterName;
    public int health;
    public int damage;
    public int speed;
    public CharacterType characterType;
    public Sprite characterSprite;
    public GameObject characterPrefab;

    public enum CharacterType
    {
        IceMage,
        Gambler,
        PatientZero,
        CoinFlipper,
    }
}
