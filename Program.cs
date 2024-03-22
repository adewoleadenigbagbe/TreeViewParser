using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Folder rootFolder = null;
            rootFolder = IterateFolders(@"C:\Users\user\Documents\NETProjects", rootFolder);
            Console.WriteLine("Finished !!!!!");
            Console.ReadKey();
        }

        private static Folder IterateFolders(string path, Folder root)
        {
            try
            {
                var attr = File.GetAttributes(path);
                if (!attr.HasFlag(FileAttributes.Directory))
                {
                    return root;
                }

                Console.WriteLine("Adding file: {0}", path);
                root = new Folder(path);
                foreach (var dir in Directory.GetDirectories(path))
                {
                    var child = IterateFolders(dir, root);
                    root.Folders.Add(child);
                }
            }
            catch (StackOverflowException s)
            {
                Console.WriteLine(s.Message);
                throw;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }


            return root;
        }
    }

    public class Folder
    {
        public string Name { get; set; }
        public List<Folder> Folders { get; set; }

        public Folder(string name)
        {
            Name = name;
            Folders = new List<Folder>();
        }
    }
}
