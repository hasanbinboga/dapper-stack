using NetFrame.Common.Exception;
using System.Net;

namespace NetFrame.Common.Utils.FileManagement
{
    /// <summary>
    ///It is a helper class developed to ensure that all kinds of files 
    ///that will be uploaded to the system by users are stored in the system. 
    /// </summary>
    public class NetworkFolderHelper
    {
        private readonly string RootPath;
        private readonly NetworkCredential Credentials;

        NetworkFolderHelper(string domain, string user, string pass)
        {
            try
            {

                Credentials = new NetworkCredential(user, pass, domain);
            }
            catch (System.Exception ex)
            {
                throw new UtilsException("In the appSettings section of the web.config file, NetworkFolder-RootPath, NetworkFolder-Domain, NetworkFolder-User, NetworkFolder-Pass values are required.", ex);
            }

        }

        /// <summary>
        /// Saves the file sent with arg to the Network Shared folder. Returns true if the operation is successful.
        /// </summary>
        /// <param name="arg">File to be saved in Network Shared folder</param>
        /// <returns>Returns true if the registration is successful.</returns>
        public bool SaveFileToServer(SaveFileArg arg)
        {
            try
            {
                //Mount to the network shared folder.
                using (new NetworkConnection(RootPath, Credentials))
                {
                    //Since there is no Network shared folder information in the incoming path, subfolder information is fragmented.
                    var parts = arg.Path.Split(new[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                    //Network shared folder information is added to newpath.
                    var newPath = RootPath;

                    for (int i = 1; i < parts.Length; i++)
                    {
                        newPath = Path.Combine(newPath, parts[i]);
                    }

                    //If the folder does not exist, it is created.
                    if (Directory.Exists(newPath) == false)
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    //The file is copied to the folder.
                    var filePath = Path.Combine(newPath, arg.FileName);
                    File.WriteAllBytes(filePath, arg.FileBytes);
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// If the file information sent with arg is in the Network Shared folder, it is returned as a byte array.
        /// </summary>
        /// <param name="arg">File to read from Network Shared folder</param>
        /// <returns>byte dizisi</returns>
        public byte[] GetFileFromServer(GetFileArg arg)
        {
            byte[] response = null;
            try
            {
                //Mount to the network shared folder.
                using (new NetworkConnection(RootPath, Credentials))
                {
                    var parts = arg.Path.Split(new[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);

                    var newPath = RootPath;
                    for (int i = 1; i < parts.Length; i++)
                    {
                        newPath = Path.Combine(newPath, parts[i]);
                    }
                    var filePath = Path.Combine(newPath, arg.FileName);
                    if (File.Exists(filePath))
                    {
                        response = File.ReadAllBytes(filePath);
                    }
                }
            }
            catch (System.Exception exc)
            {
                throw new UtilsException("Dosya sunucudan okunurken hata oluştu.", exc);
            }
            return response;
        }

        public bool IsFileExists(GetFileArg arg)
        {
            var response = false;
            try
            {
                //Mount to the network shared folder.
                using (new NetworkConnection(RootPath, Credentials))
                {
                    var parts = arg.Path.Split(new[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);

                    var newPath = RootPath;
                    for (int i = 1; i < parts.Length; i++)
                    {
                        newPath = Path.Combine(newPath, parts[i]);
                    }
                    var filePath = Path.Combine(newPath, arg.FileName);
                    if (File.Exists(filePath))
                    {
                        response = true;
                    }
                }
            }
            catch (System.Exception exc)
            {
                throw new UtilsException("An error occurred while reading the file from the server.", exc);
            }
            return response;
        }
    }
}
