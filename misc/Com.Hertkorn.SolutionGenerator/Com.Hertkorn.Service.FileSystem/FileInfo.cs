using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Service.FileSystem
{
    public class FileInfo : FileSystemInfo
    {
        // Methods
        public FileInfo(string fileName)
        {
            throw new NotImplementedException();
        }
        //public System.IO.StreamWriter AppendText();
        public FileInfo CopyTo(string destFileName)
        {
            throw new NotImplementedException();
        }
        public FileInfo CopyTo(string destFileName, bool overwrite)
        {
            throw new NotImplementedException();
        }
        //public System.IO.FileStream Create();
        //public System.IO.StreamWriter CreateText();
        public void Decrypt()
        {
            throw new NotImplementedException();
        }
        public void Encrypt()
        {
            throw new NotImplementedException();
        }
        //public FileSecurity GetAccessControl();
        //public FileSecurity GetAccessControl(AccessControlSections includeSections);
        public void MoveTo(string destFileName)
        {
            throw new NotImplementedException();
        }
        //public FileStream Open(FileMode mode);
        //public FileStream Open(FileMode mode, FileAccess access);
        //public FileStream Open(FileMode mode, FileAccess access, FileShare share);
        //public FileStream OpenRead();
        //public StreamReader OpenText();
        //public FileStream OpenWrite();

        public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
        {
            throw new NotImplementedException();
        }

        public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            throw new NotImplementedException();
        }
        //public void SetAccessControl(FileSecurity fileSecurity);


        // Properties
        public DirectoryInfo Directory { get; protected set; }
        public string DirectoryName { get; protected set; }
        public bool IsReadOnly { get; set; }
        public long Length { get; protected set; }













        public override void Delete()
        {
            throw new NotImplementedException();
        }

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
    }
}
