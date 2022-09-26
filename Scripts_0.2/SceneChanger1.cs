using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneChanger1 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
            SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}