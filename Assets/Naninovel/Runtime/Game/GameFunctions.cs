namespace Naninovel.Runtime.Game
{
    [ExpressionFunctions]
    public static class GameFunctions
    {
        public static bool RewardExist(string itemId)
        {
            var gameService = Engine.GetService<GameService>();
            return gameService.rewardItems.Contains(itemId);
        }
    }
}