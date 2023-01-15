using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float groundLevel;

    private Vector2 initialPosition;
    private Rigidbody2D rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < groundLevel)
        {
            transform.position = initialPosition;
        }

        if (Input.GetButton("Horizontal"))
        {
            Move();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        Vector3 movement = new(speed * inputX, transform.position.y);
        movement *= Time.deltaTime;
        transform.Translate(movement);
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
