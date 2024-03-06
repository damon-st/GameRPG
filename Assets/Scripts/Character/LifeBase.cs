using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBase : MonoBehaviour
{
    [SerializeField] protected float healthInit;
    [SerializeField] protected float healthMax;

    public float Health { get; protected set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Health = healthInit;
    }

    public void ReceiverDamage(float amount) 
    {
        if (amount <= 0) return;

        if (Health > 0f)
        {
            Health -= amount;
            UpdateBarLife(Health, healthMax);
            if(Health <=0f)
            {
                Health = 0f;
                UpdateBarLife(Health, healthMax);
                CharacterDefeated();
            }
        }
    }

    protected virtual void UpdateBarLife(float lifeActual,float lifeMax)
    {

    }

    protected virtual void CharacterDefeated()
    {

    }
}
