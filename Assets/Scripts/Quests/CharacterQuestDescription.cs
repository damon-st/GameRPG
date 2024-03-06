using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterQuestDescription : QuestDescription
{
    [SerializeField] private TextMeshProUGUI taskObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;

    [Header("Item")]
    [SerializeField] private Image recompensaItemIcon;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;


    private void Update()
    {
        if (QuestLoad == null) return;
        if (QuestLoad.QuestCompleted) return;
        taskObjetivo.text = $"{QuestLoad.AmountActual}/{QuestLoad.AmountObjetivo}";
    }

    public override void ConfigQuestUI(Quest questForCharge)
    {
        base.ConfigQuestUI(questForCharge);
        recompensaOro.text=questForCharge.RecompensaOro.ToString();
        recompensaExp.text = questForCharge.RecompensaExp.ToString();
        taskObjetivo.text = $"{questForCharge.AmountActual}/{questForCharge.AmountObjetivo}";

        recompensaItemIcon.sprite = questForCharge.RecompensaItem.Item.Icon;
        recompensaItemCantidad.text = questForCharge.RecompensaItem.Amount.ToString();
    }

    private void QuestCompletedResponse(Quest quest) 
    {
        if(QuestLoad.ID == quest.ID)
        {
            taskObjetivo.text = $"{QuestLoad.AmountActual}/{QuestLoad.AmountObjetivo}";
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (QuestLoad.QuestCompleted)
        {
            gameObject.SetActive(false);
        }
        Quest.EventQuestCompleted += QuestCompletedResponse;
    }

    private void OnDisable()
    {
        Quest.EventQuestCompleted -= QuestCompletedResponse;

    }
}
