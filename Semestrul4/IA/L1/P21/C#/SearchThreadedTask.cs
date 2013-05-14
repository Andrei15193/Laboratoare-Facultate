using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IALab1
{
    public class SearchThreadedTask : ThreadedTask
    {
        public SearchThreadedTask(SearchMethod searchMethod)
        {
            if (searchMethod == null)
                throw new ArgumentNullException("The searchMethod cannot be null!");
            this.searchMethod = searchMethod;
        }

        public override void Task(object param)
        {
            uint[] numbers = param as uint[];
            if (numbers != null)
                this.numberOfFrobenius = this.searchMethod.SearchForFrobeniusNumber(numbers);
            else
                this.taskResult = ThreadedTaskResult.Error;
        }

        public override ThreadedTaskResult TaskResult
        {
            get
            {
                return this.TaskResult;
            }
        }

        public uint FrobeniusNumber
        {
            get
            {
                return this.numberOfFrobenius;
            }
        }

        private void TaskEndCallback(ThreadedTaskResult taskResult)
        {
            this.taskResult = taskResult;
        }

        private SearchMethod searchMethod;
        private ThreadedTaskResult taskResult = ThreadedTaskResult.NotStarted;
        private uint numberOfFrobenius;
    }
}
