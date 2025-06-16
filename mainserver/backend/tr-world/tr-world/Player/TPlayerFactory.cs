using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace tr_world.Player;

internal class TPlayerFactory : IEntityFactory<IPlayer>
{
    public IPlayer Create(ICore core, IntPtr entityPointer, uint id)
    {
        return new TPlayer(core, entityPointer, id);
    }
}