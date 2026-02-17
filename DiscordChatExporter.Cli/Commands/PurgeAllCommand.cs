using CliFx.Attributes;

namespace DiscordChatExporter.Cli.Commands;

[Command("purgeall", Description = "Deletes messages from all accessible channels.")]
public class PurgeAllCommand : ExportAllCommand
{
    [CommandOption(
        "dry-run",
        Description = "Only count messages that would be deleted without deleting them."
    )]
    public bool IsDryRunEnabled { get; init; }

    protected override bool ShouldPurge => true;

    protected override bool IsDryRun => IsDryRunEnabled;
}
