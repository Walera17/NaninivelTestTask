using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator animator;

    private Vector3 startPosition;
    private bool visible,isDead;
    private static readonly int Hit = Animator.StringToHash("hit");

    private void Start()
    {
        startPosition = transform.position;
        MovementEnemy();
    }

    void MovementEnemy()
    {
        visible = !visible;
        StartCoroutine(Moving(GetRandomPosition(visible)));
    }

    private IEnumerator Moving(Vector3 targetPosition)
    {
        while ((targetPosition - transform.position).sqrMagnitude > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10f * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        float random = Random.Range(1.8f, 2.3f);
        yield return new WaitForSeconds(random);
        MovementEnemy();
    }

    Vector3 GetRandomPosition(bool visibleParam)
    {
        if (visibleParam)
            return Random.value > 0.5 ? startPosition + transform.right : startPosition - transform.right;

        return startPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet _) && !isDead)
        {
            animator.SetTrigger(Hit);
            isDead = true;
            Vector3 position = transform.position;
            AudioManager.instance.PlayDeathEnemy(position, 0.5f);
        }
    }

    public void AnimatorDestroyGameObject()
    {
        EnemySpawner.Remove(this);
        Destroy(gameObject);
    }
}