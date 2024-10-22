using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using Microsoft.VisualBasic;
using OlympusMod.Content.UI;

namespace OlympusMod.Core.Loaders
{
    public class UILoader : ModSystem
    {
        public static List<UserInterface> UserInterfaces = [];

        public static List<OlympusUIState> UIStates = [];
        public override void Load()
        {
            foreach (Type t in Mod.Code.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(OlympusUIState))))
            {
                var state = (OlympusUIState)Activator.CreateInstance(t);
                var UI = new UserInterface();
                UI.SetState(state);
                state.UserInterface = UI;
                UIStates?.Add(state);
                UserInterfaces?.Add(UI);
            }
        }
        public override void Unload()
        {
            foreach (OlympusUIState state in UIStates)
            {
                state.Unload();
            }
            UserInterfaces = null;
            UIStates = null;
        }
        public static T GetUIState<T>() where T : OlympusUIState
        {
            return UIStates.FirstOrDefault(n => n is T) as T;
        }
        public static void AddLayer(List<GameInterfaceLayer> layers, UIState state, int index, bool visible, InterfaceScaleType scale)
        {
            string name = state == null ? "Unknown" : state.ToString();
            layers.Insert(index, new LegacyGameInterfaceLayer("Olympus: " + name,
                delegate
                {
                    if (visible)
                        state.Draw(Main.spriteBatch);

                    return true;
                }, scale));
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            for (int k = 0; k < UIStates.Count; k++)
            {
                OlympusUIState state = UIStates[k];
                AddLayer(layers, state, state.InsertionIndex(layers), state.Visible, state.Scale);
            }
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (Main.ingameOptionsWindow || Main.InGameUI.IsVisible)
                return;
            foreach (UserInterface eachState in UserInterfaces)
            {
                if (eachState?.CurrentState != null && ((OlympusUIState)eachState.CurrentState).Visible)
                    eachState.Update(gameTime);
            }
        }
    }
}
