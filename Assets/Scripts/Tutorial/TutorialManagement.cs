using UnityEngine;

public class TutorialManagement : MonoBehaviour
{
    private ITutorial _tutorial;

    private void Awake()
    {
        _tutorial = GetComponent<ITutorial>();
    }

    public void StartTutorial()
    {
        _tutorial?.EnterTutorial();
    }
    public void ExitTutorial()
    {
        _tutorial?.ExitTutorial();
    }
}