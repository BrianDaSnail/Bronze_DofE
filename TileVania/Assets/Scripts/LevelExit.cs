using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float waitTime = 1f;

    private void OnTriggerEnter2D(Collider2D other) //When player touches exit, load next scene.
    {
        {
            if(other.tag == "Player")
            {
                StartCoroutine(LoadNextScene()); 
            }
        }
    }


    IEnumerator LoadNextScene() //Adds a delay when loading next scene.
    {
        
        yield return new WaitForSecondsRealtime(waitTime);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<GameSession>().AddPlayerLife();
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
        
    }
}
