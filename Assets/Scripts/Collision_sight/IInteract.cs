using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteract
{
    bool lookingAt { get; set; }

    bool triggered { get; set; }

    InteractionEnum interationType { get; set; } 

    void triggerAction();
}
