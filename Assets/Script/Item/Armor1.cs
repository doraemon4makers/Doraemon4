using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor1
{
    private int armorValue;

    public Armor1(int value)//没有自己写构造方法的时候 系统会自动帮你创建一个构造方法 自己写了之后就不再创建
    {
        armorValue = value;
    }

    public int CalculateDamage(int damageValue)
    {
        Debug.Log("Damage:" + (damageValue - armorValue));
        return Mathf.Max(damageValue - armorValue, 0); // 1.同类型的数值计算得到的是同类型 2.不同类型的数值计算 会得到更高精度的类型
    }
}
