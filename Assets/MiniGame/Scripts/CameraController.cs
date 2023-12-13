using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Movement followTransform;
    public Movement Movement => followTransform;

    private void LateUpdate()
    {
        transform.position = followTransform.transform.position;
    }
}