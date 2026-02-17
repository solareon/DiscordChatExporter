using CliFx.Attributes;

namespace DiscordChatExporter.Cli.Commands;

[Command("purge", Description = "Deletes messages from one or multiple channels.")]
public class PurgeChannelsCommand : ExportChannelsCommand
{
    [CommandOption(
        "dry-run",
        Description = "Only count messages that would be deleted without deleting them."
    )]
    public bool IsDryRunEnabled { get; init; }

    protected override bool ShouldPurge => true;

    protected override bool IsDryRun => IsDryRunEnabled;
}
