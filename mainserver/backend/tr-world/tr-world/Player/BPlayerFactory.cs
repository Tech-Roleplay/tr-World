using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace tr_world.Player;

internal class BPlayerFactory : IEntityFactory<IPlayer>
{
    public IPlayer Create(ICore core, IntPtr entityPointer, uint id)
    {
        return new BPlayer(core, entityPointer, id);
    }
}