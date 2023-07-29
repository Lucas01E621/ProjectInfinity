using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Microsoft.Xna.Framework;
using ProjectInfinity.Content.Items.Dye;
using ReLogic.Content;

namespace ProjectInfinity
{
	public class ProjectInfinity : Mod
	{
        private List<IOrderedLoadable> loadCache;
        public static ProjectInfinity Instance { get; set; }
		
		public ProjectInfinity()
		{
			Instance = this;
		}
		
		public override void Load()
		{
            
			Logger.Debug("Modname is:" +this.Name);

            loadCache = new List<IOrderedLoadable>();

            foreach (Type type in Code.GetTypes())
            {
                if (!type.IsAbstract && type.GetInterfaces().Contains(typeof(IOrderedLoadable)))
                {
                    object instance = Activator.CreateInstance(type);
                    loadCache.Add(instance as IOrderedLoadable);
                }

                loadCache.Sort((n, t) => n.Priority.CompareTo(t.Priority));
            }

            for (int k = 0; k < loadCache.Count; k++)
            {
                loadCache[k].Load();
                SetLoadingText("Loading " + loadCache[k].GetType().Name);
            }
            LoadFX();
        }
		public override void Unload()
		{
            foreach (IOrderedLoadable loadable in loadCache)
            {
                loadable.Unload();
            }
            loadCache = null;

            if (!Main.dedServ)
            {
                Instance = null;
            }
        }
        public static void SetLoadingText(string text)
        {
            FieldInfo Interface_loadMods = typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.Interface")!.GetField("loadMods", BindingFlags.NonPublic | BindingFlags.Static)!;
            MethodInfo UIProgress_set_SubProgressText = typeof(Mod).Assembly.GetType("Terraria.ModLoader.UI.UIProgress")!.GetProperty("SubProgressText", BindingFlags.Public | BindingFlags.Instance)!.GetSetMethod()!;

            UIProgress_set_SubProgressText.Invoke(Interface_loadMods.GetValue(null), new object[] { text });
        }
        public void LoadFX()
        {
            Filters.Scene["testscreenshader"] = new Filter(new ScreenShaderData(new Ref<Effect>(this.Assets.Request<Effect>("Effects/testscreenshader", AssetRequestMode.ImmediateLoad).Value), "sstest"), EffectPriority.Medium);
            Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(new Ref<Effect>(this.Assets.Request<Effect>("Effects/Shockwave", AssetRequestMode.ImmediateLoad).Value), "waveEffect"), EffectPriority.VeryHigh);
            Filters.Scene["Shockwave"].Load();
        }
    }
}