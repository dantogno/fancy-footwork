using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour,IInteract
{
    public bool lookingAt { get; set; }
    public bool triggered { get; set; }
    public InteractionEnum interationType { get; set; }

    public InteractionEnum interactionType;

    private Renderer render;
    private Color originalColor;

    public bool canBeTriggered = true;

    // Start is called before the first frame update
    void Start()
    {
        lookingAt = false;
        triggered = false;
        if (interationType == InteractionEnum.pickable
            || interationType == InteractionEnum.door)
        {
            render = GetComponent<Renderer>();
            originalColor = render.material.GetColor("_EmissionColor");
            render.material.SetColor("_EmissionColor", Color.black);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //switch for different types
        switch(interationType)
        {
            case InteractionEnum.pickable:
                HighLight();
                break;
            case InteractionEnum.trigger:
                if (lookingAt)
                    triggerAction();
                else
                {
                    if (triggered)
                        triggered = false;
                }
                break;
            case InteractionEnum.door:
                HighLight();
                break;
        }
    }
    //set trigger to true if looking at and a button is clicked
    public void triggerAction()
    {
        if (interationType == InteractionEnum.pickable
           || interationType == InteractionEnum.door)
        {
            float clicked = Input.GetAxis("Fire2");
            if(clicked>0)
            {
                if (!triggered)
                    triggered = true;
            }
            else if(clicked==0)
            {
                triggered = false;
            }
        }
        else
        {
            triggered=true;
        }
    }

    //outline an object
    public void HighLight()
    {
        //if the player is looking at an object then set the color to the original color
        if(lookingAt)
        {
            //if it hasn't already been set, then set back to preset color
            if (render.material.GetColor("_EmissionColor") != originalColor)
                render.material.SetColor("_EmissionColor", originalColor);
            //call function
            triggerAction();
        }
        else
        {
            //take away outline
            if(render.material.GetColor("_EmissionColor") != Color.black)
                render.material.SetColor("_EmissionColor", Color.black);
            //set trigger back to false if true
            if (triggered)
                triggered = false;
        }
    }
}
