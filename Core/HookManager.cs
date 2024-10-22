using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympusMod.Core
{
    public class HookManager : ModSystem
    {
        public static List<HookGroup> hooks = [];
        public static T GetInstance<T>() where T : HookGroup
        {
            return hooks.FirstOrDefault(n => n is T) as T;
        }
        public override void Load()
        {
            foreach (Type t in OlympusMod.Instance.Code.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(HookGroup))))
            {
                HookGroup instance = (HookGroup)Activator.CreateInstance(t);
                hooks.Add(instance);
                instance.Load();
            }
        }
        public override void Unload()
        {
            foreach (HookGroup instance in hooks)
                instance.Unload();
            hooks?.Clear();
        }
    }
}
