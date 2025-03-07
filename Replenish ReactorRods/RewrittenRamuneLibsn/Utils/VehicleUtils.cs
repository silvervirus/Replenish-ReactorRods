﻿

using System;
using System.Collections;
using Nautilus.Extensions;
using UnityEngine;
using UWE;

namespace RamuneLib.Utils
{
    public static class VehicleUtils
    {
        public enum SpeedType
        {
            All,
            ForwardForce,
            BackwardForce,
            SidewardForce,
            VerticalForce,
        }


        /// <summary>
        /// Retrieves an array of speed values for the Seaglide.
        /// </summary>
        /// <returns>An array containing the Seaglide speed values in the following order: forwardMaxSpeed, backwardMaxSpeed, strafeMaxSpeed, verticalMaxSpeed, waterAcceleration, swimDrag.</returns>
        public static float[] SeaglideSpeeds()
        {
            float[] values = null;

            values.Add(Player.main.playerController.seaglideForwardMaxSpeed);
            values.Add(Player.main.playerController.seaglideBackwardMaxSpeed);
            values.Add(Player.main.playerController.seaglideStrafeMaxSpeed);
            values.Add(Player.main.playerController.seaglideVerticalMaxSpeed);
            values.Add(Player.main.playerController.seaglideWaterAcceleration);
            values.Add(Player.main.playerController.seaglideSwimDrag);

            return values;
        }


        /// <summary>
        /// Retrieves an array of speed values on the provided vehicle component.
        /// </summary>
        /// <returns>An array containing the values on the provided vehicle component in the following order: forwardForce, backwardForce, sidewardForce, verticalForce.</returns>
        public static float[] SpeedValues(this Vehicle vehicle)
        {
            float[] values = null;

            values.Add(vehicle.forwardForce);
            values.Add(vehicle.backwardForce);
            values.Add(vehicle.sidewardForce);
            values.Add(vehicle.verticalForce);

            return values;
        }


        private static IEnumerator SpeedupAsync(this Vehicle vehicle, SpeedType speedType, float multiplier, float duration = 0, Action onIncrease = null, Action onDecrease = null)
        {
            float[] originals = vehicle.SpeedValues();

            switch(speedType)
            {
                case SpeedType.All:
                    vehicle.forwardForce *= multiplier;
                    vehicle.backwardForce *= multiplier;
                    vehicle.sidewardForce *= multiplier;
                    vehicle.verticalForce *= multiplier;
                    break;

                case SpeedType.ForwardForce:
                    vehicle.forwardForce *= multiplier;
                    break;

                case SpeedType.BackwardForce:
                    vehicle.backwardForce *= multiplier;
                    break;

                case SpeedType.SidewardForce:
                    vehicle.sidewardForce *= multiplier;
                    break;

                case SpeedType.VerticalForce:
                    vehicle.verticalForce *= multiplier;
                    break;
            }

            onIncrease?.Invoke();

            if(duration <= 0)
                yield break;

            yield return new WaitForSeconds(duration);

            switch(speedType)
            {
                case SpeedType.All:
                    vehicle.forwardForce = originals[0];
                    vehicle.backwardForce = originals[1];
                    vehicle.sidewardForce = originals[2];
                    vehicle.verticalForce = originals[3];
                    break;

                case SpeedType.ForwardForce:
                    vehicle.forwardForce = originals[0];
                    break;

                case SpeedType.BackwardForce:
                    vehicle.backwardForce = originals[1];
                    break;

                case SpeedType.SidewardForce:
                    vehicle.sidewardForce = originals[2];
                    break;

                case SpeedType.VerticalForce:
                    vehicle.verticalForce = originals[3];
                    break;
            }

            onIncrease?.Invoke();
        }


        public static void Speedup(this Vehicle vehicle, SpeedType speedType, float multiplier, float duration = 0, Action onIncrease = null, Action onDecrease = null)
        {
            CoroutineHost.StartCoroutine(vehicle.SpeedupAsync(speedType, multiplier, duration, onIncrease, onDecrease));
        }
    }
}