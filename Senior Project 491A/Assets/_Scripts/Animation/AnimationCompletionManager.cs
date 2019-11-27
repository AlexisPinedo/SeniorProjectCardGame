using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCompletionManager : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimationManager playerAnimationManager;
    
    [SerializeField]
    private ShopAnimationManager shopAnimationManager;
    
    [SerializeField]
    private PlayZoneAnimationManager playZoneAnimationManager;


    private bool CheckIfAnimationsCompleted()
    {
        if (playerAnimationManager && shopAnimationManager && playZoneAnimationManager)
            return true;
        else     
            return false;
    }
}
