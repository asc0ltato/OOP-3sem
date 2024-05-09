using System.Reflection;
using System.IO.Compression;

namespace Lab12
{
    public static class ZSSLog
    {
        static string logfile = "zsslogfile.txt";
        public static void Write(string method, string? filename = null)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logfile, true))
                {
                    string logMessage = $"Дата: {DateTime.Now}\n" + $"Имя файла: {filename}\n" + $"Метод: {method}";
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось записать информацию в файл: {ex.Message}");
            }
        }

        public static string Read()
        {
            try
            {
                using (StreamReader reader = new StreamReader(logfile))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Не удалось прочитать информацию из файла: {ex.Message}");
                return "";
            } 
        }
        public static bool Find(string str)
        {
            try
            {
                string text = Read();
                if (text.IndexOf(str) != -1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex) {
                Console.WriteLine($"Ошибка поиска в файле: {ex.Message}");
                return false;
            }
        }

    }

    public static class ZSSDiskInfo
    {
        public static void GetFreeSpaceOnDrive()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Свободное место на диске {drive.Name}: {drive.AvailableFreeSpace} байтов");
            }

            ZSSLog.Write(MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
        }

        public static void GetFileSystem()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Файловая система диска {drive.Name}: {drive.DriveFormat}");
            }

            ZSSLog.Write(MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
        }

        public static void GetDiskInformation()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Имя диска: {drive.Name}");
                    Console.WriteLine($"Объем диска: {drive.TotalSize} байтов");
                    Console.WriteLine($"Доступный объем: {drive.TotalFreeSpace} байтов");
                    Console.WriteLine($"Метка тома: {drive.VolumeLabel}\n");
                }
            }

            ZSSLog.Write(MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
        }
        public static void DemonstrateDiskInfo()
        {
            Console.WriteLine("Информация о диске: ");
            GetFreeSpaceOnDrive();
            GetFileSystem();
            GetDiskInformation();
        }
    }


    public class ZSSFileInfo
    {
        public static string GetFullPath(string filename)
        {
            string path = Path.GetFullPath(filename);
            Console.WriteLine("Полный путь: " + path);
            return path;
        }

        public static void GetBasicFileInfo(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);
            Console.WriteLine("\nИмя:          " + fileInfo.Name +
                              "\nРасширение:     " + fileInfo.Extension +
                              "\nРазмер:          " + fileInfo.Length + " байтов");
        }

        public static void GetDateInfo(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);
            Console.WriteLine("\nДата создания:  " + fileInfo.CreationTime +
                              "\nДата изменения: " + fileInfo.LastWriteTime);
        }

        public static void DemonstrateFileInfo(string filename)
        {
            Console.WriteLine("Информация о файле: " + filename);
            GetFullPath(filename);
            GetBasicFileInfo(filename);
            GetDateInfo(filename);
        }
    }


    public class ZSSDirInfo
    {
        public static void GetDirInfo(string dirName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirName);
            if (dirInfo.Exists)
            {
                Console.WriteLine("\nИмя директории:           " + dirInfo.Name +
                                  "\nКоличество файлов:        " + dirInfo.GetFiles().Length +
                                  "\nВремя создания:           " + dirInfo.CreationTime +
                                  "\nКол-во поддиректорий:    " + dirInfo.GetDirectories().Length +
                                  "\nРодительские директории:");

                DirectoryInfo? parentDir = dirInfo.Parent;
                while (parentDir != null)
                {
                    Console.WriteLine("   " + parentDir.FullName);
                    parentDir = parentDir.Parent;
                }
            }
            else
            {
                Console.WriteLine("Директория не существует.");
            }

            ZSSLog.Write("GetDirInfo", dirName);
        }

        public static void DemonstrateDirInfo(string dirName)
        {
            Console.WriteLine("Информация о директории: " + dirName);
            GetDirInfo(dirName);
        }
    }

    public class ZSSFileManager
    {
        public static void ReadDiskContents(string diskPath)
        {
            string inspectDirPath = Path.Combine(diskPath, "ZSSInspect");
            Directory.CreateDirectory(inspectDirPath);

            string fileInfoPath = Path.Combine(inspectDirPath, "zssdirinfo.txt");

            try
            {
                string[] filesAndDirs = Directory.GetFileSystemEntries(diskPath);
                File.WriteAllLines(fileInfoPath, filesAndDirs);
                Console.WriteLine($"Создан файл zssdirinfo.txt с информацией о содержимом диска {diskPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении содержимого диска: {ex.Message}");
            }

            ZSSLog.Write(nameof(ReadDiskContents));
        }

        public static void CopyAndRenameFile(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                File.Copy(sourceFilePath, destinationFilePath);
                Console.WriteLine("Файл скопирован");

                string renamedFilePath = Path.ChangeExtension(destinationFilePath, ".txt");
                File.Move(destinationFilePath, renamedFilePath);
                Console.WriteLine("Файл переименован");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при копировании или переименовании файла: {ex.Message}");
            }

            ZSSLog.Write(nameof(CopyAndRenameFile));
        }

        public static void DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine("Файл удален");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении файла: {ex.Message}");
            }

            ZSSLog.Write(nameof(DeleteFile));
        }

        public static void CopyFilesByExtension(string sourceDir, string destinationDir, string extension)
        {
            try
            {
                Directory.CreateDirectory(destinationDir);

                string[] files = Directory.GetFiles(sourceDir, $"*.{extension}");
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(destinationDir, fileName);
                    File.Copy(file, destFile, true);
                }

                Console.WriteLine($"Файлы с расширением {extension} скопированы в {destinationDir}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при копировании файлов: {ex.Message}");
            }

            ZSSLog.Write(nameof(CopyFilesByExtension));
        }

        public static void MoveDirectory(string sourceDir, string destinationDir)
        {
            try
            {
                Directory.Move(destinationDir, sourceDir);
                Console.WriteLine($"Директория {destinationDir} перемещена в {sourceDir}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при перемещении директории: {ex.Message}");
            }

            ZSSLog.Write(nameof(MoveDirectory));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ZSSDiskInfo.DemonstrateDiskInfo();
            ZSSFileInfo.DemonstrateFileInfo("Lab12.exe"); ;
            ZSSDirInfo.DemonstrateDirInfo("C:\\Users\\Леново\\Desktop\\ООП");

            string sourceDir = "C:\\ZSSInspect"; 
            string destinationDir = "C:\\Users\\Леново\\Desktop\\ООП\\Lab12\\Lab12\\bin\\Debug\\net6.0";

            ZSSFileManager.ReadDiskContents("C:\\");

            string sourceFilePath = Path.Combine(sourceDir, "zssdirinfo.txt");
            string destinationFilePath = Path.Combine(destinationDir, "zssdirinfo2.0");

            ZSSFileManager.CopyAndRenameFile(sourceFilePath, destinationFilePath);

            string fileToDelete = Path.Combine(sourceDir, "zssdirinfo.txt");
            ZSSFileManager.DeleteFile(fileToDelete);

            string copiedFilesDestination = Path.Combine(destinationDir, "ZSSFiles");
            ZSSFileManager.CopyFilesByExtension(destinationDir, copiedFilesDestination, "txt");
            string sourceDirectory = "C:\\ZSSInspect\\ZSSFiles"; 
            string destinationDirectory = "C:\\Users\\Леново\\Desktop\\ООП\\Lab12\\Lab12\\bin\\Debug\\net6.0\\ZSSFiles";
            ZSSFileManager.MoveDirectory(sourceDirectory, destinationDirectory);

            string zipFile = "C:\\ZSSInspect\\test.zip";
            string targetFolder = "C:\\Mama";
            ZipFile.CreateFromDirectory(sourceDirectory, zipFile);
            Console.WriteLine($"Папка {sourceDirectory} архивирована в файл {zipFile}");
            ZipFile.ExtractToDirectory(zipFile, targetFolder);

            Console.WriteLine($"Файл {zipFile} распакован в папку {targetFolder}");
            Console.ReadLine();
        }
    }
}