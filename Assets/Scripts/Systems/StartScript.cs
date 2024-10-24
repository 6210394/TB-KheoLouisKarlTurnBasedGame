using UnityEngine;

public class StartScript : MonoBehaviour
{
    public string sceneName = "GameSceneNameHere";

    [SerializeField]
    GameObject gameManagerObject;

    void Start()
    {
        CreateGameManager();
    }

    public void LoadScene()
    {
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
            GameObject gameManagerObject = new GameObject("GameManager");
            gameManagerObject.AddComponent<GameManager>();
        }
    }
}
