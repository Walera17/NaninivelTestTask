using Naninovel.UI;
using UnityEngine;

namespace Naninovel.Runtime.Game
{
    [CommandAlias("startGame")]
    public class GameCommand : Command
    {
        public StringParameter Name;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            GameService gameService = Engine.GetService<GameService>();

            gameService.LoadGame(Name.Value);
            return default;
        }
    }

    [CommandAlias("setLabel")]
    public class SetLabel : Command
    {
        public StringParameter Name;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            GameService gameService = Engine.GetService<GameService>();
            gameService.label = Name.Value;
            return default;
        }
    }

    [CommandAlias("disableBlockRayCast")]
    public class DisableBlockRayCast : Command
    {
        public StringParameter Name;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            UIManager uiManager = Engine.GetService<UIManager>();
            uiManager.GetUI<ContinueInputUI>().GetComponent<CanvasGroup>().blocksRaycasts = false;
            return default;
        }
    }

    [CommandAlias("enableBlockRayCast")]
    public class EnableBlockRayCast : Command
    {
        public StringParameter Name;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            UIManager uiManager = Engine.GetService<UIManager>();
            uiManager.GetUI<ContinueInputUI>().GetComponent<CanvasGroup>().blocksRaycasts = true;
            return default;
        }
    }
}