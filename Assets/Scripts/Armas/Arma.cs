using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeWaepon
{
    Magic,
    Melee
}

[CreateAssetMenu(menuName ="Personaje/Arma")]
public class Arma : ScriptableObject
{
    [Header("Config")]
    public Sprite Weapon;
    public Sprite IconSkill;
    public TypeWaepon Type;
    public float Damage;

    [Header("Arma Magica")]
    public Proyectil ProyectilPrefab;
    public float ManaRequired;

    [Header("Stats")]
    public float ChanceCritical;
    public float ChanceBloking;

}
