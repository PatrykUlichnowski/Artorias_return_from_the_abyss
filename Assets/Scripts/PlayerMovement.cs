using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D body;
    //SerializeField is used so that you can operate on the variable from Unity 
    [SerializeField] private float speed;
    private Animator anim;
    private bool grounded = true;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //left and right movement
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //jumping
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        // fliping character sprite left and right as player moves
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        } 
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //setting up animations
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    } 
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("Jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
