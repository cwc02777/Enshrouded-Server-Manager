using System.Net;
using Microsoft.VisualBasic.FileIO;
using System.IO.Compression;

namespace Enshrouded_Server_Manager.Services
{
    public class SteamCMD
    {
        private string _targetFolder = @"./SteamCMD/";
        private string _dlZipFile = @"./SteamCMD/steamcmd.zip";
        private string _steamCmdExe = @"./SteamCMD/steamcmd.exe";

        /// <summary>
        /// Download and installation of SteamCMD in the same folder than the Executeable
        /// </summary>
        public void Install()
        {
            FileSystem.CreateDirectory("./SteamCMD/");

            if (File.Exists(_dlZipFile))
            {
                try
                {
                    File.Delete(_dlZipFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Following error appeared while extracting: {ex.Message.ToString()}",
                    "Error while extracting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //Download of SteamCMD Client
            using (WebClient Client = new WebClient())
            {
                Client.DownloadFile("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip", "./SteamCMD/steamcmd.zip");
            }

            if (File.Exists(_steamCmdExe))
            {
                try
                {
                    File.Delete(_steamCmdExe);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Following error appeared while extracting: {ex.Message.ToString()}",
                    "Error while extracting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                ZipFile.ExtractToDirectory(_dlZipFile, _targetFolder);
                MessageBox.Show("SteamCMD installed");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Following error appeared while extracting: {ex.Message.ToString()}",
                    "Error while extracting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

    }

}