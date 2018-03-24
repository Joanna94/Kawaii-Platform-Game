using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public float moveSpeed = 3f;
    public float xMax = 5;
    public float xMin = 0;
    
    private float startPositionX;
    private bool isMovingRight = true;


    // Use this for initialization
    void Start () {
        setStartXPosition();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void setStartXPosition()
    {
        startPositionX = currentXPosition();
    }

    void Move()
    {

        if (IsMovingInRange())
        {
            if (isMovingRight)
                MoveRight();
            else
                MoveLeft();
        }
        else
            changeDirectionOfMoving();
        
    }

    float currentXPosition()
    {
        return this.transform.position.x;
    }

    bool IsMovingInRange()
    {
        if (isMovingRight && (currentXPosition() < startPositionX + xMax))
            return true;
        else if (!isMovingRight && (currentXPosition() > startPositionX - xMin))
            return true;

        return false;
    }

    void changeDirectionOfMoving()
    {
        if (isMovingRight)
        {
            isMovingRight = false;
            MoveLeft();
        }
        else
        {
            isMovingRight = true;
            MoveRight();
        }
    }

    void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    void MoveLeft()
    {
        transform.Translate((-moveSpeed) * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }
}
