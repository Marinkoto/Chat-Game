using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    bool Hittable { get; set; }
    void TakeDamage(int damage);
    void ReturnHealth(int healthToReturn);
    void Die();
}
