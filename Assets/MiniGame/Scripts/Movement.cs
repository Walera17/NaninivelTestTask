using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform bulletStartPosition;
    [SerializeField] private InputService inputService;
    [SerializeField] private PatrolComponent patrolComponent;
    [SerializeField] Animator animator;

    public event Action<float> CameraCorrect;

    private Vector3 bulletTarget;
    ShootingPoint targetShootingPoint;
    private bool isMove;
    private readonly int Run = Animator.StringToHash("run");
    private readonly int Mirror = Animator.StringToHash("mirror");
    private readonly int Shoot = Animator.StringToHash("shoot");

    void Start()
    {
        inputService.OnShoot += InputServiceOnShoot;
        NexWayPoint();
    }

    private void OnDestroy()
    {
        inputService.OnShoot -= InputServiceOnShoot;
    }

    private void InputServiceOnShoot(Vector3 point)
    {
        bulletTarget = point;
        animator.SetTrigger(Shoot);
    }

    public void AnimatorShoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletStartPosition.position, bulletStartPosition.rotation);
        bullet.SetupTarget(bulletTarget);
    }

    private void Update()
    {
        if (isMove)
            Move();
    }

    private void Move()
    {
        if ((targetShootingPoint.transform.position - transform.position).sqrMagnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetShootingPoint.transform.position, 7f * Time.deltaTime);
            Rotation();
        }
        else
            StartCoroutine(LocationShootingPoints());
    }

    private void Rotation()
    {
        Quaternion newRotation = Quaternion.LookRotation(targetShootingPoint.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 7 * Time.deltaTime);
    }

    private IEnumerator LocationShootingPoints()
    {
        isMove = false;
        animator.SetBool(Run, false);
        CameraCorrect?.Invoke(targetShootingPoint.CameraCorrect);
        targetShootingPoint.SpawnEnemies();
        yield return new WaitForSeconds(2f);
        while (EnemySpawner.enemiesList.Count > 0)
        {
            yield return null;
        }
        if (!targetShootingPoint.Finish)
            NexWayPoint();
        else
            inputService.ShowRestartPanel();
    }

    private void NexWayPoint()
    {
        if (patrolComponent.GetPatrolPoint(out targetShootingPoint))
        {
            animator.SetBool(Mirror, targetShootingPoint.CameraCorrect < 0);
            CameraCorrect?.Invoke(0);
            animator.SetBool(Run, true);
            isMove = true;
        }
    }
}