using Harmony;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

[ModTitle("RainWatersCrops")]
[ModDescription("Rain has a chance to water unwatered crops.")]
[ModAuthor("AquatikJustice")]
[ModVersion("1.0")]
[RaftVersion("1.03")]

public class RainWatersCrops : Mod
{
    HarmonyInstance instance;

    private void Start()
    {
        instance = HarmonyInstance.Create("com.aquatikjustice.RainWatersCrops");
        instance.PatchAll(Assembly.GetExecutingAssembly());

        //RConsole.registerCommand("Weather", "Get the current weather.", "Weather", new Action(GetWeather));
    }

    public void GetWeather()
    {
        switch (ComponentManager<WeatherManager>.Value.GetCurrentWeatherIndex())
        {
            case 0:
                Log($"Current weather is None.");
                break;
            case 1:
                Log($"Current weather is Calm.");
                break;
            case 2:
                Log($"Current weather is Fog.");
                break;
            case 3:
                Log($"Current weather is Big Waves.");
                break;
            case 4:
                Log($"Current weather is Rain.");
                break;
        }
    }

    private static readonly string modTitle = "RainWatersCrops";
    public static bool isRaining = false;

    public static void Log(string text)
    {
        //RConsole.Log($"<b><color=#ffcf01>{modTitle}</color>: <color=#129aab>{text}</color></b>");
    }
}