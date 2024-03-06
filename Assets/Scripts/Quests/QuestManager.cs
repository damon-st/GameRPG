using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Character")]
    [SerializeField] private Character character;

    [Header("Quests")]
    [SerializeField] private Quest[] questAvalibles;

    [Header("Inspector Quests")]
    [SerializeField] private InspectorQuestDesciption inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContainer;

    [Header("Personaje Quests")]
    [SerializeField] private CharacterQuestDescription characterQuestPrefab;
    [SerializeField] private Transform characterQuestContainer;

    [Header("Panel Quest Completed")]
    [SerializeField] private GameObject panelQuestCompleted;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questRecompensaExt;
    [SerializeField] private TextMeshProUGUI questRecompensItemAmount;
    [SerializeField] private Image questRecomepsItemIcon;

    public Quest QuestForReclamar { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        LoadQuestInIspector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {

        }
    }


    private void LoadQuestInIspector()
    {
        for (int i = 0; i < questAvalibles.Length; i++)
        {
            InspectorQuestDesciption newQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContainer);
            newQuest.ConfigQuestUI(questAvalibles[i]);
        }
    }

    private void AddQuestForCompleted(Quest questForCompleted)
    {
        CharacterQuestDescription newQuest = Instantiate(characterQuestPrefab, characterQuestContainer);
        newQuest.ConfigQuestUI(questForCompleted);
    }

    public void AddQuest(Quest questForCompleted)
    {
        AddQuestForCompleted(questForCompleted);
    }

    public void ClaimPrizes()
    {
        if (QuestForReclamar == null) return;
        MonedasManager.Instance.AddCoins(QuestForReclamar.RecompensaOro);
        character.CharacterExperience.AddExperience(QuestForReclamar.RecompensaExp);
        Inventario.Instance.AddItem(QuestForReclamar.RecompensaItem.Item, QuestForReclamar.RecompensaItem.Amount);
        panelQuestCompleted.SetActive(false);
        QuestForReclamar = null;
    }

    public void AddProggress(string questID, int amount)
    {
        Quest questForUpdate = QuestsExists(questID);
        if (questForUpdate.QuestAcepted)
        {
            questForUpdate?.AddProggres(amount);
        }
    }

    private Quest QuestsExists(string questID)
    {
        for (int i = 0; i < questAvalibles.Length; i++)
        {
            if (questAvalibles[i].ID == questID)
            {
                return questAvalibles[i];
            }
        }
        return null;
    }

    private void ShowQuestCompleted(Quest quest)
    {
        panelQuestCompleted.SetActive(true);
        questName.text = quest.Name;
        questRecompensaOro.text = quest.RecompensaOro.ToString();
        questRecompensaExt.text = quest.RecompensaExp.ToString();
        questRecompensItemAmount.text = quest.RecompensaItem.Amount.ToString();
        questRecomepsItemIcon.sprite = quest.RecompensaItem.Item.Icon;

    }

    private void QuestCompletedResponse(Quest questCompleted)
    {
        QuestForReclamar = QuestsExists(questCompleted.ID);
        if (QuestForReclamar != null)
        {
            ShowQuestCompleted(QuestForReclamar);
        }
    }


    private void OnEnable()
    {
        for (int i = 0; i < questAvalibles.Length; i++)
        {
            questAvalibles[i].ResetQuest();
        }
        Quest.EventQuestCompleted += QuestCompletedResponse;
    }


    private void OnDisable()
    {
        Quest.EventQuestCompleted -= QuestCompletedResponse;
    }
}
