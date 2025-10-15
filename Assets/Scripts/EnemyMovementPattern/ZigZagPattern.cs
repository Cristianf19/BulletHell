using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagPattern : EnemyMovementPattern
{
    public float frequency = 2f;
    public float amplitude = 1f;

    public override Vector2 GetDirection()
    {
        float x = Mathf.Sin(Time.time * frequency) * amplitude;
        return new Vector2(x, -1f).normalized;
    }
}
