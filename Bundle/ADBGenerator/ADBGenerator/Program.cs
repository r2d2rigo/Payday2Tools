using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Generic;
using System.Data.SQLite.Linq;
using BundleLib;
using System.IO;

namespace ADBGenerator
{
    class Program
    {
        private static readonly List<string> KnownFileTypes = new List<string>()
        {
            "nav_data",
            "unit",
            "object",
            "model",
            "material_config",
            "cooked_physics",
            "continent",
            "mission",
            "gui",
            "texture",
            "sequence_manager",
            "massunit",
            "scene",
            "effect",
            "world_cameras",
            "environment",
            "world_sounds",
            "world",
            "continents",
            "cover_data",
            "bnk",
            "animation_state_machine",
            "animation_states",
            "animation_def",
            "animation_subset",
            "animation",
            "physic_effect",
            "menu",
            "credits",
            "font",
            "banksinfo",
            "shaders",
            "diesel_layers",
            "light_intensities",
            "network_settings",
            "render_config",
            "texture_channels",
            "render_template_database",
            "post_processor",
            "cgb",
            "camera_shakes",
            "cameras",
            "atom_batcher_settings",
            "physics_settings",
            "scenes",
            "controller_settings",
            "achievment",
            "objective",
            "hint",
            "comment",
            "action_message",
            "dialog_index",
            "dialog",
            "prefhud",
            "lua",
            "xml",
            "movie",
            "stream",
            "tga",
            "bnkinfo",
            "sfap0",
            "psd",
            "xbox_live",
            "bmfc",
            "idstring_lookup",
            "decals",
            "strings",
            "merged_font",
        };

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                PrintHelpText();

                return;
            }

            string assetsDir = args[0];
            string outputDir = string.Empty;

            if (args.Length > 1)
            {
                outputDir = args[1];
            }

            BundleHeader bundleHeader = BundleHeader.Load(File.OpenRead(Path.Combine(assetsDir, "0a76b707eba65bc7_h.bundle")));

            int a = 0;
        }

        private static void PrintHelpText()
        {
            Console.WriteLine("Usage: ADBGenerator.exe assetsdir [outputdir]");
        }
    }
}
