using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public string GameSeed = "Default";
    [DoNotSerialize]
    public int CurrentSeed = 0;
    private void Awake()
    {
        CurrentSeed = GameSeed.GetHashCode();
        Random.InitState(CurrentSeed);
    }
}
