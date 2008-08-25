using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Service.FileSystem
{
    public abstract class FileSystemInfo
    {
        public abstract void Delete();
        public void Refresh()
        {
            throw new NotImplementedException();
        }

        //public FileAttributes Attributes
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        public DateTime CreationTime { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public abstract bool Exists { get; }
        public string Extension { get; private set; }
        public virtual string FullName { get; protected set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastAccessTimeUtc { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        public abstract string Name { get; }
    }
}
