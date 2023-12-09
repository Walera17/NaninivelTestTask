using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject contactPrefab;
    [SerializeField] private float speed = 25f;
    private Vector3 direction;

    public void SetupTarget(Vector3 bulletTarget)
    {
        direction = (bulletTarget - transform.position).normalized;
    }

    private void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(contactPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}