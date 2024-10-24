using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void OnPlayerSpawned();
    public event OnPlayerSpawned onPlayerSpawned;


    public static GameManager instance;
    public string sceneToLoad;
    public Transform playerSpawnPoint;

    [HideInInspector]
    public GameObject playerReference;
    [HideInInspector]
    public GameObject cameraReference;

    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    GameObject playerCamera;

    public int lives = 3;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoseLife();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ManagerLoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            KillAllEnemies();
        }
    }

    void Start()
    {
        Init();
        SceneManager.sceneLoaded +=  OnSceneLoaded;
    }

    void Init()
    {
        Debug.Log("Initializing GameManager");
        sceneToLoad = SceneManager.GetActiveScene().name;

        if(SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "GameOver")
        {
            GetComponentInChildren<Canvas>().enabled = false;
            return;
        }
        else
        {
            GetComponentInChildren<Canvas>().enabled = true;
        }

        SpawnPlayer();
        
        //this would be relevant in the case I had a camera for each enemy and it would swtich between them on their turn
        foreach (CinemachineVirtualCamera camera in FindObjectsOfType<CinemachineVirtualCamera>())
        {
            ChangeCameraTarget(playerReference.transform, camera);
        }

        TurnManager.instance.Init();
    }

    public void SpawnPlayer()
    {
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
        if(cameraReference == null)
        {
            CreatePlayerCamera();
        }
        playerReference = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        onPlayerSpawned?.Invoke();
        return;
    }

    public void CreatePlayerCamera()
    {
        cameraReference = Instantiate(playerCamera, playerSpawnPoint.position, playerSpawnPoint.rotation);
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
        SceneManager.LoadScene(sceneName);
    }

    public void ManagerLoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name + mode);
        Init();
    }

    public void LoseLife()
    {
        lives -= 1;

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            ManagerLoadScene();
        }

    }

    public void GameOver()
    {
        ManagerLoadScene("GameOver");
    }

    public void KillAllEnemies()
    {
        foreach (EnemyEntityScript enemy in FindObjectsOfType<EnemyEntityScript>())
        {
            enemy.Die();
        }
    }
}
