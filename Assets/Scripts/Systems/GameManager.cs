using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
}
