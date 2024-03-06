using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRemoveTransition : MonoBehaviour
{
    [SerializeField] private CinemachineBrain brain;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            brain.m_DefaultBlend.m_Time = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Restar());
        }
    }
    IEnumerator Restar()
    {
        yield return new WaitForSeconds(1f);
        if (brain.isActiveAndEnabled)
        {
            brain.m_DefaultBlend.m_Time = 0.5f;
        }
    }
}
