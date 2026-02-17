using CliFx.Attributes;

namespace DiscordChatExporter.Cli.Commands;

[Command("purgedm", Description = "Deletes messages from all direct message channels.")]
public class PurgeDirectMessagesCommand : ExportDirectMessagesCommand
{
    [CommandOption(
        "dry-run",
        Description = "Only count messages that would be deleted without deleting them."
    )]
    public bool IsDryRunEnabled { get; init; }

    protected override bool ShouldPurge => true;

    protected override bool IsDryRun => IsDryRunEnabled;
}
