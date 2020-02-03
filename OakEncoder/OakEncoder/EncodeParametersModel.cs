using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OakEncoder
{
    class EncodeParametersModel : INotifyPropertyChanged
    {
        private string filePath;
        public string FilePath
        {
            //get;set;
            get { return filePath; }
            //set
            //{
            //    filePath = value;
            //    OutputName = Path.GetFileName(filePath);
            //}
        }
        public void SetFilePath(string path)
        {
            filePath = path;
            OutputName = (this.Encoding == true) ? Path.GetFileName(filePath) : Path.GetFileNameWithoutExtension(filePath);
            NotifyPropertyChanged("FilePath");
            NotifyPropertyChanged("OutputName");
        }

        public string Key { get; set; }

        private int keyint = 0;
        public int KeyInt
        {
            get
            {
                return (Encoding == false) ? -keyint : keyint;
            }
            set {
                keyint = value;
            }
        }
        //{ get { return int.Parse(Key); } set { Key = value.ToString(); } }

        private string extension = ".oakoded";

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public string Extension
        {
            get { return extension; }
            set
            {
                extension = value;
                extension = extension.Trim(new char[] { '.' });
                if (extension.Length > 0)
                {
                    extension.Prepend('.');
                }
                else
                {
                    throw new Exception("Invalid extension specified.");
                }
            }
        }  

        public bool? Encoding
        {
            get
            {
                if (FilePath == null)
                    return null;
                if (Path.GetExtension(FilePath) == Extension)
                    return false;
                else return true;
            }
        }

        public string OutputName
        {
            get;set;
        }

        public string OutputPath
        {
            get
            {
                if (FilePath == null)
                    return null;
                else
                {
                    string newPath = Path.GetDirectoryName(FilePath) + Path.DirectorySeparatorChar +
                        (this.Encoding == false ? 
                        OutputName :
                        //Path.GetFileNameWithoutExtension(FilePath) : 
                        OutputName + Extension);
                    return newPath;
                }
            }
        }
    }
}
