using Microsoft.AspNetCore.SignalR;

namespace ClientResultsWithBlazor.Server.Hubs
{
    public class TurnByTurnHub : Hub
    {
        int _hostDieValue = 1;

        public async Task JoinTheGame(string username)
        {
            this.Groups.AddToGroupAsync()
        }

        public async Task StartTheGame()
        {
            _hostDieValue = Random.Shared.Next(1, 6);
            await WaitForResult(this.Context.ConnectionId);
        }

        public async Task WaitForResult(string connectionId)
        {
            var result = await Clients.Client(connectionId).InvokeAsync<int>("GetResult", "Roll the die!");
            if(_hostDieValue > result)
            {
                await Clients.Client(connectionId).SendAsync("EndResult", "You lose");
            }
            else if (_hostDieValue < result)
            {
                await Clients.Client(connectionId).SendAsync("EndResult", "You win");
            }
            else if(_hostDieValue == result)
            {
                await Clients.Client(connectionId).SendAsync("EndResult", "Tie");
            }
        }
    }
}
