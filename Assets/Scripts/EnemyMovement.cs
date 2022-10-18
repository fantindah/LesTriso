using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    public List<MovementVector> movementFrame = new();
    public float timeBetweenMoves;
    public bool goBack = false;
    private Coroutine movementCoroutine;
    private bool isGoingBack = false;

    //GIZMOS
    private List<MovementVector> movementFrameConst = new();

    private void Start()
    {

        foreach(MovementVector movement in movementFrame)
        {
            movement.maxNorm = movement.norm;
            movement.isPositive = (movement.norm >= 0);
            movementFrameConst.Add(new MovementVector(movement.direction, movement.norm));
        }

        movementCoroutine = StartCoroutine(Movement());
    }

    private void Update()
    {
        if (goBack)
        {
            goBack = false;
            ReverseMovement();
        }
    }

    public IEnumerator Movement()
    {
        foreach (MovementVector movement in movementFrame)
        {
            if (isGoingBack)
            {
                while (movement.norm != movement.maxNorm)
                {
                    switch (movement.direction)
                    {
                        case MovementDirection.Horizontal:
                            if (movement.isPositive)
                            {
                                transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                                movement.norm++;
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                                movement.norm--;
                            }
                            break;
                        case MovementDirection.Vertical:
                            if (movement.isPositive)
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                                movement.norm++;
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
                                movement.norm--;
                            }
                            break;
                    }
                    yield return new WaitForSeconds(timeBetweenMoves);
                }
            } else
            {
                while (movement.norm != 0)
                {
                    switch (movement.direction)
                    {
                        case MovementDirection.Horizontal:
                            if (movement.isPositive)
                            {
                                transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                                movement.norm--;
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
                                movement.norm++;
                            }
                            break;
                        case MovementDirection.Vertical:
                            if (movement.isPositive)
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
                                movement.norm--;
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
                                movement.norm++;
                            }
                            break;
                    }
                    yield return new WaitForSeconds(timeBetweenMoves);
                }
            }
        }
    }

    public void ReverseMovement()
    {
        if(movementCoroutine is not null) StopCoroutine(movementCoroutine);
        movementFrame.Reverse();
        isGoingBack = !isGoingBack;
        movementCoroutine = StartCoroutine(Movement());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 startingFrom = transform.position;

        List<MovementVector> movementsGizmo = movementFrame.ToArray().ToList();
        if (isGoingBack) movementsGizmo.Reverse();

        foreach (MovementVector movement in movementsGizmo)
        {
            Vector3 direction = startingFrom;

            if (movement.direction == MovementDirection.Horizontal)
            {
                direction.x += movement.norm;
            }
            else if (movement.direction == MovementDirection.Vertical)
            {
                direction.y += movement.norm;
            }
            Gizmos.DrawLine(startingFrom, direction);

            startingFrom = direction;
        }
    }

}

[Serializable]
public class MovementVector
{
    public MovementDirection direction;
    public int norm;
    [HideInInspector] public int maxNorm;
    [HideInInspector] public bool isPositive;

    public MovementVector(MovementDirection direction, int norm)
    {
        this.direction = direction;
        this.norm = norm;
        maxNorm = norm;
        isPositive = (norm >= 0);
    }   
}

public enum MovementDirection
{
    Vertical,
    Horizontal
}
