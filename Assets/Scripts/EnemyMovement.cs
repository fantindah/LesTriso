using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    public List<MovementVector> movementFrame = new();
    public float distanceBetweenMovements;
    public float timeBetweenMoves, checkPointTime;
    public bool goBack = false, unblocked = false;


    private Coroutine movementCoroutine;
    public bool isGoingBack = false;
    public bool isBlocked = false;
    public float backCooldown = 0f;

    public int activeMovement;
    

    //GIZMOS
    private List<MovementVector> movementFrameConst = new();

    private void Awake()
    {
        movementCoroutine = StartCoroutine(Movement());
    }

    private void Start()
    {
        foreach(MovementVector movement in movementFrame)
        {
            movement.maxNorm = movement.norm;
            movement.isPositive = (movement.norm >= 0);
            movementFrameConst.Add(new MovementVector(movement.direction, movement.norm, movement.actionAfterPassed, movement.actionAfterPassedBackward));
        }
    }

    private void Update()
    {

        backCooldown -= Time.deltaTime;


        if (goBack && backCooldown < 0) {
            backCooldown = 1f;
            goBack = false;
            ReverseMovement();
        } else
        {
            goBack = false;
        }

        if (unblocked)
        {
            unblocked = false;
            isBlocked = false;
        }
    }

    public IEnumerator Movement()
    {
        foreach (MovementVector movement in movementFrame)
        {
            activeMovement = movementFrame.IndexOf(movement);

            if (isGoingBack)
            {
                while (movement.norm != movement.maxNorm)
                {
                    switch (movement.direction)
                    {
                        case MovementDirection.Horizontal:
                            if (movement.isPositive)
                            {
                                transform.position = new Vector3(transform.position.x - distanceBetweenMovements, transform.position.y, 0);
                                movement.norm++;
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x + distanceBetweenMovements, transform.position.y, 0);
                                movement.norm--;
                            }
                            break;
                        case MovementDirection.Vertical:
                            if (movement.isPositive)
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y - distanceBetweenMovements, 0);
                                movement.norm++;
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y + distanceBetweenMovements, 0);
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
                                transform.position = new Vector3(transform.position.x + distanceBetweenMovements, transform.position.y, 0);
                                movement.norm--;
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x - distanceBetweenMovements, transform.position.y, 0);
                                movement.norm++;
                            }
                            break;
                        case MovementDirection.Vertical:
                            if (movement.isPositive)
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y + distanceBetweenMovements, 0);
                                movement.norm--;
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y - distanceBetweenMovements, 0);
                                movement.norm++;
                            }
                            break;
                    }
                    yield return new WaitForSeconds(timeBetweenMoves);
                }
            }

            switch (isGoingBack ? movement.actionAfterPassedBackward : movement.actionAfterPassed)
            {
                case ActionAfterPassed.RemoveFromTheList:
                    if (!isGoingBack) RemoveAllMovementFromBefore(movement);
                    break;
                case ActionAfterPassed.Wait:
                    yield return new WaitForSeconds(checkPointTime);
                    break;
                case ActionAfterPassed.GameOver:
                    Debug.Log("TU ES MORT AHAHAHAHAHAH");
                    break;
                case ActionAfterPassed.WaitUntilCondition:
                    isBlocked = true;
                    while (isBlocked)
                    {
                        if (isGoingBack) break;
                        yield return new WaitForEndOfFrame();
                    }
                    break;
            }
        }
    }

    public void ReverseMovement()
    {
        if(movementCoroutine is not null) StopCoroutine(movementCoroutine);
        movementFrame.Reverse();
        isBlocked = false;
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
                direction.x += movement.norm * distanceBetweenMovements;
            }
            else if (movement.direction == MovementDirection.Vertical)
            {
                direction.y += movement.norm * distanceBetweenMovements;
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
    public ActionAfterPassed actionAfterPassedBackward;
    [HideInInspector] public int maxNorm;
    [HideInInspector] public bool isPositive;

    public MovementVector(MovementDirection direction, int norm, ActionAfterPassed actionAfterPassed, ActionAfterPassed actionAfterPassedBackward)
    {
        this.direction = direction;
        this.norm = norm;
        this.actionAfterPassed = actionAfterPassed;
        this.actionAfterPassedBackward = actionAfterPassedBackward;
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
    Wait,
    WaitUntilCondition,
    GameOver
}
