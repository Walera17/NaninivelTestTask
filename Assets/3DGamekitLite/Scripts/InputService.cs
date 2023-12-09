using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InputService : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject restartPanel;
    public event Action<Vector3> OnShoot;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Physics.Raycast(cam.ScreenPointToRay(eventData.position), out RaycastHit hitInfo))
            OnShoot?.Invoke(hitInfo.point);
    }

    public void ShowRestartPanel()
    {
        restartPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}