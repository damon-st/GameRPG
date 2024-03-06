using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public static Action<Quest> EventQuestCompleted;

    [Header("Info")]
    public string Name;
    public string ID;
    public int AmountObjetivo;

    [Header("Descripcion")]
    [TextArea] public string Description;

    [Header("Recompensas")]
    public int RecompensaOro;
    public float RecompensaExp;
    public QuestRecompensaItem RecompensaItem;

    [HideInInspector] public int AmountActual;
    [HideInInspector] public bool QuestCompleted;
    [HideInInspector] public bool QuestAcepted;


    public void AddProggres(int amount)
    {
        AmountActual += amount;
        VerifyQuestCompletado();
    }

    private void VerifyQuestCompletado()
    {
        if (AmountActual >= AmountObjetivo)
        {
            AmountActual = AmountObjetivo;
            QuestComplet();
        }
    }

    private void QuestComplet()
    {
        if (QuestCompleted) return;
        QuestCompleted = true;
        EventQuestCompleted?.Invoke(this);
    }

    public void ResetQuest()
    {
        QuestCompleted = false;
        AmountActual = 0;
    }

}

[Serializable]
public class QuestRecompensaItem
{
    public InventarioItem Item;
    public int Amount;
}