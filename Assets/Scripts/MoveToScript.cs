using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToScript : MonoBehaviour
{
    public Vector3 target;
    public float speed;

    public bool destroyable = false;
    public float lifeTime = 1f;

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    public void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        if (destroyable)
        {
            Destroy(gameObject, lifeTime);
        }
    }
}
