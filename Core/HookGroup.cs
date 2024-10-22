using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympusMod.Core
{
    public abstract class HookGroup
    {
        public static void LogError(string message) => OlympusMod.Instance.Logger.Error(message);
        public static void DumpIL(ILContext il) => MonoModHooks.DumpIL(OlympusMod.Instance, il);
        public virtual void Load()
        {

        }
        public virtual void Unload()
        {

        }
    }
}
