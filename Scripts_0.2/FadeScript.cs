using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeToNextScene();
        }
    }

    public void FadeToNextScene()
    {
        FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToScene (int levelIndex)
    {
        sceneToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
