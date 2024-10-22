using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public string sceneToLoad;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.ManagerLoadScene(sceneToLoad);
        }
    }
}
