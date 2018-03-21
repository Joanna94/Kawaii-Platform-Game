using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public float moveSpeed = 3f;
    public float xMax = 5;
    public float yMax = 0;
    public bool isMovingHorizontal = true;
    
    private float startPositionX;
    private float startPositionY;
    private bool isMovingUp = true;
    private bool isMovingRight = true;


    // Use this for initialization
    void Start () {
        startPositionX = this.transform.position.x;
        startPositionY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

        if (isMovingHorizontal) {
            if (isMovingRight)
            {
                if (this.transform.position.x < startPositionX + xMax)
                    MoveRight();
                else
                {
                    isMovingRight = false;
                    MoveLeft();
                }
            }
            else
            {
                if (this.transform.position.x > startPositionX)
                    MoveLeft();
                else
                {
                    isMovingRight = true;
                    MoveRight();
                }
            }
        }
        else //is moving vertical
        {
            if (isMovingUp)
            {
                if (transform.position.y < startPositionY + yMax)
                    MoveUp();
                else
                {
                    isMovingUp = false;
                    MoveDown();
                }
            }
            else
            {
                if (transform.position.y > startPositionY)
                    MoveDown();
                else
                {
                    isMovingUp = true;
                    MoveUp();
                }
            }
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

    void MoveUp()
    {
        transform.Translate(0.0f, moveSpeed * Time.deltaTime, 0.0f, Space.World);
    }

    void MoveDown()
    {
        transform.Translate(0.0f, (-moveSpeed) * Time.deltaTime, 0.0f, Space.World);
    }

}
