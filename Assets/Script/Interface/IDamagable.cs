using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    HpBar hpBar { get; set; }
    bool isPlayerSide { get; set; }
    void BeDamage(int damageValue);
}
