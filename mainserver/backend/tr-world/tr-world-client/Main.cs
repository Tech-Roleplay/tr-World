﻿using AltV.Net;
using AltV.Net.Client;
using AltV.Net.Client.Elements.Data;
using AltV.Net.Client.Elements.Entities;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Types;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Blip = AltV.Net.Client.Elements.Entities.Blip;
using Vehicle = AltV.Net.Client.Elements.Entities.Vehicle;
using static AltV.Net.Client.Natives;
using IBlip = AltV.Net.Client.Elements.Interfaces.IBlip;
using Timer = System.Timers.Timer;

namespace trWorld_client;

public class Main : AltV.Net.Client.Resource
{
    /// <summary>
    ///     Runs on startup of the resource
    /// </summary>
    public override void OnStart()
    {
        Console.WriteLine(AppContext.BaseDirectory);

        Alt.Log("Hello from Client");

        Alt.OnKeyDown += OnKeyDownHandler;
        Alt.OnKeyUp += OnKeyUpHandler;

        LoadBlips();

        
    }

    /// <summary>
    ///     Runs each tick, perfectly for events receiving
    /// </summary>
    public override void OnTick()
    {
        base.OnTick();
        Alt.OnServer<Vehicle, int>("SetPlayerIntoVehicle", SetPlayerIntoVeh);
        Alt.OnServer("adminmenu:Show", ShowAdminMenu);
        Alt.OnServer<string, int>("client:warn:showFullUI", ShowWarning);
        Alt.OnServer<string>("UpdateSpecificPlayer:UpdateJobBlip", LoadJobBlip);
        //Alt.OnServer<float, float, float, uint, uint, float, bool, string>("DEV:CreateBlip", CreatePersonalBilp);
        // Alt.OnServer<List<IBlip>>("LoadBilps", )
        Alt.OnServer("testremoveblip", testremoveblip);
        
        // time sync

        // Kamera auf First Person setzen (4)
        Alt.Natives.SetFollowPedCamViewMode(4);

        // Kamerawechsel mit V blockieren (INPUT_NEXT_CAMERA = 0)
        Alt.Natives.DisableControlAction(0, 0, true);

    }

    private static Timer _closeTimer;
    
    /// <summary>
    /// Zeigt eine Vollbild-Warnmeldung.
    /// </summary>
    /// <param name="message">Die Warnmeldung</param>
    /// <param name="timeout">Optionaler Timeout in Millisekunden (0 = bleibt offen)</param>
    public static void ShowWarning(string message, int timeout = 400)
    {
        // Native: SET_WARNING_MESSAGE(bool entryLine, char* message, char* p1, bool p2, int p3, char* p4, char* p5, bool background, int unk)
        Alt.EmitClient("native:invoke", "SET_WARNING_MESSAGE", true, message, "", false, -1, null, null, true, 0);

        if (timeout > 0)
        {
            _closeTimer?.Stop();
            _closeTimer = new System.Timers.Timer(timeout);
            _closeTimer.Elapsed += (_, _) => CloseWarning();
            _closeTimer.AutoReset = false;
            _closeTimer.Start();
        }
    }

    /// <summary>
    /// Schließt die Warnmeldung (wenn aktiv).
    /// </summary>
    private static void CloseWarning()
    {
        Alt.EmitClient("native:invoke", "REMOVE_WARNING_MESSAGE_LIST_ITEMS"); // Entfernt alle UI-Items
        Alt.EmitClient("native:invoke", "IS_WARNING_MESSAGE_ACTIVE");          // Check, ob sichtbar
        // Du kannst auch ein Dummy-Update forcieren, um es sofort zu schließen
    }
    
    private void testremoveblip()
    {
        blipsList.Clear();
    }

    /// <summary>
    ///     Runs on stopping of the resource
    /// </summary>
    public override void OnStop()
    {
        Alt.Log("Bye from Client");

        Alt.OnKeyDown -= OnKeyDownHandler;
        Alt.OnKeyUp -= OnKeyUpHandler;
        // Kameraeinstellungen
        Alt.Natives.SetFollowPedCamViewMode(4);
        Alt.Natives.DisableControlAction(0, 0, true);
    }
    
    private const string  DISCORD_APP_ID = "1329925312795902003";
    
    public static async Task GetOAuthToken()
    {
        try
        {
            string token = await Alt.Discord.RequestOAuth2Token(DISCORD_APP_ID);
            Alt.EmitServer("token", token);
        }
        catch (Exception e)
        {
            // Fehler können durch eine ungültige App-ID, Discord-Serverprobleme oder das Verweigern des Zugriffs durch den Benutzer verursacht werden.
            Alt.LogError(e.Message);
        }
    }

    private void SyncDateTime(int hour, int minute, int day, int month, int year)
    {
        Alt.Natives.SetClockTime(hour, minute, 0);
        Alt.Natives.SetClockDate(day, month, year);
    }
    
    private void LoadJobBlip(string JobName)
    {
        
    }
    
    private List<Blip> _activeBlips = new List<Blip>();
    private void OnUpdateBlips(List<float[]> blipData)
    {
        // Vorherige Blips entfernen
        foreach (var blip in _activeBlips)
        {
            _activeBlips.Remove(blip);
        }
            
        _activeBlips.Clear();

        // Neue Blips erstellen
        foreach (var data in blipData)
        {
            var newBlip = Alt.CreatePointBlip(new Position(data[0], data[1], data[2]));
            newBlip.Sprite = (uint)data[3];
            newBlip.Color = (uint)data[4];
            newBlip.ScaleXY = new Vector2(data[5], data[5]);
            _activeBlips.Add((Blip)newBlip);
        }
    }
    
    /// <summary>
    ///     Loads all Blips
    /// </summary>
    private void LoadBlips()
    {
        //CreatePublicBlip(-609.01495f, -599.91205f, 34.67566f, 835, 53, 0.7f, true, "Parlament");
        //CreatePublicBlip(-692.8296f, -636.38007f, 31.55694f, 855, 53, 0.7f, true, "Whitehouse");
        CreatePublicBlip(-609.01495f, -599.91205f, 34.67566f, 835, 53, 0.7f, true, "Parlament");
        CreatePublicBlip(-692.8296f, -636.38007f, 31.55694f, 855, 53, 0.7f, true, "Whitehouse");

        CreatePublicBlip(-1277.3447f, -560.0572f, 30.241001f, 764, 23, 0.7f, true, "LS Townhall");

        // IAA
        CreatePublicBlip(91.77598f, -669.6379f, 44.22036f, 788, 7, 0.7f, true, "IAA & SIU");
        CreatePublicBlip(99.99243f, -744.5883f, 45.754734f, 419, 7, 0.7f, true, "FIB");
        CreatePublicBlip(236.9338f, -407.2875f, 47.924366f, 419, 0, 0.7f, true, "Department of Justice - San Andreas");

        //DOJ
        CreatePublicBlip(433.15298f, -981.9341f, 30.710115f, 526, 29, 0.7f, true, "LSPD");
        CreatePublicBlip(359.48407f, -1583.4907f, 29.291954f, 526, 28, 0.7f, true, "LSSD");
        CreatePublicBlip(1858.5795f, 3677.6748f, 33.69357f, 526, 28, 0.7f, true, "BCSO");
        CreatePublicBlip(-1110.2585f, -846.3497f, 19.31684f, 526, 29, 0.7f, true, "LSPD");
        CreatePublicBlip(-1629.9904f, -1010.5518f, 13.101151f, 526, 29, 0.7f, true, "LSPD");
        CreatePublicBlip(-1311.5162f, -1530.3833f, 4.558398f, 526, 29, 0.7f, true, "LSPD");
        CreatePublicBlip(917.5098f, 3576.8838f, 33.56183f, 526, 26, 0.7f, true, "SNPD");
        CreatePublicBlip(-440.7782f, 6018.8945f, 31.492304f, 526, 3, 0.7f, true, "PBPD");
        CreatePublicBlip(641.17017f, 0.6331002f, 82.7864f, 526, 63, 0.7f, true, "SASP"); //Vinewood
        CreatePublicBlip(822.751f, -1289.806f, 28.240631f, 526, 39, 0.7f, true, "SAHP");
        CreatePublicBlip(1852.4473f, 2585.813f, 45.672016f, 285, 62, 0.7f, true, "SA Prison");
        CreatePublicBlip(376.33688f, 779.18384f, 185.17918f, 526, 52, 0.7f, true, "SAPR");
        CreatePublicBlip(-560.2198f, -135.14146f, 38.152267f, 526, 29, 0.7f, true, "LSPD");

        // Fire Department
        CreatePublicBlip(-379.78036f, 6118.821f, 31.210928f, 648, 1, 0.7f, true, "Fire Station");
        CreatePublicBlip(1697.5897f, 3586.9915f, 35.089474f, 648, 1, 0.7f, true, "Fire Station");
        CreatePublicBlip(213.33661f, -1645.3972f, 29.803219f, 648, 1, 0.7f, true, "Fire Station");
        CreatePublicBlip(1202.7572f, -1463.9777f, 34.876827f, 648, 1, 0.7f, true, "Fire Station");
        CreatePublicBlip(-1035.6283f, -2383.6064f, 14.094898f, 648, 1, 0.7f, true, "Fire Station");
        CreatePublicBlip(-661.1193f, -75.9205f, 38.591236f, 648, 1, 0.7f, true, "Fire Station");

        // EMS
        CreatePublicBlip(299.1843f, -584.83685f, 43.260838f, 61, 11, 0.7f, true, "Hospital");
        CreatePublicBlip(343.9636f, -1398.7815f, 32.509254f, 61, 11, 0.7f, true, "Hospital");
        CreatePublicBlip(-449.8421f, -341.88446f, 33.979736f, 61, 11, 0.7f, true, "Hospital");
        CreatePublicBlip(1839.3762f, 3672.352f, 33.754364f, 61, 11, 0.7f, true, "Hospital");
        CreatePublicBlip(-248.05325f, 6331.792f, 31.904264f, 61, 11, 0.7f, true, "Hospital");
        CreatePublicBlip(1151.2819f, -1529.2511f, 35.36556f, 61, 11, 0.7f, true, "Hospital");

        //Bus
        CreatePublicBlip(454.01633f, -658.4551f, 27.866255f, 513, 47, 0.7f, true, "Bus Station");

        // MEtro
        CreatePublicBlip(-1084.2657f, -2719.5398f, -7.410132f, 607, 49, 0.6f, true, "Metro LSIA Terminal");
        CreatePublicBlip(-883.624f, -2314.9814f, -11.732785f, 607, 49, 0.6f, true, "Metro LSIA Parking");
        CreatePublicBlip(-539.3826f, -1283.5278f, 26.901606f, 607, 49, 0.6f, true, "Metro Puerto del Sol");
        CreatePublicBlip(278.6903f, -1201.9535f, 38.896255f, 607, 49, 0.6f, true, "Metro Strawberry");
        CreatePublicBlip(-292.98038f, -332.386f, 10.063103f, 607, 49, 0.6f, true, "Metro Burton");
        CreatePublicBlip(-815.5151f, -135.68059f, 19.950294f, 607, 49, 0.6f, true, "Metro Portola Drive");
        CreatePublicBlip(-1353.9574f, -464.19272f, 15.04531f, 607, 49, 0.6f, true, "Metro Del Perro");
        CreatePublicBlip(-503.02167f, -674.93274f, 11.808959f, 607, 49, 0.6f, true, "Metro Little Seoul");
        CreatePublicBlip(-212.7016f, -1035.0039f, 30.139433f, 607, 49, 0.6f, true, "Metro Pillbox South");
        CreatePublicBlip(115.049774f, -1727.3262f, 30.110792f, 607, 49, 0.6f, true, "Metro Davis");

        //RecyleJob
        CreatePublicBlip(115.049774f, -1727.3262f, 30.110792f, 607, 49, 0.6f, true, "Metro Davis");

        // AmmoNation

        //LS Coustomes
        CreatePublicBlip(-342.38242f, -136.14066f, 38.3114f, 72, 4, 0.6f, true, "LS Customs");
        CreatePublicBlip(732.34283f, -1085.4066f, 21.461548f, 72, 4, 0.6f, true, "LS Customs");
        CreatePublicBlip(-1154.7296f, -2006.3077f, 12.480591f, 72, 4, 0.6f, true, "LS Customs");
        CreatePublicBlip(110.4f, 6626.0176f, 31.082764f, 72, 4, 0.6f, true, "Beekers Garage & Parts");
        CreatePublicBlip(1174.8264f, 2640.844f, 37.064453f, 72, 4, 0.6f, true, "Auto Repairs");

        // PDM
        CreatePublicBlip(-38.795605f, -1109.6835f, 26.432251f, 369, 13, 0.6f, true, "Premium Deluxe Motorsport");
    }

    private List<IBlip> blipsList = new List<IBlip>();
    
    /// <summary>
    ///      Creates a player Blip entity for all players
    /// </summary>
    /// <param name="x">The x-axis</param>
    /// <param name="y">The y-axis</param>
    /// <param name="z">The z-axis</param>
    /// <param name="sprite">The sprite-id</param>
    /// <param name="color">The color-id</param>
    /// <param name="scale">The floating scale</param>
    /// <param name="shortRange">Is it only nearby, or globally accessible</param>
    /// <param name="name">The name of the blip</param>
    public void CreatePersonalBlip(float x, float y, float z, uint sprite, uint color, float scale = 1.0f,
        bool shortRange = false, string name = "")
    {
        Alt.Log("Test");
        var tempblips = Alt.CreatePointBlip(new Position(x,y,z));
        tempblips.Sprite = sprite;
        tempblips.Color = color;
        tempblips.ScaleXY = new Vector2(scale, scale);
        tempblips.ShortRange = shortRange;
        if (name.Length > 0) tempblips.Name = name;
        Alt.Log("Test");
        //blipsList.Add(tempblips);
    }
    
    /// <summary>
    ///     Creates a public Blip entity for all players
    /// </summary>
    /// <param name="x">The x-axis</param>
    /// <param name="y">The y-axis</param>
    /// <param name="z">The z-axis</param>
    /// <param name="sprite">The sprite-id</param>
    /// <param name="color">The color-id</param>
    /// <param name="scale">The floating scale</param>
    /// <param name="shortRange">Is it only nearby, or globally accessible</param>
    /// <param name="name">The name of the blip</param>
    private void CreatePublicBlip(float x, float y, float z, uint sprite, uint color, float scale = 1.0f,
        bool shortRange = false, string name = "")
    {
        var tempblips = Alt.CreatePointBlip(new Position(x, y, z));
        tempblips.Sprite = sprite;
        tempblips.Color = color;
        tempblips.ScaleXY = new Vector2(scale, scale);
        tempblips.ShortRange = shortRange;
        if (name.Length > 0) tempblips.Name = name;

        blipsList.Add((IBlip)tempblips);
    }

    /// <summary>
    ///     Sets the player into the vehicle
    /// </summary>
    /// <param name="vehicle">The vehicle entity</param>
    /// <param name="seat">the number of seat - in the future it will be an enum </param>
    private void SetPlayerIntoVeh(Vehicle vehicle, int seat)
    {
        if (vehicle == null) return;

        Alt.Natives.SetPedIntoVehicle(Alt.LocalPlayer, vehicle, -1);
        
        
        
    }

    /// <summary>
    ///     Create a function call based on Keypress. At the moment is the release of the key
    /// </summary>
    /// <param name="key">The called Key</param>
    private void OnKeyUpHandler(Key key)
    {
        switch (key)
        {
            case Key.Modifiers:
                break;
            case Key.None:
                break;
            case Key.LButton:
                break;
            case Key.RButton:
                break;
            case Key.Cancel:
                break;
            case Key.MButton:
                break;
            case Key.XButton1:
                break;
            case Key.XButton2:
                break;
            case Key.Back:
                break;
            case Key.Tab:
                break;
            case Key.LineFeed:
                break;
            case Key.Clear:
                break;
            case Key.Enter:
                break;
            case Key.ShiftKey:
                break;
            case Key.ControlKey:
                break;
            case Key.Menu:
                break;
            case Key.Pause:
                break;
            case Key.CapsLock:
                break;
            case Key.KanaMode:
                break;
            case Key.JunjaMode:
                break;
            case Key.FinalMode:
                break;
            case Key.KanjiMode:
                break;
            case Key.Escape:
                break;
            case Key.IMEConvert:
                break;
            case Key.IMENonconvert:
                break;
            case Key.IMEAceept:
                break;
            case Key.IMEModeChange:
                break;
            case Key.Space:
                break;
            case Key.Prior:
                break;
            case Key.Next:
                break;
            case Key.End:
                break;
            case Key.Home:
                break;
            case Key.Left:
                break;
            case Key.Up:
                break;
            case Key.Right:
                break;
            case Key.Down:
                break;
            case Key.Select:
                break;
            case Key.Print:
                break;
            case Key.Execute:
                break;
            case Key.PrintScreen:
                break;
            case Key.Insert:
                break;
            case Key.Delete:
                break;
            case Key.Help:
                break;
            case Key.D0:
                break;
            case Key.D1:
                break;
            case Key.D2:
                break;
            case Key.D3:
                break;
            case Key.D4:
                break;
            case Key.D5:
                break;
            case Key.D6:
                break;
            case Key.D7:
                break;
            case Key.D8:
                break;
            case Key.D9:
                break;
            case Key.A:
                break;
            case Key.B:
                break;
            case Key.C:
                break;
            case Key.D:
                break;
            case Key.E:
                break;
            case Key.F:
                break;
            case Key.G:
                break;
            case Key.H:
                break;
            case Key.I:
                break;
            case Key.J:
                break;
            case Key.K:
                break;
            case Key.L:
                break;
            case Key.M:
                break;
            case Key.N:
                break;
            case Key.O:
                break;
            case Key.P:
                break;
            case Key.Q:
                break;
            case Key.R:
                break;
            case Key.S:
                break;
            case Key.T:
                break;
            case Key.U:
                break;
            case Key.V:
                break;
            case Key.W:
                break;
            case Key.X:
                break;
            case Key.Y:
                break;
            case Key.Z:
                break;
            case Key.LWin:
                break;
            case Key.RWin:
                break;
            case Key.Apps:
                break;
            case Key.Sleep:
                break;
            case Key.NumPad0:
                break;
            case Key.NumPad1:
                break;
            case Key.NumPad2:
                break;
            case Key.NumPad3:
                break;
            case Key.NumPad4:
                break;
            case Key.NumPad5:
                break;
            case Key.NumPad6:
                break;
            case Key.NumPad7:
                break;
            case Key.NumPad8:
                break;
            case Key.NumPad9:
                break;
            case Key.Multiply:
                break;
            case Key.Add:
                break;
            case Key.Separator:
                break;
            case Key.Subtract:
                break;
            case Key.Decimal:
                break;
            case Key.Divide:
                break;
            case Key.F1:
                break;
            case Key.F2:
                break;
            case Key.F3:
                break;
            case Key.F4:
                break;
            case Key.F5:
                break;
            case Key.F6:
                break;
            case Key.F7:
                break;
            case Key.F8:
                break;
            case Key.F9:
                break;
            case Key.F10:
                break;
            case Key.F11:
                break;
            case Key.F12:
                break;
            case Key.F13:
                break;
            case Key.F14:
                break;
            case Key.F15:
                break;
            case Key.F16:
                break;
            case Key.F17:
                break;
            case Key.F18:
                break;
            case Key.F19:
                break;
            case Key.F20:
                break;
            case Key.F21:
                break;
            case Key.F22:
                break;
            case Key.F23:
                break;
            case Key.F24:
                break;
            case Key.NumLock:
                break;
            case Key.Scroll:
                break;
            case Key.LShiftKey:
                break;
            case Key.RShiftKey:
                break;
            case Key.LControlKey:
                break;
            case Key.RControlKey:
                break;
            case Key.LMenu:
                break;
            case Key.RMenu:
                break;
            case Key.BrowserBack:
                break;
            case Key.BrowserForward:
                break;
            case Key.BrowserRefresh:
                break;
            case Key.BrowserStop:
                break;
            case Key.BrowserSearch:
                break;
            case Key.BrowserFavorites:
                break;
            case Key.BrowserHome:
                break;
            case Key.VolumeMute:
                break;
            case Key.VolumeDown:
                break;
            case Key.VolumeUp:
                break;
            case Key.MediaNextTrack:
                break;
            case Key.MediaPreviousTrack:
                break;
            case Key.MediaStop:
                break;
            case Key.MediaPlayPause:
                break;
            case Key.LaunchMail:
                break;
            case Key.SelectMedia:
                break;
            case Key.LaunchApplication1:
                break;
            case Key.LaunchApplication2:
                break;
            case Key.Oem1:
                break;
            case Key.Oemplus:
                break;
            case Key.Oemcomma:
                break;
            case Key.OemMinus:
                break;
            case Key.OemPeriod:
                break;
            case Key.OemQuestion:
                break;
            case Key.Oemtilde:
                break;
            case Key.Oem4:
                break;
            case Key.OemPipe:
                break;
            case Key.Oem6:
                break;
            case Key.Oem7:
                break;
            case Key.Oem8:
                break;
            case Key.Oem102:
                break;
            case Key.ProcessKey:
                break;
            case Key.Packet:
                break;
            case Key.Attn:
                break;
            case Key.Crsel:
                break;
            case Key.Exsel:
                break;
            case Key.EraseEof:
                break;
            case Key.Play:
                break;
            case Key.Zoom:
                break;
            case Key.NoName:
                break;
            case Key.Pa1:
                break;
            case Key.OemClear:
                break;
            case Key.KeyCode:
                break;
            case Key.Shift:
                break;
            case Key.Control:
                break;
            case Key.Alt:
                break;
        }
    }

    /// <summary>
    ///     Create a function call based on Keypress. At the moment is the pressing of the key
    /// </summary>
    /// <param name="key">The called Key</param>
    private void OnKeyDownHandler(Key key)
    {
        //
        //Alt.Log("Not Implemented");
    }
    
    private IWebView _adminMenuWebView;
    private bool _isAdminMenuOpen = false;

    
    #region AdminMenu

    private void ShowAdminMenu()
    {
        if (!_isAdminMenuOpen)
        {
            _adminMenuWebView = Alt.CreateWebView("http://resource/client/adminmenu/adminmenu.html");
            _adminMenuWebView.On("adminmenu:Close:Webview", CloseWebview);
            _isAdminMenuOpen = true;
        }

        _adminMenuWebView.Focus();
        Alt.ShowCursor(true);
    }

    private void CloseWebview()
    {
        if (_adminMenuWebView != null)
        {
            _adminMenuWebView.Unfocus();
            _adminMenuWebView.Destroy();
            _adminMenuWebView = null;
            _isAdminMenuOpen = false;

            foreach (var player in Alt.GetAllPlayers())
            {
                Alt.ShowCursor(false);
            }
        }
    }

    #endregion
}