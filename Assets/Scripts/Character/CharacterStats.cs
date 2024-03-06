using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Stats")]
public class CharacterStats : ScriptableObject
{
    [Header("Stats")]
    public float Damage=5f;
    public float Defending=2f;
    public float Velocity=5f;
    public float Level;
    public float ExpActual;
    public float ExpRequiredNextLevel;
    public float ExpTotal;

    [Range(0f, 100f)] public float PorcentageCritic;
    [Range(0f, 100f)] public float PorcentageBlocking;


    [Header("Atributos")]
    public int Force;
    public int Intelligence;
    public int Skill;

    [HideInInspector]public int PointsAvalibles;


    public void AddBoundForAttributeForce()
    {
        Damage += 2f;
        Defending += 1f;
        PorcentageBlocking += 0.03f;
    }

    public void AddBoundForAttribueIntelligence()
    {
        Damage += 3f;
        PorcentageCritic += 0.2f;
    }

    public void AddBoundForAttributeSkill()
    {
        Velocity += 0.1f;
        PorcentageBlocking += 0.5f;
    }

    public void AddBonusForWeapoin(Arma weapoin)
    {
        Damage += weapoin.Damage;
        PorcentageCritic += weapoin.ChanceCritical;
        PorcentageBlocking += weapoin.ChanceBloking;
    }

    public void RemoveBonusForWeapoin(Arma weapoin)
    {
        Damage -= weapoin.Damage;
        PorcentageCritic -= weapoin.ChanceCritical;
        PorcentageBlocking -= weapoin.ChanceBloking;
    }


    public void ResetearValores()
    {
        Damage = 5f;
        Defending = 2f;
        Velocity = 5f;
        Level = 1f;
        ExpActual = 0f;
        ExpRequiredNextLevel = 0f;
        ExpTotal = 0f;
        PorcentageBlocking = 0f;
        PorcentageCritic = 0f;

        Force = 0;
        Intelligence = 0;
        Skill = 0;

        PointsAvalibles = 0;
    }
}
