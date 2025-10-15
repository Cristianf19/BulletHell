using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Stats : MonoBehaviour
{
    public abstract int maxHealth();
    public abstract float cadencyShoot();
    public abstract float speed();
    public abstract float speedShoot();
    public abstract int bulletDamage();
    public abstract Vector2 bulletDirection();

}