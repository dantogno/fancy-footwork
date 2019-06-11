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

    // Start is called before the first frame update
    void Start()
    {
        lookingAt = false;
        triggered = false;
        if (interationType == InteractionEnum.pickable
            || interationType == InteractionEnum._switch)
        {
            render = GetComponent<Renderer>();
            originalColor = render.material.GetColor("_EmissionColor");
            render.material.SetColor("_EmissionColor", Color.black);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(interationType)
        {
            case InteractionEnum.pickable:
                HighLight();
                break;
            case InteractionEnum.trigger:
                if (lookingAt)
                    triggerAction();
                break;
            case InteractionEnum._switch:
                HighLight();
                break;
        }
    }
    public void triggerAction()
    {
        if (interationType == InteractionEnum.pickable
           || interationType == InteractionEnum._switch)
        {
            float clicked = Input.GetAxis("Fire2");
            if(clicked!=0)
            {
                triggered = true;
            }
        }
        else
        {
            triggered=true;
        }
    }

    public void HighLight()
    {
        if(lookingAt)
        {
            if (render.material.GetColor("_EmissionColor") != originalColor)
                render.material.SetColor("_EmissionColor", originalColor);
            triggerAction();
        }
        else
        {
            if(render.material.GetColor("_EmissionColor") != Color.black)
                render.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
