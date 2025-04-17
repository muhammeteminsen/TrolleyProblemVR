using UnityEngine;

public class UIManagement : MonoBehaviour
{
    public void LoadChapter(int chapterIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(chapterIndex);
    }
}
