using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DiscordChatExporter.Core.Utils.Extensions;
using JsonExtensions.Reading;

namespace DiscordChatExporter.Core.Discord.Data.Components;

// https://docs.discord.com/developers/components/reference#what-is-a-component
public partial record MessageComponent(
    MessageComponentKind Kind,
    IReadOnlyList<MessageComponent> Components,
    ButtonComponent? Button
)
{
    public bool HasButtons => Button is not null || Components.Any(c => c.HasButtons);

    public IReadOnlyList<ButtonComponent> Buttons =>
        Components.Select(c => c.Button).WhereNotNull().ToArray();
}

public partial record MessageComponent
{
    public static MessageComponent? Parse(JsonElement json)
    {
        var rawType = json.GetPropertyOrNull("type")?.GetInt32OrNull();
        if (rawType is null)
            return null;

        var type = rawType.Value;
        if (!Enum.IsDefined(typeof(MessageComponentKind), type))
            return null;

        var kind = (MessageComponentKind)type;

        var components =
            json.GetPropertyOrNull("components")
                ?.EnumerateArrayOrNull()
                ?.Select(Parse)
                .WhereNotNull()
                .ToArray()
            ?? [];

        var button = kind == MessageComponentKind.Button ? ButtonComponent.Parse(json) : null;

        return new MessageComponent(kind, components, button);
    }
}
