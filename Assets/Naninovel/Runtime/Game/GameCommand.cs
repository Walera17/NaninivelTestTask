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
}