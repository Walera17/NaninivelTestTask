using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class CameraArm : MonoBehaviour
{
    [SerializeField] private float armLenght;
    [SerializeField] private float speedCorrect = 5f;
    [SerializeField] private Transform child;
    [SerializeField] private CameraController cameraController;

    private Vector3 localPosition;

    private void Start()
    {
        localPosition = transform.localPosition;
        cameraController.Movement.CameraCorrect += Movement_CameraCorrect;
    }

    private void OnDestroy()
    {
        cameraController.Movement.CameraCorrect -= Movement_CameraCorrect;
    }

    private void Movement_CameraCorrect(float correct)
    {
        StartCoroutine(CorrectPositionCamera(correct));
    }

    private IEnumerator CorrectPositionCamera(float correct)
    {
        while (Mathf.Abs(transform.localPosition.z - correct) > 0.01f)
        {
            Vector3 target = localPosition + new Vector3(0, 0, correct);
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, speedCorrect * Time.deltaTime);
            yield return null;
        }

        transform.localPosition = new Vector3(localPosition.x, localPosition.y, correct);
    }

    private void Update()
    {
        child.position = transform.position - child.forward * armLenght;
    }
}