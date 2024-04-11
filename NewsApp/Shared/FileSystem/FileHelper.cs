namespace NewsApp.Shared.FileSystem
{
    public static class FileHelper
    {
        /// <summary>
        /// Copying form file to physical file in server
        /// </summary>
        /// <param name="file"></param>
        /// <param name="pathCopyTo"></param>
        /// <returns>Successful status or not</returns>
        public static async Task<bool> CopyFileAsync(IFormFile file, string pathCopyTo)
        {
            using(FileStream fs = new FileStream(pathCopyTo, FileMode.OpenOrCreate, FileAccess.Write))
            {
                await file.CopyToAsync(fs);
            }

            return true;
        } 
    }
}
