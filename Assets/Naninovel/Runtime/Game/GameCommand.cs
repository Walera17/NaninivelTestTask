using Naninovel.Runtime.Game.UI;

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

    [CommandAlias("showLog")]
    public class ShowLog : Command
    {
        public StringParameter Name;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            UIManager uiManager = Engine.GetService<UIManager>();
            QuestUI questUI = uiManager.GetUI<QuestUI>();
            questUI.ShowLog(Name.Value);
            return default;
        }
    }

    [CommandAlias("quitGame")]
    public class QuitGame : Command
    {
        public StringParameter Name;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            GameService gameService = Engine.GetService<GameService>();

            gameService.QuitGame();
            return default;
        }
    }
}