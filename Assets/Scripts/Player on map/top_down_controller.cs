using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class top_down_controller : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer SpriteRenderer;
    public List<Sprite> upSprites;
    public List<Sprite> downSprites;
    public List<Sprite> leftSprites;
    public List<Sprite> rightSprites;
    
    public float walkSpeed;
    public float frameRate;
    float idleTime;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //dir input
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //set walk
        body.velocity = direction * walkSpeed;
        //handle direction

        //if facing right, player hold left, flip
        //if facing left, player hold right, flip
        // HandleSpriteFlip();

        //handle animation
        List<Sprite> directionSprites=GetSpriteDirection();
        if(directionSprites != null){
            float playTime = Time.time - idleTime;
            int frame =(int)((playTime * frameRate) % directionSprites.Count);
            SpriteRenderer.sprite = directionSprites[frame];
        }else{
            idleTime = Time.time;
        }
    }
    // void HandleSpriteFlip(){
    //     if(!SpriteRenderer.flipX && direction.x < 0){
    //         SpriteRenderer.flipX = true;
    //     }else if (SpriteRenderer.flipX && direction.x > 0){
    //         SpriteRenderer.flipX = false;
    //     }
    // }

    List<Sprite> GetSpriteDirection(){

        List<Sprite> selectedSprites=null;

        if(direction.y>0){
            selectedSprites = upSprites;
        }else if (direction.y < 0){
            selectedSprites = downSprites;
        }else{
            if(direction.x > 0){
                selectedSprites = rightSprites;
            }else if(direction.x < 0){
                selectedSprites = leftSprites;
            }
        }
        return selectedSprites;
    }
}


