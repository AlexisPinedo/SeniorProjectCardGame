using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimationObject : AnimationObjectBase
{
    public Collider2D cardTouch;
    
    public ScaleAnimationObject(PlayerCardDisplay cardDisplay, bool shouldDestroy = false, Collider2D cardTouch = null)
    {
        this.cardDisplay = cardDisplay;
        this.shouldDestroy = shouldDestroy;
        this.cardTouch = cardTouch;
    }

}
