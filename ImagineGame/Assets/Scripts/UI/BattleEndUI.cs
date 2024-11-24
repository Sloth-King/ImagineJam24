using UnityEngine;

public class BattleEndUI : MonoBehaviour
{
    public GameObject victoryScreen;
    public GameObject defeatScreen;

    public void ShowVictoryScreen(){
        victoryScreen.SetActive(true);
    }
    public void ShowDefeatScreen(){
        defeatScreen.SetActive(true);
    }
    public void HideVictoryScreen(){
        victoryScreen.SetActive(false);
    }
    public void HideDefeatScreen(){
        defeatScreen.SetActive(false);
    }

    public void Hide(){
        HideVictoryScreen();
        HideDefeatScreen();
    }

    public void RestartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
    }

    public void backToWorldScene(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("WorldScene");
    }
}
