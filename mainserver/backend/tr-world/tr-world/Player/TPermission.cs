namespace trWorld.Player;

/// <summary>
///     Represents the different permission levels (roles) within the game.
/// </summary>
public enum TPermission
{
    /// <summary>
    ///     A regular player. Default permission level.
    /// </summary>
    Player = 0,

    /// <summary>
    ///     A moderator with basic administrative privileges (mute, kick, move vehicles).
    /// </summary>
    Moderator = 1,

    /// <summary>
    ///     An administrator with extended moderation powers, including temporary bans.
    /// </summary>
    Administrator = 2,

    /// <summary>
    ///     A developer with access to debugging and development tools (e.g., free spawning).
    /// </summary>
    Developer = 3,

    /// <summary>
    ///     The server owner with full control. Only God has more control.
    /// </summary>
    Owner = 4
}