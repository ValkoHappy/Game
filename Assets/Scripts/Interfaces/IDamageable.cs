using UnityEngine;

namespace Scripts.Interfaces
{
    public interface IDamageable
    {
        bool ApplayDamage(Rigidbody rigidbody, int damage, int force);
    }
}