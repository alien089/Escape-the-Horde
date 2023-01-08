using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    public Transform respawnPoint;

    public float speed = 50;
    public float stealthSpeed = 0f;
    private float actualSpeed = 0f;

    [SerializeField] float startDashTime = 1f;
    [SerializeField] float dashSpeed = 1f;

    float currentDashTime;

    public bool isStealth = false;
    private bool canDash = true;
    private int objective = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(rb.velocity.x) > 0.001f || Mathf.Abs(rb.velocity.y) > 0.001f)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);

        rb.velocity = (Vector2.up * Input.GetAxis("Vertical") + Vector2.right * Input.GetAxis("Horizontal")) * (actualSpeed*100) * Time.deltaTime;
        //rb.AddForce(rb.velocity);

        sprite.flipX = rb.velocity.x < 0;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isStealth = true;
            actualSpeed = stealthSpeed;
            anim.SetBool("isStealth", true);
        }
        else
        {
            actualSpeed = speed;
            isStealth = false;
            anim.SetBool("isStealth", false);
        }

        //dash
        if (canDash && Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(Dash(Vector2.up));
            }

            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(Vector2.left));
            }

            else if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(Dash(Vector2.down));
            }

            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(Vector2.right));
            }
        }
    }

    IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        currentDashTime = startDashTime; // Reset the dash timer.

        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime; // Lower the dash timer each frame.

            rb.velocity = direction * dashSpeed; // Dash in the direction that was held down.
                                                 // No need to multiply by Time.DeltaTime here, physics are already consistent across different FPS.

            yield return null; // Returns out of the coroutine this frame so we don't hit an infinite loop.
        }

        rb.velocity = new Vector2(0f, 0f); // Stop dashing.

        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IEnemy>().RespawnPlayer(respawnPoint, gameObject);
        }

        /*
        if (collision.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemy.RespawnPlayer(respawnPoint, gameObject);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("objective"))
        {
            objective++;
            Destroy(collision.gameObject);
        }
    }
}
