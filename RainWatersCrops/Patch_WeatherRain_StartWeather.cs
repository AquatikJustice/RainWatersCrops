using Harmony;
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
            if (weather == 4)
            {
                RainWatersCrops.isRaining = true;
            }
            else
            {
                RainWatersCrops.isRaining = false;
            }
        }
        catch(Exception e)
        {
            RainWatersCrops.Log($"ERROR: {e}");
        }
        
    }

}
