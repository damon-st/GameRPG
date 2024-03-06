using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogoManager : Singleton<DialogoManager>
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameTMP;
    [SerializeField] private TextMeshProUGUI npcConversationTMP;

    public NPCInteraction NPCAvalible { get; set; }

    private Queue<string> dialogosSecuence;
    private bool dialogoAnimate;
    private bool despedidaShowing;

    private void Start()
    {
        dialogosSecuence = new Queue<string>();
    }

    private void Update()
    {
        if (npcNameTMP == null) return;
        if (Input.GetKeyDown(KeyCode.E) && NPCAvalible!=null)
        {
            ConfigPanel(NPCAvalible.Dialogo);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (despedidaShowing)
            {
                OpenClosePanelDialog(false);
                despedidaShowing = false;
                return;
            }

            if ( NPCAvalible.Dialogo.ConteintInteractionExtra)
            {
                UIManager.Instance.OpenPanelInteraction(NPCAvalible.Dialogo.InterationExtra);
                OpenClosePanelDialog(false);
                return;
            }

            if (dialogoAnimate)
            {
                ContineDialogo();
            }
        }
    }

    public void OpenClosePanelDialog(bool status)
    {
        panelDialogo.SetActive(status);
    }

    private void ConfigPanel(NPCDialogo npcDialogo)
    {
        OpenClosePanelDialog(true);
        CargarDialogosSecuence(npcDialogo);



        npcIcon.sprite = npcDialogo.Icon;
        npcNameTMP.text = $"{npcDialogo.Name}:";

        ShowTextWithAnimation(npcDialogo.Saludo);
    }

    private void CargarDialogosSecuence(NPCDialogo npcDialogo)
    {
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0) return;

        for (int i = 0; i < npcDialogo.Conversacion.Length; i++)
        {
            dialogosSecuence.Enqueue(npcDialogo.Conversacion[i].Oracion);
        }
    }

    private void ContineDialogo()
    {
        if (NPCAvalible == null) return;

        if (despedidaShowing) return;

        if (dialogosSecuence.Count == 0)
        {
            string despedida = NPCAvalible.Dialogo.Despedida;
            ShowTextWithAnimation(despedida);
            despedidaShowing = true;
            return;
        }

        string nextDialogo = dialogosSecuence.Dequeue();
        ShowTextWithAnimation(nextDialogo);
    }

    private IEnumerator AnimateText(string oracion)
    {
        dialogoAnimate = false;

        npcConversationTMP.text = "";
        char[] letters = oracion.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            npcConversationTMP.text += letters[i];
            yield return new WaitForSeconds(0.03f);
        }

        dialogoAnimate = true;
    }

    private void ShowTextWithAnimation(string oracion)
    {
        StartCoroutine(AnimateText(oracion));
    }
}
