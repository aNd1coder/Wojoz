using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Wojoz.Services.Tasks
{ 
    using Wojoz.Utilities;
    using Wojoz.Config;

    /// <summary>
    /// TasksManager is called from the TasksHttpModule (or another means of scheduling a Timer). Its sole purpose
    /// is to iterate over an array of Taskss and deterimine of the Tasks's ITasks should be processed. All Taskss are
    /// added to the managed threadpool. 
    /// </summary>
    public class TaskManager
    {
        public static string RootPath;

        private TaskManager()
        {
        }

        public static readonly int TimerMinutesInterval = 5;
        static TaskManager()
        {
            if (ScheduleConfigs.GetConfig().TimerMinutesInterval > 0)
            {
                TimerMinutesInterval = ScheduleConfigs.GetConfig().TimerMinutesInterval;
            }
        }


        public static void Execute()
        {
            //Wojoz.Config.Events[] simpleItems = ScheduleConfigs.GetConfig().Events;
            //Tasks[] items;
            //List<Tasks> list = new List<Tasks>();

            //foreach (Wojoz.Config.Tasks newTasks in simpleItems)
            //{
            //    if (!newTasks.Enabled)
            //    {
            //        continue;
            //    }
            //    Tasks e = new Tasks();
            //    e.Key = newTasks.Key;
            //    e.Minutes = newTasks.Minutes;
            //    e.ScheduleType = newTasks.ScheduleType;
            //    e.TimeOfDay = newTasks.TimeOfDay;

            //    list.Add(e);
            //}

            //items = list.ToArray();

            //Tasks item = null;

            //if (items != null)
            //{

            //    for (int i = 0; i < items.Length; i++)
            //    {
            //        item = items[i];
            //        if (item.ShouldExecute)
            //        {
            //            item.UpdateTime();
            //            ITasks e = item.ITasksInstance;
            //            ManagedThreadPool.QueueUserWorkItem(new WaitCallback(e.Execute));
            //        }
            //    }
            //}
        }
    }
}
