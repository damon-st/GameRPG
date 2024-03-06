using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetector : MonoBehaviour
{
    public static Action<EnemigoInteraction> EventEnemyDetected;
    public static Action EventEnemyLost;

    public EnemigoInteraction EnemyDetected { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyDetected = collision.GetComponent<EnemigoInteraction>();
            if (EnemyDetected.GetComponent<EnemigoVida>().Health > 0)
            {
                EventEnemyDetected?.Invoke(EnemyDetected);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EventEnemyLost?.Invoke();
        }
    }
}
