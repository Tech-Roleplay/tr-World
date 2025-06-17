using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace trWorld.Player;

internal class TPlayerFactory : IEntityFactory<IPlayer>
{
    public IPlayer Create(ICore core, IntPtr entityPointer, uint id)
    {
        return new TPlayer(core, entityPointer, id);
    }
}