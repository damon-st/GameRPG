using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    public static Action<EnemigoInteraction> EventEnemySelected;
    public static Action EventObjectNoSelected;

    public EnemigoInteraction EnemySelected { get; set; }

    private Camera camara;

    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        SelectEnemy();
    }

    private void SelectEnemy()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(camara.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,Mathf.Infinity,LayerMask.GetMask("Enemy"));

            if (hit.collider != null)
            {
                EnemySelected = hit.collider.GetComponent<EnemigoInteraction>();
                EnemigoVida enemyHelf = EnemySelected.GetComponent<EnemigoVida>();
                if (enemyHelf.Health > 0f)
                {
                    EventEnemySelected?.Invoke(EnemySelected);
                }
                else
                {
                    EnemigoLoot loot = EnemySelected.GetComponent<EnemigoLoot>();
                    LootManager.Instance.ShowLoot(loot);
                }
            }
            else
            {
                EventObjectNoSelected?.Invoke();
            }
        }
    }
}
