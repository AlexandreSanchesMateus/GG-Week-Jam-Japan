using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    [Header("Component")]
    private Rigidbody2D rb;


    [Header("Conditions")] 
    [SerializeField] private bool isGrounded;
    public bool allowDJ;
    [SerializeField] private bool hasDJ = true;

    [Header("Variable")]
    [SerializeField] private float moveSpeed;
    public float jumpForce;
    private float horizontalMouvement;
    private Vector3 velocity;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] LayerMask collisionLayers;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
        //Fonction for Moving
        horizontalMouvement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        MovePlayer(horizontalMouvement);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
    }

    void MovePlayer(float _horizontalMouvement)
    {
        //Add the mouvement on X and keep the same on Y
        Vector3 targetVelocity = new Vector2(_horizontalMouvement, rb.velocity.y);
        //Move the player
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && hasDJ && allowDJ)
        {
            rb.velocity = velocity;
            rb.AddForce(new Vector2(0.0f, jumpForce * 2));
            hasDJ = false;
        }

        if (!isGrounded)
        {
            rb.AddForce(new Vector2(0.0f, -jumpForce/100));
        }

        if (isGrounded)
        {
            hasDJ = true;
            Debug.Log("GET GROUNDED BITCH");
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,groundCheckRadius);
    }
}
