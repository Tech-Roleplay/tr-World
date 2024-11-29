using AltV.Net.Client;
using AltV.Net.Client.Elements.Data;
using AltV.Net.Client.Elements.Entities;
using System;

namespace tr_world_client
{
    public class Main : Resource
    {
        public override void OnStart()
        {
            Alt.Log("Hello from Client");

            Alt.OnKeyDown += OnKeyDownHandler;
            Alt.OnKeyUp += OnKeyUpHandler;

            LoadBlips();
        }

        public override void OnTick()
        {
            base.OnTick();
            Alt.OnServer<Vehicle, int>("SetPlayerIntoVehicle", SetPlayerIntoVeh);
        }

        public override void OnStop()
        {
            Alt.Log("Bye from Client");

            Alt.OnKeyDown -= OnKeyDownHandler;
            Alt.OnKeyUp -= OnKeyUpHandler;
        }

        private void LoadBlips()
        {
            createPublicBlip(-609.01495f, -599.91205f, 34.67566f, 835, 53, 0.7f, true, "Parlament");
            createPublicBlip(-692.8296f, -636.38007f, 31.55694f, 855, 53, 0.7f, true, "Whitehouse");
        }

        public static void createPublicBlip(float x, float y, float z, uint sprite, uint color, float scale = 1.0f, bool shortRange = false, string name = "")
        {
            var tempblips = Alt.CreatePointBlip(new AltV.Net.Data.Position(x, y, z));
            tempblips.Sprite = sprite;
            tempblips.Color = color;
            tempblips.ScaleXY = new System.Numerics.Vector2((float)scale, (float)scale);
            tempblips.ShortRange = shortRange;
            if (name.Length > 0)
            {
                tempblips.Name = name;
            }
        }

        private void SetPlayerIntoVeh(Vehicle vehicle, int seat)
        {
            if (vehicle == null) return;

            Alt.Natives.SetPedIntoVehicle(Alt.LocalPlayer, vehicle, -1);
        }

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
                default:
                    break;
            }
        }

        private void OnKeyDownHandler(Key key)
        {
            //
            Alt.Log("Not Implemented");
        }

        
    }
}
