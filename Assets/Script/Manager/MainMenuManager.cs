using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    [SerializeField] private GameObject mainMenuPanel;


    private void Start()
    {
        SetUpMainMenu();
    }

    public void SetUpMainMenu()
    {
        UIManager.instance.OpenPanel(mainMenuPanel);
    }

    public void PlayGame()
    {
        UIManager.instance.ClosePanel(mainMenuPanel);

        StateManager.instance.SwitchState(GameState.GameplayState);
    }
    public void ExitGame()
    {
        SceneController.instance.ExitGame();
    }
}
