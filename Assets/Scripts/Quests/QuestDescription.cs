using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;

    public Quest QuestLoad { get; set; }

    public virtual void ConfigQuestUI(Quest questForCharge)
    {
        QuestLoad= questForCharge;
        questName.text = questForCharge.Name;
        questDescription.text = questForCharge.Description;

    }
}
