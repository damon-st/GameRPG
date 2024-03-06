using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeAttributes
{
    Force,
    Intelligence,
    Skill
}


public class AttributoButtom : MonoBehaviour
{

    public static Action<TypeAttributes> EventAddAttribute ;

    [SerializeField] private TypeAttributes type;

    public void AddAttribute()
    {
        EventAddAttribute?.Invoke(type);
    }
    
}
