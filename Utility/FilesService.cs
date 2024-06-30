using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class FilesService
    {
        private readonly string _rootPath= "wwwroot";

        //怎么使用这个方法
        // 如果有头像文件，则上传
        /*
        if (avatarFile != null)
        {
            using (var stream = avatarFile.OpenReadStream())
            {
                user.AvatarUrl = await _fileService.UploadFileAsync(user.UserPath, stream, avatarFile.Name);
            }
            await _UserManager.UpdateAsync(user);
        }
        */
        //UploadFileAsync 方法的主要功能是将用户上传的文件保存到指定的用户文件夹中
        public async Task<string> UploadFileAsync(string userPath, Stream fileStream, string fileName)
        {
            // 用户文件夹路径
            string userFolderPath = Path.Combine(_rootPath, userPath);
            // 创建用户文件夹和 images 文件夹
            string imagesFolderPath = Path.Combine(userFolderPath, "images");
            Directory.CreateDirectory(imagesFolderPath);

            // 文件保存路径
            string filePath = Path.Combine(imagesFolderPath, fileName);
            using (var fileStreamOutput = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileStreamOutput);
            }

            return filePath;
        }
        //为每个新用户创建一个默认文件夹结构，并设置默认头像图片
        public void CreateDefaultUserFolder(string userPath)
        {
            // 用户文件夹路径
            string userFolderPath = Path.Combine(_rootPath, "User\\" + userPath);
            // 创建用户文件夹和 images 文件夹
            string imagesFolderPath = Path.Combine(userFolderPath, "images");
            Directory.CreateDirectory(imagesFolderPath);

            // 复制默认图片
            string defaultImagePath = Path.Combine(_rootPath, "images\\DefaultAvatar.jpg"); // 默认图片路径
            string userImagePath = Path.Combine(imagesFolderPath, "DefaultAvatar.jpg"); // 用户头像路径
            if (File.Exists(defaultImagePath))
            {
                File.Copy(defaultImagePath, userImagePath, true);
            }
        }
    }
}
