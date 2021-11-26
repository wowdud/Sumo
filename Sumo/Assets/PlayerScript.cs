using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    //Movement
    public float speed;
    public float jump;
    float moveVelocity;
    public Rigidbody2D player;
    //public SpriteRenderer playerModel;

    public Mesh left;
    public Mesh right;
    public Mesh main;

    public static bool isPlayerFlipped = false;
    public static Vector2 playerPos;


    //Grounded Vars
    bool grounded = true;
    bool isFalling = false;

    //Attack variables
    public float attackDelay = 0.75f;
    public GameObject attackProjectile;
    public GameObject melee;

    //Going to the next level
    public int waitTime;
    public Text winText;


    void Update()
    {
        //Attacking
        attackDelay -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && attackDelay <= 0)
        {
            Instantiate(attackProjectile, transform.position, transform.rotation);
            attackDelay = 0.75f;
        }
        if (Input.GetButtonDown("Fire2") && attackDelay <= 0)
        {
            Instantiate(melee, transform.position, transform.rotation);
            attackDelay = 0.75f;
        }

        //Jumping
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
        {
            if (grounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            }
        }
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.W)) && isFalling == false)
        {
            float moveDirection = Input.GetAxisRaw("Vertical");
            player.velocity = new Vector2(0, -moveDirection * speed);
            isFalling = true;
        }

        moveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveVelocity = -speed;
            //playerModel.flip = true;
            GetComponent<MeshFilter>().sharedMesh = left;
            isPlayerFlipped = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveVelocity = speed;
            //playerModel.flipX = false;
            GetComponent<MeshFilter>().sharedMesh = right;
            isPlayerFlipped = true;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        //Misc

        playerPos = player.transform.position;

    }
    //Check if Grounded
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            grounded = true;
            isFalling = false;
        }
        if (collider.CompareTag("Enemy"))
        {
            grounded = true;
            isFalling = false;
        }
        if (collider.CompareTag("Finish"))
        {
            Invoke("NextLevel", waitTime);
            winText.gameObject.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground") || collider.CompareTag("Enemy"))
        {
            grounded = false;
        }
    }
    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}