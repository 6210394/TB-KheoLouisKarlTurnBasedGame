using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string sceneToLoad;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        foreach (CinemachineVirtualCamera camera in FindObjectsOfType<CinemachineVirtualCamera>())
        {
            ChangeCameraTarget(GameObject.Find("Player").transform, camera);
        }
    }

    public void ChangeCameraTarget(Transform target, CinemachineVirtualCamera camera)
    {
        camera.LookAt = target;
    }
    
    #region Score Management

        [SerializeField]
        public int score;
        public void AddScore(int scoreAdded)
        {
            score += scoreAdded;
        }
    #endregion

    public void ManagerLoadScene(string sceneName)
    {
        TurnManager.instance.totalTurnCount += TurnManager.instance.turnCount;
        TurnManager.instance.turnCount = 1;
        SceneManager.LoadScene(sceneToLoad);
        TurnManager.instance.Init();
        TurnManager.instance.SortListByInitiative();
    }

    public void GameOver()
    {
        ManagerLoadScene("GameOver");
    }
}
