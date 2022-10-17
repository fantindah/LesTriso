using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemyMovement : MonoBehaviour
{
    public List<Movement> movements = new();
    public bool canMove;
    public float timeBetweenMoves;


    private void Start()
    {
        StartCoroutine(StartMovement());
    }

    IEnumerator StartMovement()
    {
        foreach(Movement movement in movements)
        {

            while(movement.amountOfMovement > 0)
            {

                if (!canMove)
                {
                    yield return new WaitForSeconds(timeBetweenMoves);
                    continue;
                }

                if (movement.direction == Direction.RIGHT || movement.direction == Direction.LEFT)
                {
                    transform.position = new Vector3(transform.position.x + (movement.direction == Direction.RIGHT ? 1 : -1), transform.position.y);
                }
                else if ((movement.direction == Direction.UP || movement.direction == Direction.DOWN))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + (movement.direction == Direction.UP ? 1 : -1));
                }
                movement.amountOfMovement--;
                yield return new WaitForSeconds(timeBetweenMoves);
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 startingFrom = transform.position;
        foreach(Movement movement in movements)
        {
            Vector3 direction = startingFrom;

            if(movement.direction == Direction.RIGHT || movement.direction == Direction.LEFT)
            {
                direction.x += (movement.amountOfMovement * (movement.direction == Direction.RIGHT ? 1 : -1));
            } 
            else if((movement.direction == Direction.UP || movement.direction == Direction.DOWN))
            {
                direction.y += (movement.amountOfMovement * (movement.direction == Direction.UP ? 1 : -1));
            }
            Gizmos.DrawLine(startingFrom, direction);

            startingFrom = direction;
        }
    }

}

[Serializable]
public class Movement
{
    public int amountOfMovement;
    public Direction direction;
}

public enum Direction
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}
