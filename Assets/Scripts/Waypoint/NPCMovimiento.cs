using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovimiento : WaypointMovimiento
{
    [SerializeField] private DirectionMove direction;

    private readonly int walkingDown = Animator.StringToHash("MoveDown");

    protected override void RotateCharacter()
    {
        if (direction != DirectionMove.Horizontal) return;
        if (PointForMove.x > lastPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void RotateVertical()
    {
        if(direction != DirectionMove.Vertical) return;
        if (PointForMove.y > lastPosition.y)
        {
            _animator.SetBool(walkingDown, false);
        }
        else
        {
            _animator.SetBool(walkingDown, true);
        }
    }
}
