using UnityEngine;

public class StartScript : MonoBehaviour
{
    public string sceneName = "GameSceneNameHere";

    [SerializeField]
    GameObject gameManagerObject;

    void Start()
    {
        CreateGameManager();
        SetCursorState(true, CursorLockMode.None);
    }

    public void LoadScene()
    {
        Debug.Log("Loading Scene: " + sceneName);

        GameManager.instance.ManagerLoadScene(sceneName);
        if(sceneName == "Level1")
        {
            GameManager.instance.score = 0;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreateGameManager()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManagerObject);
        }
    }

    public void SetCursorState(bool visible, CursorLockMode lockMode)
    {
        Cursor.visible = visible;
        Cursor.lockState = lockMode;
    }
}
