using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : AnimationManagementBase<TransformAnimationObject>
{
    private void Awake()
    {
        
    }

    IEnumerator HandleAnimation()
    {
        animationsCompleted = false;
        //run animation logic
        //dequeu
        //check to run again
        animationsCompleted = true;
        yield return null;
    }
    
}
