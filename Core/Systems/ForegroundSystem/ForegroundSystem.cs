using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using ProjectInfinity.Core;
using System.Collections.Generic;

namespace ProjectInfinity.Core.Systems.ForegroundSystem
{
	class ForegroundSystem : IOrderedLoadable
	{
		public static List<Foreground> Foregrounds;

		public float Priority => 1.0f;

		public static Foreground GetForeground<T>()
		{
			return Foregrounds.First(n => n is T);
		}

		public void Load()
		{
			if (Main.dedServ)
				return;

			Foregrounds = new List<Foreground>();

			Mod Mod = ProjectInfinity.Instance;

			foreach (Type t in Mod.Code.GetTypes())
			{
				if (t.IsSubclassOf(typeof(Foreground)) && !t.IsAbstract)
					Foregrounds.Add((Foreground)Activator.CreateInstance(t));
			}
		}

		public void Unload()
		{
			Foregrounds?.ForEach(t => t.Unload());
			Foregrounds ??= null;
		}
	}
}