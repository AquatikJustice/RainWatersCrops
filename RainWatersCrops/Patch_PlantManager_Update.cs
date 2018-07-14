using Harmony;

[HarmonyPatch(typeof(PlantManager))]
[HarmonyPatch("Update")]
class Patch_Cropplot_Update
{
    public static void Postfix(PlantManager __instance)
    {
        if (ComponentManager<WeatherManager>.Value.GetCurrentWeatherIndex() == 4)
        {
            foreach (Cropplot cropplot in PlantManager.allCropplots)
            {
                foreach (PlantationSlot slot in cropplot.GetSlots())
                {
                    if (slot.busy && !slot.hasWater)
                    {
                        int r = UnityEngine.Random.Range(1, 1000001);

                        if (r > RainWatersCrops.rwcChance)
                        {
                            slot.AddWater(true);
                        }
                    }
                }
            }
        }
    }
}