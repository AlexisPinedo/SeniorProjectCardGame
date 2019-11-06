using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerHandler : MonoBehaviour
{

    void OnDestroy()
    {
        //Debug.Log("Before: " + ParticlePlay.summonerCount);
        ParticlePlay.summonerCount--;
        //Debug.Log("After: " + ParticlePlay.summonerCount);
    }
}
