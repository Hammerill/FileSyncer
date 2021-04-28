using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Server
{
    class DirList
    {
        public List<DirListNode> Nodes { get; set; }

        public DirList(string path)
        {
            Nodes = new();

            if (Directory.Exists(path))
            {
                DirectoryInfo rootDir = new(path);
                SubNodes(rootDir, rootDir.Name);
            }
            else
            {
                throw new ArgumentException($"There is no dir \"{path}\"!");
            }
        }
        private void SubNodes(DirectoryInfo currentDir, string currentDirLocation)
        {
            DirListNode currentDirNode = new() { Name = currentDir.Name, RelativeName = currentDirLocation, FullName = currentDir.FullName, IsFile = false };
            Nodes.Add(currentDirNode);

            foreach (DirectoryInfo subDir in currentDir.GetDirectories())
                SubNodes(subDir, currentDirLocation + '\\' + subDir.Name);

            foreach (FileInfo subFile in currentDir.GetFiles())
            {
                DirListNode toAdd = new() { Name = subFile.Name, RelativeName = currentDirLocation + '\\' + subFile.Name, FullName = subFile.FullName, IsFile = true };
                toAdd.Hash = GetMD5(toAdd.FullName);

                Nodes.Add(toAdd);
            }
        }
        private static string GetMD5(string filename)
        {
            string hash;
            using (var md5 = MD5.Create())
            {
                using var stream = File.OpenRead(filename);
                hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
            }
            return hash;
        }

        public void Visualize()
        {
            foreach (DirListNode node in Nodes)
            {
                Console.WriteLine($"{node.RelativeName}, {{{node.Hash}}}");
            }
        }

        public List<NodeToSend> GetNodesToSend()
        {
            List<NodeToSend> toReturn = new();

            foreach (DirListNode node in Nodes)
            {
                toReturn.Add(new NodeToSend() { Name = node.RelativeName, Hash = node.Hash });
            }

            return toReturn;
        }
    }
    class DirListNode
    {
        public bool IsFile { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public string RelativeName { get; set; }
        public string FullName { get; set; }
    }

    class NodeToSend
    {
        public string Name { get; set; }
        public string Hash { get; set; }
    }
}
