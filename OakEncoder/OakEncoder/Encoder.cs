using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace OakEncoder
{
    class Encoder : IDisposable
    {
        FileStream readStream;
        FileStream writeStream;
        //StreamWriter LogFile = new StreamWriter("./log.txt", false);
        int key;
        private int blockSize;

        public Encoder()
        {
        }
        public Encoder(string path, string output, int key)
        {
            SetPath(path, output);
            SetKey(key);
        }

        public bool SetPath(string path, string output)
        {
            try
            {
                readStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                writeStream = new FileStream(output, FileMode.Create, FileAccess.Write);
                return true;
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, ex.ToString());
            }
            return false;
        }

        private bool SetKey(int key)
        {
            if(key == 0)
            {
                return false;
            }
            else
            {
                this.key = key;
                return true;
            }
        }

        public int Key
        {
            get { return this.key; }
            set { this.SetKey(value); }
        }

        int cores = System.Environment.ProcessorCount;

        public void Start(Action afterFinished)
        {
            //if (readStream.Length < cores) // then something else
            this.blockSize = (int)(readStream.Length / cores);
            if (readStream.Length % cores > 0) this.blockSize++; 

            Task task = new Task(() => EncodeBlock(null, null));
            task.ContinueWith(
                t =>
                    {
                        readStream.Close();
                        writeStream.Close();
                        //LogFile.Flush();
                        afterFinished();
                    },
                TaskContinuationOptions.ExecuteSynchronously);
            task.Start();
        }

        int block_index = 0;

        private void EncodeBlock(Task parent, object prevWriteTask) //, object status) //int start, int n)
        {
            Interlocked.Increment(ref block_index);

            // Add CancellationToken
            Task<byte[]> readTask = //readStream.ReadAsync(buffer, 0, blockSize);
                new Task<byte[]>(
                    () => {
                        byte[] buffer = new byte[blockSize];
                        int readbytes = readStream.Read(buffer, 0, blockSize);  // blockSize simultaneous access?
                        //if (readbytes == 0)
                        //{
                        //    throw new Exception("Nothing was read.");
                        //}
                        //else
                        //{
                        //    Interlocked.Exchange(ref block_index, readbytes);
                        //}
                        //Interlocked.Exchange(ref blockSize, readbytes);
                        if (readbytes == 0) buffer = null;
                        return buffer;
                        }
                );

            Task<byte[]> encodeTask = readTask.ContinueWith(Encode,
                TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.AttachedToParent);
            // TaskContinuationOptions correct?

            Task writeTask = //writeStream.WriteAsync(encodeTask.Result, 0, readTask.Result);
                encodeTask.ContinueWith( 
                    (task, state) => 
                    {
                        //LogFile.WriteLine($"<write> Index: {block_index} BlockSize: {blockSize} Thread: {Thread.CurrentThread.ManagedThreadId}");
                        (state as Task)?.Wait();
                        //LogFile.WriteLine($"        Waited.");
                        if (task.Result != null)
                        {
                            writeStream.Write(task.Result, 0, blockSize);
                            //LogFile.WriteLine($"        Written:\"{(char)task.Result[0]}{(char)task.Result[1]}\"");
                        }
                    },
                    prevWriteTask,
                    TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.AttachedToParent);

            Task nextBlockTask = null;
            //if (readStream.Position + blockSize < readStream.Length)
            //Interlocked.CompareExchange(ref block_index, -1, cores)
            if (block_index < cores)
            {

                nextBlockTask = readTask.ContinueWith(EncodeBlock, writeTask,
                    TaskContinuationOptions.OnlyOnRanToCompletion);
                // TaskContinuationOptions correct?
            }

            // Possible encodeTask.ContinueWith call to attach writeTask

            readTask.Start();

            nextBlockTask?.Wait();
            //writeTask.Wait();
        }

        private byte[] Encode(Task<byte[]> parent)
        {
            byte[] buffer = parent.Result; //(byte[])state;
            if (buffer != null)
            {
                byte[] encoded = new byte[buffer.Length];
                for (int i = 0; i < buffer.Length; i++)
                {
                    encoded[i] = (byte)((buffer[i] + this.key) % 256);
                }
                return encoded;
            }
            else
            {
                return null;
            }
            // Possible write here
        }

        // ------

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //this.LogFile.Dispose();
                    this.readStream.Dispose();
                    this.writeStream.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
