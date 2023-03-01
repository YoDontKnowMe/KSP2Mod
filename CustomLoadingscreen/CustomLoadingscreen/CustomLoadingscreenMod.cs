using SpaceWarp.API.Mods;
using KSP.ScriptInterop;
using KSP.Modding;
using KSP.Game;
using UnityEngine;
using KSP.IO;
using KSP.ScriptInterop.impl.moonsharp;
using MoonSharp.Interpreter;

namespace CustomLoadingscreen
{
    [MainMod]
     public class CustomLoadingscreenMod : SpaceWarp.API.Mods.Mod
    {
        private static char separatorCharacter = IOProvider.DirectorySeparatorCharacter;
        private static string str = KSPUtil.ApplicationRootPath + separatorCharacter.ToString() + "GameData" + separatorCharacter.ToString() + "Mods" + separatorCharacter.ToString() + "TestMod";
        private static Version modVersion = new Version(0, 0, 1, 1);
        public string objectName = "Cheats";
        private DynValue dynVal;
        private List<DynValue> globals;

        private IScriptEnvironment ScriptEnvironment;
        private IScriptInterop scriptInterop;
        private Dictionary<string, string> dict1;
        private static KSP2ModManager modManager = GameManager.Instance.Game.KSP2ModManager;
        CheatSystem cheat = GameManager.Instance.Game.CheatSystem;
        KSP2LuaModCore luaModCore = new KSP2LuaModCore(modVersion, "TestMod", "update.lua", str);
        IVersionedAPI api = modManager.GetAPI(modVersion);
        Script script = new Script();

        public override void OnInitialized(){
            luaModCore.ModStart();
            KSP2ModManager.LogModMessage("Okey the mod starts at least-");
            this.scriptInterop = (IScriptInterop) new ScriptInteroperability(new TypeInterop());
            this.ScriptEnvironment = this.scriptInterop.RootEnvironment;
            api.RegisterEnvironment(this.ScriptEnvironment);
            KSP2ModManager.LogModMessage("LOADED SCRIPTENVIRONMONT");
            
            this.dict1 = this.ScriptEnvironment.GetTypes();
            this.dynVal = script.Globals.Get(objectName); 
            this.globals = script.Globals.Values.ToList(); //BUILD AND THEN TRY AGAIN!
        }

        public void Update(){
            luaModCore.ModUpdate();

            if(Input.GetKeyDown(KeyCode.LeftAlt)){
                modManager.script_ShowModDialog();
            }
            if(Input.GetKeyDown(KeyCode.Y)){
                //DynValue dv = script.Globals.Get("Debug");
                //KSP2ModManager.LogModMessage(dv.String);

                foreach(KeyValuePair<string, string> dict in this.dict1){
                    KSP2ModManager.LogModMessage("Key: " + dict.Key + ". Value: " + dict.Value);
                }

            }
            if(Input.GetKeyDown(KeyCode.R)){
                KSP2ModManager.LogModMessage("ALTERNATIVE");
                
                try{
                    foreach(DynValue dnv in globals){
                    KSP2ModManager.LogModMessage(dnv.String);
                    }
                }
                catch{
                    KSP2ModManager.LogModMessage("OPTION ONE NO WORK");
                }
                try{
                    foreach(DynValue dnv in globals){
                    KSP2ModManager.LogModMessage(dnv.ToString());
                    }
                }
                catch{
                    KSP2ModManager.LogModMessage("OPTION TWO NO WORK");
                }
                
            }
        }
    }
}