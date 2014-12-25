using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PD2Bundle
{
    class Program
    {
        static private NameIndex name_index = new NameIndex();
        static private KnownIndex known_index = new KnownIndex();
        static private bool list_only = false;
        static private bool extract_one = false;
        static private string extract_id = "";
        static private bool extract_all = false;
        static private bool update = false;
        static private bool update_only = false;


        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                PrintHelpText();

                return;
            }

            string bundleFile = args[0];
            string outputDir = args[1];

            if (!File.Exists(bundleFile))
            {
                Console.WriteLine("Bundle file \"" + bundleFile + "\" does not exist.");

                return;
            }

            string bundleFileDir = Path.GetDirectoryName(bundleFile);
            string bundleFileName = Path.GetFileNameWithoutExtension(bundleFile);
            string bundleHeaderFile = Path.Combine(bundleFileDir, bundleFileName + "_h.bundle");

            if (!File.Exists(bundleHeaderFile))
            {
                Console.WriteLine("Bundle header file \"" + bundleHeaderFile + "\" does not exist.");

                return;
            }

            if (!Directory.Exists(outputDir))
            {
                try
                {
                    Directory.CreateDirectory(outputDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error creating directory \"" + outputDir + "\": " + ex.GetType().ToString());

                    return;
                }
            }

            using (FileStream bundleHeaderStream = File.OpenRead(bundleHeaderFile))
            {
                ListBundleContents(bundleHeaderStream, outputDir);
            }

            using (FileStream bundleStream = File.OpenRead(bundleFile))
            {
                ExtractBundle(bundleStream, outputDir);
            }

            return;

            /*
            foreach (string arg in args)
            {
                switch (arg)
                {
                    case "-list":
                        list_only = true;
                        break;
                    case "-extract_all":
                        extract_all = true;
                        break;
                    case "-update":
                        update = true;
                        break;
                    case "-update_only":
                        update = true;
                        update_only = true;
                        break;
                    default:
                        extract_one = true;
                        extract_id = arg;
                        break;
                }
            }
            if (!Load())
                return;

            if (update)
            {
                if (!Update("all_14"))
                {
                    Console.WriteLine("Error while updating");
                    return;
                }

                update = false;
                known_index.Clear();
                name_index.Clear();
                if (!Load())
                    return;
                Console.WriteLine("Paths and Extensions Updated Successfully");
            }
            if (update_only)
                return;


            if (extract_one && extract_id.Length > 0)
            {
                bundle = BundleHeader.Load(extract_id);
                if (bundle == null)
                {
                    Console.WriteLine("Failed to parse bundle header.");
                    return;
                }
                if (list_only)
                {
                    ListBundle(extract_id);
                }
                else
                {
                    ExtractBundle(extract_id);
                }
            }
            else
            {
                foreach (string file in Directory.EnumerateFiles(".", "*_h.bundle"))
                {
                    string bundle_id = file.Replace("_h.bundle", "");
                    bundle_id = bundle_id.Remove(0, 2);
                    bundle = BundleHeader.Load(bundle_id);
                    Console.WriteLine("Loading bundle header...");
                    if (bundle == null)
                    {
                        Console.WriteLine("Failed to parse bundle header.");
                        return;
                    }
                    Console.WriteLine("Extract bundle: {0}", bundle_id);

                    if (list_only)
                    {
                        ListBundle(bundle_id);
                    }
                    else
                    {
                        ExtractBundle(bundle_id);
                    }
                }
            }
            */
        }

        /*
        static void ListBundle(string bundle_id)
        {
            foreach (BundleEntry be in bundle.Entries)
            {
                string path = String.Format("unknown_{0:x}.bin", be.Id);
                NameEntry ne = name_index.Id2Name(be.Id);
                if (ne != null)
                {
                    string name = known_index.GetPath(ne.Path);
                    string extension = known_index.GetExtension(ne.Extension);
                    if (name != null)
                    {
                        path = name;
                    }
                    else
                    {
                        path = String.Format("{0:x}", ne.Path);
                    }
                    if (ne.Language != 0)
                    {
                        path += String.Format(".{0:x}", ne.Language);
                    }
                    if (extension != null)
                    {
                        path += String.Format(".{0}", extension);
                    }
                    else
                    {
                        path += String.Format(".{0:x}", ne.Extension);
                    }
                }
                if (!update)
                    Console.WriteLine("{0:x} - {1}", ne.Path, path);
            }
        }

        static void ExtractBundle(string bundle_id)
        {
            string bundle_file = bundle_id + ".bundle";
            if (!File.Exists(bundle_file))
            {
                Console.WriteLine("Bundle file does not exist.");
                return;
            }
            using (FileStream fs = new FileStream(bundle_file, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    if (!Directory.Exists("extract"))
                    {
                        Directory.CreateDirectory("extract");
                    }
                    string file_prefix = "extract/";
                    byte[] data;
                    foreach (BundleEntry be in bundle.Entries)
                    {
                        string path = String.Format("unknown_{0:x}.bin", be.Id);
                        NameEntry ne = name_index.Id2Name(be.Id);
                        if (ne != null)
                        {
                            string name = known_index.GetPath(ne.Path);
                            string extension = known_index.GetExtension(ne.Extension);
                            if (!extract_all)
                            {
                                switch (extension)
                                {
                                    case "stream":
                                        continue;
                                    case "texture":
                                        continue;
                                    case "movie":
                                        continue;
                                }
                            }
                            if (name != null)
                            {
                                path = name;
                            }
                            else
                            {
                                path = String.Format("{0:x}", ne.Path);
                            }
                            if (ne.Language != 0)
                            {
                                path += String.Format(".{0:x}", ne.Language);
                            }
                            if (extension != null)
                            {
                                path += String.Format(".{0}", extension);
                            }
                            else
                            {
                                path += String.Format(".{0:x}", ne.Extension);
                            }
                        }
                        string folder = Path.GetDirectoryName(path);
                        if (folder != null && folder.Length != 0)
                        {
                            if (!Directory.Exists(file_prefix + folder))
                            {
                                Directory.CreateDirectory(file_prefix + folder);
                            }
                        }
                        using (FileStream os = new FileStream(file_prefix + path, FileMode.Create, FileAccess.Write))
                        {
                            using (BinaryWriter obr = new BinaryWriter(os))
                            {
                                fs.Position = be.Address;
                                if (be.Length == -1)
                                {
                                    data = br.ReadBytes((int)(fs.Length - fs.Position));
                                    obr.Write(data);
                                }
                                else
                                {
                                    data = br.ReadBytes((int)be.Length);
                                    obr.Write(data);
                                }
                            }
                        }
                    }
                }
            }
        }

        static bool Load()
        {
            if (!update)
                Console.WriteLine("Loading bundle_db.blb...");
            if (!name_index.Load("bundle_db.blb"))
            {
                Console.WriteLine("Failed to parse bundle_db.blb. Are you sure the path is valid and the file is not corrupt?");
                return false;
            }

            if (!update)
                Console.WriteLine("Loading known files index...");
            if (!known_index.Load())
            {
                Console.WriteLine("Failed to parse the know index files (extensions.txt and paths.txt). Are they present and in the current directory?");
                return false;
            }
            return true;
        }

        static bool Update(string bundle_id)
        {
            HashSet<string> new_paths = new HashSet<string>();
            HashSet<string> new_exts = new HashSet<string>();
            StringBuilder sb = new StringBuilder();
            string[] idstring_data;

            Console.WriteLine("Updating Paths and Extensions...");

            bundle = BundleHeader.Load(bundle_id);
            if (bundle == null)
            {
                Console.WriteLine("[Update error] Failed to parse bundle header. ({0})", bundle_id);
                return false;
            }

            string bundle_file = bundle_id + ".bundle";
            if (!File.Exists(bundle_file))
            {
                Console.WriteLine("[Update error] Bundle file does not exist. ({0})", bundle_file);
                return false;
            }
            using (FileStream fs = new FileStream(bundle_file, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] data;
                    foreach (BundleEntry be in bundle.Entries)
                    {
                        string path = String.Format("unknown_{0:x}.bin", be.Id);
                        NameEntry ne = name_index.Id2Name(be.Id);
                        if (ne != null)
                        {
                            string name = known_index.GetPath(ne.Path);
                            string extension = known_index.GetExtension(ne.Extension);

                            if (name != null)
                            {
                                path = name;
                            }
                            else
                            {
                                path = String.Format("{0:x}", ne.Path);
                            }
                        }


                        if (path.Contains("idstring_lookup"))
                        {
                            fs.Position = be.Address;
                            if (be.Length == -1)
                            {
                                data = br.ReadBytes((int)(fs.Length - fs.Position));
                            }
                            else
                            {
                                data = br.ReadBytes((int)be.Length);
                            }

                            foreach (byte read in data)
                            {
                                sb.Append((char)read);
                            }

                            idstring_data = sb.ToString().Split('\0');
                            sb.Clear();

                            foreach (string idstring in idstring_data)
                            {
                                if (idstring.Contains("/"))
                                    new_paths.Add(idstring.ToLower());
                                else if (!idstring.Contains("/") && !idstring.Contains(".") && !idstring.Contains(":") && !idstring.Contains("\\"))
                                    new_exts.Add(idstring.ToLower());
                            }

                            new_paths.Add("idstring_lookup");
                            new_paths.Add("existing_banks");

                            known_index.Clear();
                            known_index.Load(ref new_paths, ref new_exts);

                            foreach (string file in Directory.EnumerateFiles(".", "*_h.bundle"))
                            {
                                string bundle_id_list = file.Replace("_h.bundle", "");
                                bundle_id_list = bundle_id_list.Remove(0, 2);
                                bundle = BundleHeader.Load(bundle_id_list);
                                if (bundle == null)
                                {
                                    Console.WriteLine("[Update error] Failed to parse bundle header. ({0})", bundle_id_list);
                                    return false;
                                }
                                ListBundle(bundle_id_list);
                            }

                            known_index.GenerateUsedPaths();
                            known_index.GenerateUsedExts();

                            new_paths.Clear();
                            new_exts.Clear();

                            return true;
                        }
                    }
                    br.Close();
                }
                fs.Close();
            }

            return false;
        }
        */

        //----------------

        private static void PrintHelpText()
        {
            Console.WriteLine("Usage: p2bundle.exe bundlefile outputdir [options]");
            Console.WriteLine("Options: ");
            Console.WriteLine("-extract:\t\tExtracts bundle file (default option if none are specified).");
            Console.WriteLine("-help:\t\tDisplays this help text.");
            Console.WriteLine("-update:\t\tGenerates extension and file path lists.");
        }

        private static void ExtractBundle(Stream bundleStream, string outputDir)
        {
        }

        private static void ListBundleContents(Stream bundleHeaderStream, string outputDir)
        {
            BundleHeader bundleHeader = BundleHeader.Load(bundleHeaderStream);

            foreach (BundleEntry be in bundleHeader.Entries)
            {
                string path = String.Format("unknown_{0:x}.bin", be.Id);
                NameEntry ne = name_index.Id2Name(be.Id);
                if (ne != null)
                {
                    string name = known_index.GetPath(ne.Path);
                    string extension = known_index.GetExtension(ne.Extension);
                    if (name != null)
                    {
                        path = name;
                    }
                    else
                    {
                        path = String.Format("{0:x}", ne.Path);
                    }
                    if (ne.Language != 0)
                    {
                        path += String.Format(".{0:x}", ne.Language);
                    }
                    if (extension != null)
                    {
                        path += String.Format(".{0}", extension);
                    }
                    else
                    {
                        path += String.Format(".{0:x}", ne.Extension);
                    }
                }
            }
        }
    }
}
