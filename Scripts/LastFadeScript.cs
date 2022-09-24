using UnityEngine;
using UnityEngine.SceneManagement;

public class LastFadeScript : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeToScene(0);
        }
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
