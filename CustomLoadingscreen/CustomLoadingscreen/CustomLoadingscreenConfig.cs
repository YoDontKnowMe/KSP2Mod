using SpaceWarp.API.Configuration;
using Newtonsoft.Json;

namespace CustomLoadingscreen
{
    [JsonObject(MemberSerialization.OptOut)]
    [ModConfig]
    public class CustomLoadingscreenConfig
    {
         [ConfigField("pi")] [ConfigDefaultValue(3.14159)] public double pi;
    }
}