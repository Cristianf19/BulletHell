using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPattern : EnemyMovementPattern
{
    public float duration = 2f;
    private float startTime;

    private void Start()
    {
        startTime = Time.time; 
    }

    public override Vector2 GetDirection()
    {
        if (Time.time - startTime > duration)
        {
            return Vector2.zero;
        }

        return Vector2.down;
    }
}
