﻿using Discord;
using Discord.Webhook;

namespace Enshrouded_Server_Manager.Services;

public class DiscordOutput
{
    // send server is online status to webhook
    public async Task ServerOnline(string serverName, string Url)
    {
        // The webhook url follows the format https://discord.com/api/webhooks/{id}/{token}
        using var client = new DiscordWebhookClient(Url);

        var embed = new EmbedBuilder
        {
            Title = $"ESM - {serverName} : Online",
            Description = "",
            Color = Discord.Color.Green
        };

        await client.SendMessageAsync(text: "", embeds: new[] { embed.Build() });
    }

    // send server is offline status to webhook
    public async Task ServerOffline(string serverName, string Url)
    {
        // The webhook url follows the format https://discord.com/api/webhooks/{id}/{token}
        using var client = new DiscordWebhookClient(Url);

        var embed = new EmbedBuilder
        {
            Title = $"ESM - {serverName} : Offline",
            Description = "",
            Color = Discord.Color.Red
        };

        await client.SendMessageAsync(text: "", embeds: new[] { embed.Build() });
    }

    // send server is updating status to webhook
    public async Task ServerUpdating(string serverName, string Url)
    {
        // The webhook url follows the format https://discord.com/api/webhooks/{id}/{token}
        using var client = new DiscordWebhookClient(Url);

        var embed = new EmbedBuilder
        {
            Title = $"ESM - {serverName} : Updating...",
            Description = "",
            Color = Discord.Color.Gold
        };


        await client.SendMessageAsync(text: "", embeds: new[] { embed.Build() });
    }

    // send server backup has been created status to webhook
    public async Task ServerBackup(string serverName, string Url)
    {
        // The webhook url follows the format https://discord.com/api/webhooks/{id}/{token}
        using var client = new DiscordWebhookClient(Url);

        var embed = new EmbedBuilder
        {
            Title = $"ESM - {serverName} : Backup created",
            Description = "",
            Color = Discord.Color.Blue
        };

        await client.SendMessageAsync(text: "", embeds: new[] { embed.Build() });
    }
}