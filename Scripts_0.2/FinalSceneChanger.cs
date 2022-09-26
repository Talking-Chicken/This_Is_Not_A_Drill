using UnityEngine;
using UnityEngine.SceneManagement;
 
public class FinalSceneChanger : MonoBehaviour
{
    void OnEnable()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}