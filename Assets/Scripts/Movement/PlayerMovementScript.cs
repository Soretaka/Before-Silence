using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour, ISaveable
{

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 inputDirection;
    [SerializeField]private float speed=3.0f;
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public Iinteractable Interactable { get; set; }

    public string m_UniqueID="player";
    string ISaveable.UniqueID { get { return m_UniqueID; } set { m_UniqueID = value; } }

    
    public Dictionary<string, object> OnSave()
    {

        //isDoorOpen = (bool)data[nameof(isDoorOpen)];
        //isLocked = (bool)data[nameof(isLocked)];
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add(nameof(gameObject.transform.position.x), gameObject.transform.position.x);
        data.Add(nameof(gameObject.transform.position.y), gameObject.transform.position.y);
        return data;
    }

    public void OnLoad(Dictionary<string, object> data)
    {
        gameObject.transform.position =new Vector2( (float)data[nameof(gameObject.transform.position.x)], (float)data[nameof(gameObject.transform.position.y)]);

    }



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
