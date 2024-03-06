using UnityEngine;

internal class LevelManager : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Transform pointReaparence;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            character.transform.localPosition = pointReaparence.position;
            character.RestarCharacter();
        }
    }

}
