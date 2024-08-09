using Rage;
using System;

[assembly: Rage.Attributes.Plugin("StickyWheels", Author = "Khorio", Description = "Keeps the wheels turned", PrefersSingleInstance = true)]
namespace StickyWheels
{
    using System.Linq;

    public static class EntryPoint
    {
        private static Vehicle fLastVehicle
        {
            get
            {
                return Game.LocalPlayer.Character.LastVehicle;
            }
        }

        private static GameControl[] SteerControls = [GameControl.VehicleMoveLeft, GameControl.VehicleMoveRight, GameControl.VehicleMoveLeftOnly, GameControl.VehicleMoveRightOnly];

        public static void Main()
        {
            Game.DisplayNotification("~b~StickyWheels ~o~v0.2.1~w~ by ~b~Khorio");
            GameFiber.StartNew(() =>
            {
                float fLastAngle = 0f;
                while (true)
                {
                    GameFiber.Yield();
                    if (!fLastVehicle) continue;                        
                    if (fLastVehicle.Speed > 0f) continue;
                    if (SteerControls.Any(x => Game.IsControlPressed(0, x)))
                    {
                        if (Game.LocalPlayer.Character.IsInVehicle(fLastVehicle, false))
                        {
                            fLastAngle = fLastVehicle.SteeringAngle;
                        } 
                    }  
                    fLastVehicle.SteeringAngle = fLastAngle;
                }
            });
        }
    }
}
