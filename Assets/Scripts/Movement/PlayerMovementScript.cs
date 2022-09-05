using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 inputDirection;
    [SerializeField]private float speed=3.0f;
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public Iinteractable Interactable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueUI.isOpen)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("E");

            if (Interactable != null && !dialogueUI.isOpen)
            {
                /*pass player ke interact jika bisa*/
                Debug.Log("Interactable detected");
                Interactable.Interact(this);
            }
            //CheckIObj();
        }

    }

    private void FixedUpdate()
    {
        Move();
        
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log(horizontal + " " + vertical);
        if (horizontal==0 && vertical==0)
        {
            rb.velocity = new Vector2(0,0);
            return;
        }
        inputDirection = new Vector2(horizontal, vertical).normalized;
        rb.velocity = inputDirection * speed;
    }

    void FlipSpite()
    {
        if (sprite.flipX && inputDirection.x < 0)
        {
            sprite.flipX = true;
        }
        else if (sprite.flipX && inputDirection.x > 0)
        {
            sprite.flipX = false;
        }
    }

    void SetSprite()
    {

    }

}
