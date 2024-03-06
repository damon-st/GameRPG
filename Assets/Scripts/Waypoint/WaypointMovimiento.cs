using UnityEngine;


public enum DirectionMove
{
    Horizontal,
    Vertical
} 

public class WaypointMovimiento : MonoBehaviour
{

    [SerializeField] private float velocity;

    public Vector3 PointForMove => _waypoint.GetPositionMove(pointActualIndex);

    protected Waypoint _waypoint;
    protected int pointActualIndex;
    protected Vector3 lastPosition;
    protected Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
     pointActualIndex = 0;
        _waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        RotateCharacter();
        RotateVertical();
        if (CheckPointActualReached())
        {
            UpdateIndexMove();
        }
    }

    private void MoveCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, PointForMove, velocity*Time.deltaTime);
    }

    private bool CheckPointActualReached()
    {
        float disntaceToPoint = (transform.position - PointForMove).magnitude;
        if (disntaceToPoint < 0.1f) 
        {
            lastPosition= transform.position;
            return true;
        }
        return false;
    }

    private void UpdateIndexMove()
    {
        if (pointActualIndex==_waypoint.Points.Length-1)
        {
            pointActualIndex = 0;
        }
        else
        {
            if (pointActualIndex < _waypoint.Points.Length - 1)
            {
                pointActualIndex ++;
            }
        }
    }

    protected virtual void RotateCharacter()
    {
        
    }

    protected virtual void RotateVertical()
    {

    }
}
