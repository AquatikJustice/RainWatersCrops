using Harmony;
using UnityEngine;
using System.Collections;
using System;

[HarmonyPatch(typeof(Weather_Rain))]
[HarmonyPatch("StartWeather")]
class Patch_WeatherRain_StartWeather
{
    public static void Postfix(Weather __instance)
    {
        try
        {
            int weather = ComponentManager<WeatherManager>.Value.GetCurrentWeatherIndex();
            RainWatersCrops.Log($"Current Weather Index: {weather}");
            if (weather == 4)
            {
                RainWatersCrops.Log("Setting isRaining to true...");
                RainWatersCrops.isRaining = true;
                RainWatersCrops.Log($"Weather Index: {weather} - It's raining.");
            }
            else
            {
                RainWatersCrops.isRaining = false;
                RainWatersCrops.Log($"Weather Index: {weather} - It's NOT raining.");
            }
        }
        catch(Exception e)
        {
            RainWatersCrops.Log($"ERROR: {e}");
        }
        
    }

}
