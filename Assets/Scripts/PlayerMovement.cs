using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D body;
    //SerializeField is used so that you can operate on the variable from Unity 
    [SerializeField] private float speed;
    private Animator animation;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //left and right movement
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //jumping
        if (Input.GetKey(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, speed);
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
        animation.SetBool("Run", horizontalInput != 0);
        //test
    }
    // Start is called before the first frame update
    void Start()
    {
        
    } 
}
