﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Ionic.Zip;

namespace RWUpdater
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string previousInstallFolderPath = args[0];
            string zipPath = args[1];

            Console.WriteLine("Starting update...");

            //Console.WriteLine("Creating target folder..");

            Console.WriteLine("Unzipping update: " + zipPath);
            using (ZipFile zip = ZipFile.Read(zipPath))
            {
                foreach (ZipEntry entry in zip)
                {
                    entry.Extract(previousInstallFolderPath,ExtractExistingFileAction.OverwriteSilently);
                }
            }
            
            Console.WriteLine("Deleting zip...");
            File.Delete(zipPath);
            
            //Console.WriteLine("Deleting orginal folder: " + previousInstallFolderPath);

            Console.WriteLine("Restarting app...");
            Process.Start(previousInstallFolderPath + "/MarkdownToRW.exe");
            
            Console.WriteLine("Update done! Exiting in 3 seconds...");

            Thread.Sleep(3000);

            Environment.Exit(0);
        }
    }
}