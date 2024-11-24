using GDWeave;
using ChalkSave.Mods;
namespace ChalkImageDraw;


public class Mod : IMod {

    public Mod(IModInterface modInterface) {
        modInterface.Logger.Information("BarkOrMeow loaded!");
        modInterface.RegisterScriptMod(new InjectPlayer());
    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
