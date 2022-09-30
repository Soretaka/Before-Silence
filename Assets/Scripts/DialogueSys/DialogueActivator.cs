using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script untuk handle aktivasi dialog*/
public class DialogueActivator : MonoBehaviour, Iinteractable
{
    public NPC nPC;
    /*private SpriteRenderer npcSr;
    private SpriteRenderer bgSr;*/
    private DialogueObj dialogueObj { get; set; }

    public GameObject background;
    private void Start()
    {
/*        bgSr = background.GetComponent<SpriteRenderer>();
        npcSr = GetComponent<SpriteRenderer>();*/        
        dialogueObj = nPC.npcDialogue;
        Debug.Log(nPC.name);
        Debug.Log(dialogueObj);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovementScript player))
        {
            Debug.Log("collided!!");
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovementScript player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }

        }
    }

    public void UpdateDialogueObject(NPC nPC)
    {
        dialogueObj = nPC.npcDialogue;
        dialogueObj.Dialoguepicleft = nPC.npcDialogue.Dialoguepicleft;
        //sDebug.Log(dialogueObj, dialogueObj.Dialoguepic);
    }

    //function bdy dari interface
    public void Interact(PlayerMovementScript player)
    {
        //Debug.Log("Interacted"+dialogueObj);

        if (TryGetComponent(out DialogueResponseEvent responseEvent) && responseEvent.DialogueObj == dialogueObj)
        {
            player.DialogueUI.AddResponseEvents(responseEvent.Events);
        }

        player.DialogueUI.showDialogue(dialogueObj, dialogueObj.Dialoguepicleft, dialogueObj.Dialoguepicright);
    }
}
