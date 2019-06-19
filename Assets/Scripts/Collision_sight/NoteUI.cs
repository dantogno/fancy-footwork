using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteUI : MonoBehaviour
{
    Interactable interact;
    public Image UI;
    private bool clickedButton=false;
    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<Interactable>();
        UI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(interact.lookingAt && !clickedButton)
        {
            UI.enabled = true;
        }
        else if(!interact.lookingAt && !clickedButton)
        {
            UI.enabled = false;
        }
        if(interact.triggered)
        {
            UI.enabled = false;
            clickedButton = true;
        }
    }
}
