using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnimationObject : AnimationObjectBase
{
    public Vector3 destination;
    public float lerpTime;
    public bool canScale;
    public bool storeOriginalPosition;
    public bool stallOtherAnimations;
    
    public TransformAnimationObject(PlayerCardDisplay cardDisplay, Vector3 destination, 
        float lerpTime, bool canScale = false, bool storeOriginalPosition = false, 
        bool shouldDestroy = false, bool stallOtherAnimations = true)
    {
        this.cardDisplay = cardDisplay;
        this.destination = destination;
        this.lerpTime = lerpTime;
        this.canScale = canScale;
        this.storeOriginalPosition = storeOriginalPosition;
        this.stallOtherAnimations = stallOtherAnimations;
        this.shouldDestroy = shouldDestroy;
    }
    
}
