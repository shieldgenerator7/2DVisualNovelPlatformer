using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AlienHP))]
public class OnDamagedPlayDialogue : MonoBehaviour
{
    public string dialogueTitle;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AlienHP>().onDamaged -= damaged;
        GetComponent<AlienHP>().onDamaged += damaged;
    }

    private void damaged()
    {
        //Find dialogue path by its title
        if (dialogueTitle != "" && dialogueTitle != null)
        {
            FindObjectOfType<DialogueManager>().playDialogue(dialogueTitle);
        }
    }
}
