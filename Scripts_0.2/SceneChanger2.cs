using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
 
public class SceneChanger2 : MonoBehaviour
{
    public PlayableDirector director;
    void OnEnable()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        // if (director.stopped)
        //     SceneManager.LoadScene(2, LoadSceneMode.Single);
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector) {
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            SceneManager.LoadScene(2);
        }
    }
}