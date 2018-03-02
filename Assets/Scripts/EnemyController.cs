using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float moveSpeed = 3f;
    public float startPositionX;
    public float xMax = 5f;
    public float xMin = 5f;
    public Animator animator;

    private bool isFacingRight = true;
    private bool isMovingRight = true;
    private float killOffset = 0.7f;

    // Use this for initialization
    void Start () {
        startPositionX = this.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {

        if (!animator.GetBool("isDead"))
        {
            if (isMovingRight)
            {
                if (this.transform.position.x < startPositionX + xMax)
                    MoveRight();
                else
                {
                    isMovingRight = false;
                    MoveLeft();
                    Flip();
                }
            }
            else
            {
                if (this.transform.position.x > startPositionX - xMin)
                    MoveLeft();
                else
                {
                    isMovingRight = true;
                    MoveRight();
                    Flip();
                }
            }
        }
	}

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * (-1);
        transform.localScale = theScale;

        isFacingRight = !isFacingRight;
    }

    void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    void MoveLeft()
    {
        transform.Translate((-moveSpeed) * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.transform.position.y > this.transform.position.y + killOffset)
            {
                animator.SetBool("isDead", true);
                StartCoroutine(KillOnAnimationEnd());
                Debug.Log("Enemy killed");
            }
        }
    }

    IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.6f);
        this.gameObject.SetActive(false);
    }
}
