using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public bool controlledHorizontally = true;
    public bool controlledVertically = true;

    public float moveSpeed = 5f;
    public float sprintSpeedModifier = 2f;

    public float speedModifier;
    public Dictionary<string, float> speedModifierList = new Dictionary<string, float>();



    public float moveX = 0f;
    public float moveY = 0f;


    public Animator animator;
    bool isAnimated = false;

    public enum Direction { None, Up, Down, Left, Right }
    public Direction currentDirection = Direction.None;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            isAnimated = true;
        }
    }

    void Update()
    {
        // Get input if the entity is allowed to move in that direction
        if (controlledHorizontally)
        {
            moveX = Input.GetAxis("Horizontal");
        }
        if (controlledVertically)
        {
            moveY = Input.GetAxis("Vertical");
        }

        //Check the direction of the player for the animator and set it
        if (isAnimated)
        {
            GetDirection();
            SetDirectionOfAnimation(currentDirection);
        }

        // Create a vector3 with the input values, normalies to prevent faster diagonal movement
        Vector3 movement = new Vector3(moveX, moveY, 0f);
        if (movement.magnitude > 1)
            movement.Normalize();


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

        // Foolproofing
        if(speedModifier == 0)
        {
            speedModifier = 1;
        }

        // Move the entity
        movement *= speedModifier;
        transform.position = Vector3.Lerp(transform.position, transform.position + movement, moveSpeed * Time.deltaTime);
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


    void GetDirection()
    {
        if (moveX != 0 || moveY != 0)
        {
            animator.SetBool("isMoving", true);
            if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
            {
                currentDirection = moveX > 0 ? Direction.Right : Direction.Left;
            }
            else
            {
                currentDirection = moveY > 0 ? Direction.Up : Direction.Down;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
            currentDirection = Direction.None;
        }
    }

    void SetDirectionOfAnimation(Direction currentDirection)
    {
        if(currentDirection == Direction.Up)
        {
            animator.SetBool("isUp", true);
        }
        else
        {
            animator.SetBool("isUp", false);
        }

        if(currentDirection == Direction.Down)
        {
            animator.SetBool("isDown", true);
        }
        else
        {
            animator.SetBool("isDown", false);
        }

        if(currentDirection == Direction.Left)
        {
            animator.SetBool("isLeft", true);
        }
        else
        {
            animator.SetBool("isLeft", false);
        }

        if(currentDirection == Direction.Right)
        {
            animator.SetBool("isRight", true);
        }
        else
        {
            animator.SetBool("isRight", false);
        }
    }

       
}
