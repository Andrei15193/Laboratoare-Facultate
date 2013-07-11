using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IALab1
{
    public abstract class ThreadedTask
    {
        public abstract ThreadedTaskResult TaskResult { get; }

        public abstract void Task(object param);

        public event Action<ThreadedTaskResult> ThreadEndCallback;

        public void StartTask(object param)
        {
            this.thread = new Thread(this.ThreadStart);
            this.thread.IsBackground = true;
            thread.Start(param);
        }

        public void AbortTask()
        {
            thread.Abort();
        }

        public void InterruptTask()
        {
            thread.Interrupt();
        }

        private void ThreadStart(object param)
        {
            ThreadedTaskResult taskResult = ThreadedTaskResult.Finished;
            try
            {
                this.Task(param);
            }
            catch (ThreadAbortException)
            {
                taskResult = ThreadedTaskResult.Aborted;
            }
            catch (ThreadInterruptedException)
            {
                taskResult = ThreadedTaskResult.Interrupted;
            }
            catch
            {
                taskResult = ThreadedTaskResult.Error;
            }
            finally
            {
                if (this.ThreadEndCallback != null)
                    this.ThreadEndCallback(taskResult);
            }
        }

        private Thread thread;
    }

    public enum ThreadedTaskResult
    {
        Aborted,
        Interrupted,
        Finished,
        Error,
        NotStarted
    }
}
