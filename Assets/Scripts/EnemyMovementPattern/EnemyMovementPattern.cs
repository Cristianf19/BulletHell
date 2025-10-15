using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementPattern : MonoBehaviour
{
    public abstract Vector2 GetDirection();
}
