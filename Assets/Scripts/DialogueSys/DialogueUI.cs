using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DialogueUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject dialoguebox;
    //container npc art
    [SerializeField] private GameObject npcArtbox;
    [SerializeField] private GameObject panelBG;

    /*[SerializeField] private ActionLimit_Script actionLimit_Script;
    [SerializeField] private StageEnd_Script stageEnd_Script;*/

    [SerializeField]private TMP_Text textlabel;
    [SerializeField]private Button nextButton;

    [SerializeField]private Button[] toggleButtons;
    /*buat bool untuk mengontrol terbuka/tidak
     dapat dibaca darimana aja(get), hanya bisa diset/ganti sendiri(privates set)*/
    public bool isOpen { get; private set; }

    private TypeEff typeEff;
    private Responsehandler responseHandler;

    private void Start()
    {
        typeEff = GetComponent<TypeEff>();
        responseHandler = GetComponent<Responsehandler>();
        CloseDialogue();
        panelBG.SetActive(false);
    }



    public void showDialogue(DialogueObj dialogueobj, Sprite sprite)
    {
        for (int i = 0; i < toggleButtons.Length; i++)
        {
            toggleButtons[i].interactable = false;
        }
        isOpen = true;
        panelBG.SetActive(true);
        dialoguebox.SetActive(true);
        npcArtbox.SetActive(true);
        npcArtbox.GetComponent<Image>().sprite = sprite;
        //Debug.Log(dialogueobj.name + "wtf is sthsi");
        StartCoroutine(StepTroughDialogue(dialogueobj));
    }

    public void AddResponseEvents(ResposeEvent[] responseEvent)
    {
        responseHandler.AddResponseEvent(responseEvent);
    }

    private IEnumerator StepTroughDialogue(DialogueObj dialogueobj)
    {   //show face
        for (int i = 0; i < dialogueobj.Dialogue.Length; i++)
        {
            string dialogue = dialogueobj.Dialogue[i];
            //Debug.Log("aku diapanggil" + i);
            yield return RunTypeEffect(dialogue);
            textlabel.text = dialogue;
            //Debug.Log(textlabel);

            /*Cek jika 
             * sudah sampai akhir dialogueobj
             * responsenya masih ada
             * kalau sudah samapi ujung dan ada responses, pindah ke vlok kode if cek response
             */
            if (i==dialogueobj.Dialogue.Length-1 &&dialogueobj.hasresponses)
            {
                break;
            }
            yield return null;
            yield return new WaitForUIButtons(nextButton);

            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space))  ;
        }
        /*blok kode yang cek responses*/
        if (dialogueobj.hasresponses)
        {
            responseHandler.ShowResponses(dialogueobj.Responses);
        }
        else
        {
            if (dialogueobj.isEnd)
            {
                Debug.Log("play dead");
                //stageEnd_Script.EndDay();
                //play dead
            }
            CloseDialogue();
        }

    }



    private IEnumerator RunTypeEffect(string dialogue)
    {
        var waitForButton = new WaitForUIButtons(nextButton);
        //Debug.Log(textlabel);
        typeEff.Run(dialogue, textlabel);
        while (typeEff.isRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.X)||waitForButton.PressedButton==nextButton)
            {
                typeEff.stop();
            }
        }
    }

    public void CloseDialogue()
    {
        isOpen = false;
        dialoguebox.SetActive(false);
        npcArtbox.SetActive(false);
        panelBG.SetActive(false);
        //actionLimit_Script.act = actionLimit_Script.maxact;
        textlabel.text = string.Empty;
        for (int i = 0; i < toggleButtons.Length; i++)
        {
            toggleButtons[i].interactable = true;
        }
    }
}
