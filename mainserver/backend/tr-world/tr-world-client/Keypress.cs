/*using System;
using AltV.Net;
using AltV.Net.Client;
using AltV.Net.Client.Elements.Data;

namespace trWorld_client;

[AttributeUsage(AttributeTargets.Method)]
public class Keypress : Attribute
{
    public Key Key { get; }
    public bool RequireCtrl { get; }
    public bool RequireShift { get; }
    public Keypress(Key key, bool requireCtrl = false, bool requireShift= false)
    {
        Key = key;
        RequireCtrl = requireCtrl;
        RequireShift = requireShift;

    }
}

public class KeypressLogic : IScript
{
    public KeypressLogic()
    {
        // Abonniere das KeyPress-Event
        Alt.OnKeyDown();
    }

    private void HandleKeyPress(string pressedKey)
    {
        // Hier kommt deine Logik rein
        // Überprüfe den gedrückten Key und führe entsprechende Aktionen aus
        // Nutze hier deine AttributeClass, um zusätzliche Informationen zu verarbeiten

        // Beispiel:
        if (pressedKey == "F1")
        {
            // Rufe eine Methode in deiner AttributeClass auf
            YourAttributeClass attributeClass = new YourAttributeClass();
            attributeClass.DoSomethingWithKey("F1");
        }
    }
}*/

