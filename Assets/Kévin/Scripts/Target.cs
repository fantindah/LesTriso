using System;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public TargetLife targetLife;
    private int numberOfClickBeforeDeath;

    private void Start()
    {
        numberOfClickBeforeDeath = UnityEngine.Random.Range(targetLife.minInclusive, targetLife.maxExclusive);
    }

    private void OnMouseDown()
    {
        numberOfClickBeforeDeath--;
        if(numberOfClickBeforeDeath <= 0)
        {
            Destroy(gameObject);
        }
    }
}

[Serializable]
public struct TargetLife
{
    public int minInclusive, maxExclusive;
}
