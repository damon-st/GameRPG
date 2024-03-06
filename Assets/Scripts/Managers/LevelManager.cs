using UnityEngine;

internal class LevelManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject panelPause;

    [SerializeField] private Character character;
    [SerializeField] private Transform pointReaparence;




    public void PauseGame()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }

    public void ResumeGame()
    {

        Time.timeScale = 1f;
        panelPause.SetActive(false);
    }

    public void QUitApplication()
    {
        Application.Quit();
    }
    

    public void RestarPlayer()
    {
        character.transform.localPosition = pointReaparence.position;
        character.RestarCharacter();
    }

}
