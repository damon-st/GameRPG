using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points;

    public Vector3[] Points => points;


    public Vector3 PositionActual { get; set; }

    private bool gameInit;


    // Start is called before the first frame update
    void Start()
    {
        gameInit = true;
        PositionActual = transform.position;   
    }


    public Vector3 GetPositionMove(int index)
    {
        return PositionActual+ points[index];
    }
   

    private void OnDrawGizmos()
    {
        if(!gameInit && transform.hasChanged)
        {
            PositionActual = transform.position;
        }
        if(points==null || points.Length<=0) return;
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(points[i]+PositionActual, 0.5f);
            if(i< points.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + PositionActual, points[i+1] + PositionActual);
            }
        }
    }
}
