using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public List<Material> materialsToChange;
    private Interactable interact;
    private bool canChange = false;
    public enum triggerStates { changingMaterial,ready};
    private triggerStates currentState;
    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<Interactable>();
        currentState = triggerStates.ready;
    }

    // Update is called once per frame
    void Update()
    {
        if(interact.triggered)
        {
            canChange = true;
        }
        else
        {
            if(canChange && currentState==triggerStates.ready)
            {
                currentState = triggerStates.changingMaterial;
                ChangeMaterial();
            }
            else if(canChange && currentState == triggerStates.changingMaterial)
            {
                canChange = false;
            }
        }
    }

    System.Random rnd = new System.Random(System.Guid.NewGuid().GetHashCode());
    private void ChangeMaterial()
    {
        canChange = false;
        int index = rnd.Next(-1, materialsToChange.Count);
        if(index!=-1)
        {
            gameObject.GetComponent<Renderer>().material = materialsToChange[index];
        }
        currentState = triggerStates.ready;

    }
}
