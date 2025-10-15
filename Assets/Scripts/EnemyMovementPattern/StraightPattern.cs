using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightPattern : EnemyMovementPattern
{
    public override Vector2 GetDirection()
    {
        return Vector2.down;
    }
}
