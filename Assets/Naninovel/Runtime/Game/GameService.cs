using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naninovel.Runtime.Game
{
    [InitializeAtRuntime]
    public class GameService : IEngineService
    {
        public readonly List<string> rewardItems = new();
        public string label;
        private string currentLevelName;

        public UniTask InitializeServiceAsync()
        {
            return UniTask.CompletedTask;
        }

        public void LoadGame(string nameScene)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Additive);
            if (asyncOperation == null)
            {
                Debug.LogError("Не удается загрузить уровень " + nameScene);
                return;
            }

            asyncOperation.completed += OnLoadOperationComplete;

            currentLevelName = nameScene;
        }

        private void OnLoadOperationComplete(AsyncOperation obj)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLevelName));
            DisableNaninovelCamera();
        }

        public void UnLoadGame()
        {
            EnableNaninovelCamera();
            ReturnToNaninovelInLabel();

            UnloadLevel(currentLevelName);
        }

        private void UnloadLevel(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName);

            asyncOperation.completed += OnUnloadOperationComplete;
        }

        private void OnUnloadOperationComplete(AsyncOperation obj)
        {
        }

        private static void DisableNaninovelCamera()
        {
            CameraManager cameraManager = Engine.GetService<CameraManager>();
            cameraManager.Camera.gameObject.SetActive(false);
        }

        private static void EnableNaninovelCamera()
        {
            CameraManager cameraManager = Engine.GetService<CameraManager>();
            cameraManager.Camera.gameObject.SetActive(true);
        }

        private void ReturnToNaninovelInLabel()
        {
            ScriptPlayer scriptPlayer = Engine.GetService<ScriptPlayer>();
            Script script = scriptPlayer.PlayedScript;
            int line = script.GetLineIndexForLabel(label);
            scriptPlayer.Play(script, line);
        }

        public void ResetService()
        {
            rewardItems.Clear();
        }

        public void DestroyService()
        {
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}