using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel1 : MonoBehaviour {

    public float moveSpeed = 0.1f;
    public float jumpForce = 6f;
    public LayerMask groundLayer;
    public Animator animator;
    public bool isWalking = false;

    public AudioClip enemyKillSound;
    public AudioClip deathSound;
    public AudioClip itemSound;
    public AudioClip winSound;

    private AudioSource source;
    private Rigidbody2D rigidBody;
    private bool isFacingRight = true;
    private float killOffset = 0.5f;
    private Vector2 startPosition;
    private float startFallingPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        isWalking = false;

        if (GameManager.instance.currentGameState == GameState.GS_GAME)
        {

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                MoveRight();
                isWalking = true;

                if (!isFacingRight)
                    Flip();

            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                MoveLeft();
                isWalking = true;

                if (isFacingRight)
                    Flip();
            }

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        if(IsGrounded())
            startFallingPosition = this.transform.position.y;

        detectFalling();

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", IsGrounded());
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;

        source = GetComponent<AudioSource>();
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, 1.5f, groundLayer.value);
    }

    void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    void MoveLeft()
    {
        transform.Translate((-moveSpeed) * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    void Jump()
    {
        if (IsGrounded())
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void detectFalling()
    {
        if (!IsGrounded())
        {
            if (this.transform.position.y < startFallingPosition - 20)
            {
                this.transform.position = startPosition;
                GameManager.instance.SubLives();
                source.PlayOneShot(deathSound, 1f);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            GameManager.instance.AddKeys();
            source.PlayOneShot(itemSound, 1f);
        }
        else if (other.CompareTag("Carrot"))
        {
            GameManager.instance.AddLives();
            source.PlayOneShot(itemSound, 1f);
        }
        else if (other.CompareTag("Finish"))
        {
            if (GameManager.instance.keys == 3)
            {
                GameManager.instance.LevelCompleted();
                source.PlayOneShot(winSound, 1f);
            }
        }
        else if (other.CompareTag("BronzeCoin"))
        {
            GameManager.instance.AddBronzeCoins();
            GameManager.instance.IncreaseTotalScore((int)Points.P_BRONZECOINS);
            source.PlayOneShot(itemSound, 1f);
        }
        else if (other.CompareTag("SilverCoin"))
        {
            GameManager.instance.AddSilverCoins();
            GameManager.instance.IncreaseTotalScore((int)Points.P_SILVERCOINS);
            source.PlayOneShot(itemSound, 1f);
        }
        else if (other.CompareTag("GoldCoin"))
        {
            GameManager.instance.AddGoldCoins();
            GameManager.instance.IncreaseTotalScore((int)Points.P_GOLDCOINS);
            source.PlayOneShot(itemSound, 1f);
        }
        else if (other.CompareTag("SpikeMan"))
        {
            if (this.transform.position.y < other.transform.position.y + killOffset)
            {
                this.transform.position = startPosition;
                GameManager.instance.SubLives();
                source.PlayOneShot(deathSound, 1f);
            }
            else
            {
                GameManager.instance.IncreaseTotalScore((int)Points.P_SPIKEMAN);
                source.PlayOneShot(enemyKillSound, 1f);
            }
        }

        if (!other.CompareTag("SpikeMan"))
            other.gameObject.SetActive(false);

        if (other.CompareTag("Finish"))
            other.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.parent = collision.transform;
        }     
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.parent = null;
        }   
    }
}
