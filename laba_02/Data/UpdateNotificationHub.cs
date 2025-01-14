

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[IgnoreAntiforgeryToken]
public class UpdateNotificationHub : Hub
{
    // Notify clients to refresh their data
public async Task ReceiveUpdate(string message)
    {
        try
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }
        catch (Exception ex)
        {
            // Логирование ошибок
            Console.WriteLine($"Ошибка отправки обновления: {ex.Message}");
        }
    }
}
