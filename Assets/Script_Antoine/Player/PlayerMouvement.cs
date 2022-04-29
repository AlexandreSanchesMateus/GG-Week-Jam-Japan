using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody2D rb;

    [Header("Conditions")] 
    public bool isGrounded;
    public bool hasDJ = true;

    [Header("Variable")]
    [SerializeField] private float moveSpeed;
    public float jumpForce;
    [SerializeField][Tooltip("0 = grounded")] private int gravityStrength;
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
        /*if (horizontalMouvement > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);*/

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

        if (!isGrounded)
        {
            rb.AddForce(new Vector2(0.0f, -jumpForce/100));
            Debug.Log("GET GROUNDED BITCH");
        }

        if (isGrounded)
            hasDJ = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,groundCheckRadius);
    }
}
