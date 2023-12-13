using Naninovel;
using Naninovel.Runtime.Game;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputService : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool isTesting;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private string itemReward;
    public event Action<Vector3> OnShoot;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        if (isTesting)
            ShowRestartPanel();
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

    public void GetReward()
    {
        var gameService = Engine.GetService<GameService>();
        gameService.rewardItems.Add(itemReward);

        gameService.UnLoadGame();
    }

    public void RestartGame()
    {
        var gameService = Engine.GetService<GameService>();

        gameService.UnLoadGame();

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}