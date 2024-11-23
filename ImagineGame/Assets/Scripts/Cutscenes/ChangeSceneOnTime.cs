using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTime : MonoBehaviour
{
    public float changeTime;
    public string sceneName;

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
