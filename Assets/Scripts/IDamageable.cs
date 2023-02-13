using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    bool ApplayDamage(Rigidbody rigidbody, int damage, int force);
}
