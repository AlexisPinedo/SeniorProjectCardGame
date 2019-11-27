using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCompletionManager : MonoBehaviour
{
    //[SerializeField]
    //private PlayerAnimationManager playerAnimationManager;
    
    //[SerializeField]
    //private ShopAnimationManager shopAnimationManager;
    
    //[SerializeField]
    //private PlayZoneAnimationManager playZoneAnimationManager;
    public static event Action<bool> AllAnimationsCompleted;

    public void CheckIfAnimationsCompleted()
    {
        if (PlayerAnimationManager.SharedInstance.AnimationsCompleted &&
            ShopAnimationManager.SharedInstance.AnimationsCompleted &&
            PlayZoneAnimationManager.SharedInstance.AnimationsCompleted)
            AllAnimationsCompleted(true);
        else
            AllAnimationsCompleted(false);
    }
}
