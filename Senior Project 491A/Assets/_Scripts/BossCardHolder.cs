using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BossCardHolder : EnemyCardHolder
{
    protected override void Awake()
    {
        //base.Awake();
        this.enabled = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnMouseDown()
    {
        Debug.Log("boss has been clicked");
    }

}
