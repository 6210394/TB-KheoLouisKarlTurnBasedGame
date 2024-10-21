using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementRemaining = 0f;
    TMPro.TextMeshProUGUI movementRemainingText;

    CharacterController characterController;
    Camera playerCamera;

    public float moveSpeed = 5f;
    public float sprintSpeedModifier = 2f;

    public float speedModifier;
    public Dictionary<string, float> speedModifierList = new Dictionary<string, float>();

    float moveX = 0f;
    float moveZ = 0f;

    void Start()
    {
        Init();
    }
    void Update()
    {
        if(movementRemaining > 0)
        {
            Move();
        }
    }

    #region Functions
        void Init()
        {
            playerCamera = Camera.main;
            characterController = GetComponent<CharacterController>();
            movementRemainingText = GameObject.Find("MovementRemaining").GetComponent<TMPro.TextMeshProUGUI>();
        }
        public void Move()
        {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");

            # region camera references for relative movement
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();
            #endregion

            //apply camera relative movement + normalize movement for diagonal consistency
            Vector3 movement = (cameraForward * moveZ + cameraRight * moveX);
            if (movement.magnitude > 1)
                movement.Normalize();

            #region Sprinting
            // Check if the player is sprinting
            if(Input.GetKey(KeyCode.LeftShift))
            {
                if(!speedModifierList.ContainsKey("Sprint"))
                {
                    AddSpeedModifier("Sprint", sprintSpeedModifier);
                }
            }
            else
            {
                if(speedModifierList.ContainsKey("Sprint"))
                {
                    RemoveSpeedModifier("Sprint");
                }
            }
            #endregion

            // Foolproofing, making sure the speed modifier is never 0 (that would make the player stuck in place)
            if(speedModifier == 0)
            {
                speedModifier = 1;
            }

            PlayerCharacterRotation(movement, moveSpeed * speedModifier);
            characterController.Move(movement * moveSpeed * speedModifier *  Time.deltaTime);
            movementRemaining -= movement.magnitude * moveSpeed * speedModifier * Time.deltaTime;

            movementRemainingText.color = Color.green;
            movementRemainingText.text = movementRemaining.ToString("F1");

            if (movementRemaining <= 0)
            {
                movementRemaining = 0;
                PlayerScript.instance.EndTurnButtonDisplay(true);
                movementRemainingText.color = Color.red;
            }
        }
        void PlayerCharacterRotation(Vector3 direction, float currentSpeed)
        {
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);
                
                //if the player is already moving or jumping, rotate the player smoothly
                if(characterController.velocity.magnitude > 0.1f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, currentSpeed * Time.deltaTime);
                }
                //else, snap the rotation
                else
                {
                    transform.rotation = toRotation;
                }
            }
        }

        #region SpeedModifier Management

        //The logic here is to only modify the movement speed of the player when the speed modifier, a combination
        //of all the speed modifiers, is changed by an event (like releasing sprint or stepping on the Fast Zone).
        //Doing this, multiple modifiers can be applied at once multiplicatively and stress is released from the Update method.

        public void AddSpeedModifier(string name, float modifier)
        {
            speedModifierList[name] = modifier;
            CalculateSpeedModifier();
        }

        public void RemoveSpeedModifier(string name)
        {
            if (speedModifierList.ContainsKey(name))
            {
                speedModifierList.Remove(name);
            }
            CalculateSpeedModifier();
        }

        public void CalculateSpeedModifier()
        {
            speedModifier = 1;
            foreach (float modifier in speedModifierList.Values)
            {
                speedModifier *= modifier;
            }
        }
        #endregion
    #endregion


}
