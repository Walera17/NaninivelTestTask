using UnityEngine;

public class PatrolComponent : MonoBehaviour
{
    [SerializeField] private bool isRandom;
    [SerializeField] private ShootingPoint[] patrolPoints;
    int currentPatrolPointIndex = -1;

    private bool isValidatePatrolPoints;

    private void Awake()
    {
        isValidatePatrolPoints = CheckPatrolPoints();
    }

    public bool GetPatrolPoint(out ShootingPoint point)
    {
        if (!isValidatePatrolPoints)
        {
            point = null;
            return false;
        }

        if (isRandom)
        {
            ShootingPoint newPoints;
            do
            {
                newPoints = patrolPoints[Random.Range(0, patrolPoints.Length)];
            } while ((transform.position - newPoints.transform.position).sqrMagnitude < 1f);

            point = newPoints;
        }
        else
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
            point = patrolPoints[currentPatrolPointIndex];
        }

        return true;
    }

    bool CheckPatrolPoints()
    {
        if (patrolPoints.Length == 0) return false;

        foreach (ShootingPoint point in patrolPoints)
        {
            if (point == null)
                return false;
        }

        return true;
    }
}