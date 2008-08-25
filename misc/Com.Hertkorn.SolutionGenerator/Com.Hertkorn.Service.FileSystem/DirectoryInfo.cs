using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Service.FileSystem
{
    public class DirectoryInfo : FileSystemInfo
    {
        internal DirectoryInfo(System.IO.DirectoryInfo directoryInfo)
        {
        }

        public DirectoryInfo(string path)
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        //public void Create(DirectorySecurity directorySecurity);
        public DirectoryInfo CreateSubdirectory(string path)
        {
            throw new NotImplementedException();
        }

        //public DirectoryInfo CreateSubdirectory(string path, DirectorySecurity directorySecurity);
        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public void Delete(bool recursive)
        {
            throw new NotImplementedException();
        }

        //public DirectorySecurity GetAccessControl();
        //public DirectorySecurity GetAccessControl(AccessControlSections includeSections);
        public DirectoryInfo[] GetDirectories()
        {
            throw new NotImplementedException();
        }

        public DirectoryInfo[] GetDirectories(string searchPattern)
        {
            throw new NotImplementedException();
        }

        public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException();
        }

        public FileInfo[] GetFiles()
        {
            throw new NotImplementedException();
        }

        public FileInfo[] GetFiles(string searchPattern)
        {
            throw new NotImplementedException();
        }

        public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
        {
            throw new NotImplementedException();
        }

        public FileSystemInfo[] GetFileSystemInfos()
        {
            throw new NotImplementedException();
        }

        public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
        {
            throw new NotImplementedException();
        }

        public void MoveTo(string destDirName)
        {
            throw new NotImplementedException();
        }

        //public void SetAccessControl(DirectorySecurity directorySecurity);

        // Properties
        protected bool m_exists;
        public override bool Exists
        {
            get
            {
                return m_exists;
            }
        }
        protected string m_name;
        public override string Name
        {
            get
            {
                return m_name;
            }
        }
        public DirectoryInfo Parent { get; protected set; }
        public DirectoryInfo Root { get; protected set; }







    }
}
