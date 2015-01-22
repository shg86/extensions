﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class FileInfoExtensions
    {
        /// <summary>
        /// 	Renames a file.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.Rename("test2.txt");
        /// 	</code>
        /// </example>
        public static FileInfo Rename(this FileInfo file, string newName)
        {
            var filePath = Path.Combine(Path.GetDirectoryName(file.FullName), newName);
            file.MoveTo(filePath);
            return file;
        }

        /// <summary>
        /// 	Renames a without changing its extension.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.RenameFileWithoutExtension("test3");
        /// 	</code>
        /// </example>
        public static FileInfo RenameFileWithoutExtension(this FileInfo file, string newName)
        {
            var fileName = string.Concat(newName, file.Extension);
            file.Rename(fileName);
            return file;
        }

        /// <summary>
        /// 	Changes the files extension.
        /// </summary>
        /// <param name = "file">The file.</param>
        /// <param name = "newExtension">The new extension.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.ChangeExtension("xml");
        /// 	</code>
        /// </example>
        public static FileInfo ChangeExtension(this FileInfo file, string newExtension)
        {
            newExtension = newExtension.EnsureStartsWith(".");
            var fileName = string.Concat(Path.GetFileNameWithoutExtension(file.FullName), newExtension);
            file.Rename(fileName);
            return file;
        }

        /// <summary>
        /// 	Deletes several files at once and consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.Delete()
        /// 	</code>
        /// </example>
        public static void Delete(this FileInfo[] files)
        {
            files.Delete(true);
        }

        /// <summary>
        /// 	Deletes several files at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "consolidateExceptions">if set to <c>true</c> exceptions are consolidated and the processing is not interrupted.</param>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.Delete()
        /// 	</code>
        /// </example>
        public static void Delete(this FileInfo[] files, bool consolidateExceptions)
        {

            if (consolidateExceptions)
            {
                List<Exception> exceptions = new List<Exception>();

                foreach (var file in files)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                }
            }
            else
            {
                foreach (var file in files)
                {
                    file.Delete();
                }
            }
        }

        /// <summary>
        /// 	Copies several files to a new folder at once and consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <returns>The newly created file copies</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		var copiedFiles = files.CopyTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] CopyTo(this FileInfo[] files, string targetPath)
        {
            return files.CopyTo(targetPath, true);
        }

        /// <summary>
        /// 	Copies several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <param name = "consolidateExceptions">if set to <c>true</c> exceptions are consolidated and the processing is not interrupted.</param>
        /// <returns>The newly created file copies</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		var copiedFiles = files.CopyTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] CopyTo(this FileInfo[] files, string targetPath, bool consolidateExceptions)
        {
            var copiedfiles = new List<FileInfo>();
            List<Exception> exceptions = null;

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    copiedfiles.Add(file.CopyTo(fileName));
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null)
                            exceptions = new List<Exception>();
                        exceptions.Add(e);
                    }
                    else
                        throw;
                }
            }

            return copiedfiles.ToArray();
        }

        /// <summary>
        /// 	Moves several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <returns>The moved files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.MoveTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] MoveTo(this FileInfo[] files, string targetPath)
        {
            return files.MoveTo(targetPath, true);
        }

        /// <summary>
        /// 	Movies several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name = "files">The files.</param>
        /// <param name = "targetPath">The target path.</param>
        /// <param name = "consolidateExceptions">if set to <c>true</c> exceptions are consolidated and the processing is not interrupted.</param>
        /// <returns>The moved files</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.MoveTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] MoveTo(this FileInfo[] files, string targetPath, bool consolidateExceptions)
        {
            List<Exception> exceptions = null;

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    file.MoveTo(fileName);
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null)
                            exceptions = new List<Exception>();
                        exceptions.Add(e);
                    }
                    else
                        throw;
                }
            }

            return files;
        }
    }
}
