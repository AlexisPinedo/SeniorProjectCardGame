using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagementBase<T> : MonoBehaviour where T : AnimationObjectBase
{
    protected Queue <T> animQueue = new Queue<T>();

    //When a card in player hand is moving
    protected bool animationsCompleted = false;

    public bool AnimationsCompleted
    {
        get => animationsCompleted;
    }

    void CheckIfAllAnimationManagersAreComplete()
    {
        
    }
    
}
