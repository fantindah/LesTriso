using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    public List<MovementVector> movementFrame = new();
    public float timeBetweenMoves, checkPointTime;
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
            movementFrameConst.Add(new MovementVector(movement.direction, movement.norm, movement.actionAfterPassed));
        }

        //movementCoroutine = StartCoroutine(Movement());
        //j'ai enlevé le lancement pour tester avec le GP
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
                switch (movement.actionAfterPassed)
                {
                    case ActionAfterPassed.RemoveFromTheList:
                        if(!isGoingBack) RemoveAllMovementFromBefore(movement);
                        break;
                    case ActionAfterPassed.Wait:
                        yield return new WaitForSeconds(checkPointTime);
                        break;
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
                switch (movement.actionAfterPassed)
                {
                    case ActionAfterPassed.RemoveFromTheList:
                        if (!isGoingBack) RemoveAllMovementFromBefore(movement);
                        break;
                    case ActionAfterPassed.Wait:
                        yield return new WaitForSeconds(checkPointTime);
                        break;
                }
            }
        }
    }

    private void ReverseMovement()
    {
        if(movementCoroutine is not null) StopCoroutine(movementCoroutine);
        movementFrame.Reverse();
        isGoingBack = !isGoingBack;
        movementCoroutine = StartCoroutine(Movement());
    }

    private void RemoveAllMovementFromBefore(MovementVector movement)
    {
        if(movementCoroutine is not null) StopCoroutine(movementCoroutine);
        int value = movementFrame.IndexOf(movement);
        while (value >= 0)
        {
            movementFrame.Remove(movementFrame[value]);
            value--;
        }
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

            switch (movement.actionAfterPassed)
            {
                case ActionAfterPassed.RemoveFromTheList:
                    Gizmos.DrawSphere(direction, .5f);
                    break;
                case ActionAfterPassed.Wait:
                    Gizmos.DrawCube(direction, new Vector3(1, 1, 0));
                    break;
            }

            startingFrom = direction;
        }
    }

}

[Serializable]
public class MovementVector
{
    public MovementDirection direction;
    public int norm;
    public ActionAfterPassed actionAfterPassed;
    [HideInInspector] public int maxNorm;
    [HideInInspector] public bool isPositive;

    public MovementVector(MovementDirection direction, int norm, ActionAfterPassed actionAfterPassed)
    {
        this.direction = direction;
        this.norm = norm;
        this.actionAfterPassed = actionAfterPassed;
        maxNorm = norm;
        isPositive = (norm >= 0);
    }   
}

public enum MovementDirection
{
    Vertical,
    Horizontal
}

public enum ActionAfterPassed
{
    Nothing,
    RemoveFromTheList,
    Wait
}
