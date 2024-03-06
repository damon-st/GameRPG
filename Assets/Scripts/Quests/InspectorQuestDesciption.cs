using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectorQuestDesciption : QuestDescription
{
    [SerializeField] private TextMeshProUGUI questRecompensa;

    public override void ConfigQuestUI(Quest questForCharge)
    {
        base.ConfigQuestUI(questForCharge);
        questRecompensa.text = $"-{questForCharge.RecompensaOro} oro\n"+
            $"-{questForCharge.RecompensaExp} epx\n"+
            $"-{questForCharge.RecompensaItem.Item.name} x{questForCharge.RecompensaItem.Amount}";
    }


    public void AceptQuest()
    {
        if (QuestLoad == null) return;
        QuestLoad.QuestAcepted = true;
        QuestManager.Instance.AddQuest(QuestLoad);
        gameObject.SetActive(false);
    }
}
