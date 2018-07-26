using Harmony;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

[ModTitle("RainWatersCrops")]
[ModDescription("Rain has a chance to water unwatered crops.")]
[ModAuthor("AquatikJustice")]
[ModVersion("1.1")]
[RaftVersion("Update 5")]

public class RainWatersCrops : Mod
{
    HarmonyInstance instance;
    private static readonly string modTitle = "RainWatersCrops";
    public static bool isRaining = false;
    private static string settingsPath;
    public static int rwcChance = 999950;

    private void Start()
    {
        instance = HarmonyInstance.Create("com.aquatikjustice.rainwaterscrops");
        instance.PatchAll(Assembly.GetExecutingAssembly());

        RConsole.registerCommand("rwcChance", "Let's you set the difficulty to High, Medium, Low", "rwcChance", new Action(SetChance));

        settingsPath = Directory.GetCurrentDirectory() + @"\mods\configs\RWC-Config.cfg";

        if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\mods\configs"))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\mods\configs");
        }

        if (!File.Exists(settingsPath))
        {
            using (var writer = new StreamWriter(settingsPath))
            {
                writer.Write(rwcChance);
            }
        }
        else
        {
            LoadSavedSettings();
        }
    }

    public void SetChance()
    {
        string[] commandArgs = RConsole.lastCommands.LastOrDefault().Split(' ');

        if (commandArgs.Count() > 1)
        {
            switch (commandArgs[1].ToLower())
            {
                case "high":
                    rwcChance = 999900;
                    SaveSetting(rwcChance);
                    Log("Chance of rain watering crops is set to HIGH.");
                    break;
                case "medium":
                    rwcChance = 999950;
                    SaveSetting(rwcChance);
                    Log("Chance of rain watering crops is set to MEDIUM.");
                    break;
                case "low":
                    rwcChance = 999990;
                    SaveSetting(rwcChance);
                    Log("Chance of rain watering crops is set to LOW");
                    break;
                case "custom":
                    if (commandArgs.Count() > 2)
                    {
                        bool isInt = int.TryParse(commandArgs[2], out rwcChance);
                        if (!isInt)
                        {
                            Log($"\"{commandArgs[2]}\" is not a number");
                        }
                        else
                        {
                            if (rwcChance < 1000000)
                            {
                                SaveSetting(rwcChance);
                            }
                            else
                            {
                                Log("ERROR: Please enter a number BELOW 1 million.");
                            }
                        }
                    }
                    else
                    {
                        Log("Please enter a number below 1 million to set it to a custom chance. Default is 999950.");
                    }
                    break;
                default:
                    Log("ERROR: Invalid choice. Please choose either High, Medium, or Low");
                    break;
            }
        }
    }

    public void SaveSetting(int chance)
    {
        try
        {
            if (File.Exists(settingsPath))
            {
                File.WriteAllText(settingsPath, String.Empty);
                File.WriteAllText(settingsPath, $"{rwcChance}");
            }
        }
        catch
        {
            Log($"ERROR: Settings unable to be saved to file {settingsPath}");
        }
    }

    public void LoadSavedSettings()
    {
        string settings = File.ReadAllText(settingsPath);
        Log(settings);
        if (settings != "")
        {
            rwcChance = Convert.ToInt32(settings);
        }
    }

    public static void Log(string text)
    {
        RConsole.Log($"<b><color=#ffcf01>{modTitle}</color>: <color=#129aab>{text}</color></b>");
    }
}