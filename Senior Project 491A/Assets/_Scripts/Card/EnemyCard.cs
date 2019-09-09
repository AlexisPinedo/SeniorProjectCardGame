using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCard : Card
{
    [SerializeField] protected int _rewardValue, _healthValue;

    public int HealthValue
    {
        get { return _healthValue; }
        set { _healthValue = value; }
    }

    public int RewardValue
    {
        get => _rewardValue;
        set => _rewardValue = value;
    }
}












//public abstract class EnemyCard : Card
//{
//    //public delegate void _EnemyDestroyed(Vector2 cardPosition, bool cardRemoved);
//    //public static event _EnemyDestroyed EnemyDestroyed;
//
//    // Create delegate event like the one above for the enemycardclicked
//    // Delegate will be type void and take a gameobject as a parameter
//    //public delegate void _EnemyCardClicked(EnemyCard enemyCard);
//    //public static event _EnemyCardClicked EnemyClicked;
//
//    [SerializeField]
//    protected int _rewardValue, _healthValue;
//
//    public int HealthValue
//    {
//        get { return _healthValue; }
//        set { _healthValue = value; }
//    }
//public BossTurnCardPlayer manager;

    //private void Awake()
    //{
    //    manager = this.GetComponent<BossTurnCardPlayer>();
    //}

    //protected virtual void OnDestroy()
    //{
    //    if (EnemyDestroyed != null)
    //        EnemyDestroyed.Invoke(this.transform.position, false);
    //}

    //public virtual void OnMouseDown()
    //{
    //    //invoke your event like in the ondestroy method and pass in this.gameObject
    //    if (EnemyClicked != null)
    //    {
    //        EnemyClicked.Invoke(this);
    //    }
    //}
//}
