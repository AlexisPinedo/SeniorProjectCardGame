using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagementBase<T> : MonoBehaviour where T : AnimationObjectBase
{
    protected Queue <T> animQueue = new Queue<T>();

    //When a card in player hand is moving
    protected bool animationsCompleted = true;

    public bool AnimationsCompleted
    {
        get => animationsCompleted;
    }

    protected void EndAnimEvent()
    {
        animationsCompleted = true;
    }

    public virtual void AddObjectToQueue(T animObject)
    {
        if (animQueue.Count == 0)
        {
            animQueue.Enqueue(animObject);
            //Debug.Log("Starting addition of objects");
            StartCoroutine(HandleAnim());
        }
        else
        {
            animQueue.Enqueue(animObject);
        }
    }

    public virtual IEnumerator HandleAnim()
    {
        yield return null;
    }

    void CheckIfAllAnimationManagersAreComplete()
    {
        
    }
    
}
