using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public bool mouseMode = true;

    //distance of the camera from the player
    public float distance = 10.0f;
    public float minDistance = 5.0f;
    public float maxDistance = 20.0f;

    //mouse sensitivity
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float zoomRate = 20.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    public float heightOffset = 0.0f;


    void Start ()
    {
        GameManager.instance.onPlayerSpawned += GetNewPlayerReference;

        GetNewPlayerReference();
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    public void GetNewPlayerReference()
    {
        target = GameManager.instance.playerReference.transform;
    }

    void Update ()
    {
        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomRate;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        GetMouseInput();

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        position.y += heightOffset;

        CheckForWallInFrontOfCam(position, rotation);
        
        transform.rotation = rotation;
        transform.position = position;
    }

    void GetMouseInput()
    {
        if(Input.GetMouseButton(1))
        {
            if (target) //if camera has a target, which in our case is the player, script continues
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseMode = false;
                //get the mouse movement
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                //notice how y is subtracted, this is because the y axis is inverted
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseMode = true;
        }
    }

    void CheckForWallInFrontOfCam(Vector3 position, Quaternion rotation)
    {
        RaycastHit hit;
        if (Physics.Raycast(target.position, (position - target.position).normalized, out hit, distance))
        {
                // If a wall is detected and it's tagged as "Environment", move the camera closer to the player
            if (hit.collider.gameObject.CompareTag("Environment"))
            {
                position = rotation * new Vector3(0.0f, 0.0f, -hit.distance) + new Vector3(target.position.x, target.position.y + heightOffset, target.position.z);
                distance = hit.distance;
            }
        }
    }

    
}