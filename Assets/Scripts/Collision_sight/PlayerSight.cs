using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    //the layer that the interactables are on
    public LayerMask layerMaskToCheck;

    //length of the ray
    public float rayDistance = 10;

    //list of game objects previously looked at
    private List<GameObject> lookedAt;
    //list of game objects that are currently being looked at
    List<GameObject> objectsBeingLookedAt = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //initalize the list
        lookedAt = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if game objects are being hit
        CheckHit();
        //check if game object is still being hit
        CheckIfStillLooking();
    }

    void CheckHit()
    {
        //hit for first set of rays
        RaycastHit hit;
        //hit for second set of rays
        RaycastHit hit2;
        //origin of ray
        Vector3 orgin = GameObject.FindGameObjectWithTag("Player").transform.position;
        //lower the y value of orgin
        orgin.y -= .8f;
        //create ray objects
        Ray ray = new Ray(orgin, transform.forward);
        Ray ray2 = new Ray(orgin, transform.forward);

        //y value of rays
        for (float y = 0; y < 2f; y+=.2f)
        {
            //x value of rays
            for (float x = 0; x < 1; x+=.3f)
            {
                //offset of orgin
                Vector3 offset = new Vector3(ray.origin.x + x, ray.origin.y + y, ray.origin.z);
                //draw and check raycast
                if(Physics.Raycast(offset,ray.direction, out hit, rayDistance, layerMaskToCheck))
                {
                    //get the interactable script
                    IInteract interactType = hit.collider.gameObject.GetComponent<Interactable>();
                    //add to previous list
                    if (!lookedAt.Contains(hit.collider.gameObject))
                        lookedAt.Add(hit.collider.gameObject);
                    //add to current list
                    if (!objectsBeingLookedAt.Contains(hit.collider.gameObject))
                        objectsBeingLookedAt.Add(hit.collider.gameObject);
                    //if the object can be set then set looking at to true
                    if (interactType != null)
                    {
                        interactType.lookingAt = true;
                    }
                }
                //draw debug
                Debug.DrawRay(offset, ray.direction,Color.green);

                //offset orgin
                Vector3 offset2 = new Vector3(ray2.origin.x - x, ray2.origin.y + y, ray2.origin.z);
                //check if something is being hit
                if (Physics.Raycast(offset2,ray2.direction, out hit2, rayDistance, layerMaskToCheck))
                {
                    //get the interaction script of object
                    IInteract interactType = hit2.collider.gameObject.GetComponent<Interactable>();
                    //add to previous list
                    if (!lookedAt.Contains(hit2.collider.gameObject))
                        lookedAt.Add(hit2.collider.gameObject);
                    //add to current list
                    if (!objectsBeingLookedAt.Contains(hit2.collider.gameObject))
                        objectsBeingLookedAt.Add(hit2.collider.gameObject);
                    //if not null then set variable to true
                    if (interactType!=null)
                    {
                        interactType.lookingAt = true;
                    }
                }
                //draw debug
                Debug.DrawRay(offset2, ray2.direction, Color.blue);
            }
        }
    }

    //check if the object is still being looked at
    void CheckIfStillLooking()
    {
        //check all of the previous objects
        for(int i=0;i<lookedAt.Count;i++)
        {
            //if they aren't currently being looked at then reset
            if(!objectsBeingLookedAt.Contains(lookedAt[i]))
            {
                if (lookedAt[i].GetComponent<Interactable>() != null)
                    //set variable to false
                    lookedAt[i].GetComponent<Interactable>().lookingAt = false;
                //remove from previous list
                lookedAt.Remove(lookedAt[i]);
            }
        }
        //clear the current list
        objectsBeingLookedAt.Clear();
    }

}
