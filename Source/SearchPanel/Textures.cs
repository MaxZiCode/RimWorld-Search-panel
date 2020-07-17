using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace SearchPanel
{
    public static class Textures
    {
        public static readonly Texture2D SearchToolIcon = ContentFinder<Texture2D>.Get("UI/Lupa(not Pupa)");
        public static readonly Texture2D FavoutireButton = ContentFinder<Texture2D>.Get("UI/Favourite Button");
        public static readonly Texture2D CollapseTexture = ContentFinder<Texture2D>.Get("UI/Buttons/Dev/Collapse", true);
        public static readonly Texture2D RevealTexture = ContentFinder<Texture2D>.Get("UI/Buttons/Dev/Reveal", true);
    }
}
