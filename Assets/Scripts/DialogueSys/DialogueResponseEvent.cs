using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueResponseEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private DialogueObj dialogueObj;
    [SerializeField] private ResposeEvent[] events;
    DialogueActivator dialogueActivator;
    public DialogueObj DialogueObj => dialogueObj;

    public ResposeEvent[] Events => events;

    private void Start()
    {
        dialogueActivator = this.GetComponent<DialogueActivator>();
        //dialogueObj = dialogueActivator.nPC.npcDialogue;
    }

    public void OnValidate()
    {
        if (dialogueObj==null)
        {
            return;
        }
        if (dialogueObj.Responses==null)
        {
            return;
        }
        if (events!=null&&events.Length==dialogueObj.Responses.Length)
        {
            return;
        }

        if (events==null)
        {
            events = new ResposeEvent[dialogueObj.Responses.Length];
        }
        else
        {
            Array.Resize(ref events, dialogueObj.Responses.Length);
        }

        for (int i = 0; i < dialogueObj.Responses.Length; i++)
        {
            Response response = dialogueObj.Responses[i];
            if (events[i]!=null)
            {
                events[i].name = response.Responsetext;
                continue;
            }
            events[i] = new ResposeEvent() { name = response.Responsetext };
        }
    }
}
