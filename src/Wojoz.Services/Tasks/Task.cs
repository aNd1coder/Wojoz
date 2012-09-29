using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Wojoz.Data.SqlServer;

namespace Wojoz.Services.Tasks
{
    /// <summary>
    /// Tasks is the configuration of an ITasks. 
    /// </summary>
    public class Task
    {
        /*
        private static readonly ScheduledTaskssDAL dalScheduledTaskss = new ScheduledTaskssDAL();

        public Task()
        {
        }

        private ITask _iTasks = null;

        /// <summary>
        /// The current implementation of ITasks
        /// </summary>
        public ITask ITasksInstance
        {
            get
            {
                LoadITasks();
                return _iTasks;
            }
        }

        /// <summary>
        /// Private method for loading an instance of ITasks
        /// </summary>
        private void LoadITasks()
        {
            if (_iTasks == null)
            {
                if (this.ScheduleType == null)
                {
                    TaskLogs.WriteFailedLog("计划任务没有定义其 type 属性");
                }

                Type type = Type.GetType(this.ScheduleType);
                if (type == null)
                {
                    TaskLogs.WriteFailedLog(string.Format("计划任务 {0} 无法被正确识别", this.ScheduleType));
                }
                else
                {
                    _iTasks = (ITask)Activator.CreateInstance(type);
                    if (_iTasks == null)
                    {
                        TaskLogs.WriteFailedLog(string.Format("计划任务 {0} 未能正确加载", this.ScheduleType));
                    }
                }
            }
        }

        private string _key;

        /// <summary>
        /// A unique key used to query the database. The name of the Server will also be used to ensure the "Key" is 
        /// unique in a cluster
        /// </summary>
        public string Key
        {
            get { return this._key; }
            set { this._key = value; }
        }

        private int _timeOfDay = -1;

        /// <summary>
        /// Absolute time in mintues from midnight. Can be used to assure Tasks is only 
        /// executed once per-day and as close to the specified
        /// time as possible. Example times: 0 = midnight, 27 = 12:27 am, 720 = Noon
        /// </summary>
        public int TimeOfDay
        {
            get { return this._timeOfDay; }
            set { this._timeOfDay = value; }
        }

        private int _minutes = 60;

        /// <summary>
        /// The scheduled Tasks interval time in minutes. If TimeOfDay has a value >= 0, Minutes will be ignored. 
        /// This values should not be less than the Timer interval.
        /// </summary>
        public int Minutes
        {
            get
            {
                if (this._minutes < TaskManager.TimerMinutesInterval)
                {
                    return TaskManager.TimerMinutesInterval;
                }
                return this._minutes;
            }
            set { this._minutes = value; }
        }

        private string _scheduleType;

        /// <summary>
        /// The Type of class which implements ITasks
        /// </summary>
        [XmlAttribute("type")]
        public string ScheduleType
        {
            get { return this._scheduleType; }
            set { this._scheduleType = value; }
        }

        private DateTime _lastCompleted;

        /// <summary>
        /// Last Date and Time this Tasks was processed/completed.
        /// </summary>
        [XmlIgnoreAttribute]
        public DateTime LastCompleted
        {
            get { return this._lastCompleted; }
            set
            {
                dateWasSet = true;
                this._lastCompleted = value;
            }
        }

        //internal testing variable
        bool dateWasSet = false;

        [XmlIgnoreAttribute]
        public bool ShouldExecute
        {
            get
            {
                if (!dateWasSet) //if the date was not set (and it can not be configured), check the data store
                {
                    LastCompleted = dalScheduledTaskss.GetLastExecuteScheduledTasksDateTime(this.Key, Environment.MachineName);
                }

                //If we have a TimeOfDay value, use it and ignore the Minutes interval
                if (this.TimeOfDay > -1)
                {
                    //Now
                    DateTime dtNow = DateTime.Now;  //now
                    //We are looking for the current day @ 12:00 am
                    DateTime dtCompare = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day);
                    //Check to see if the LastCompleted date is less than the 12:00 am + TimeOfDay minutes
                    return LastCompleted < dtCompare.AddMinutes(this.TimeOfDay) && dtCompare.AddMinutes(this.TimeOfDay) <= DateTime.Now;

                }
                else
                {
                    //Is the LastCompleted date + the Minutes interval less than now?
                    return LastCompleted.AddMinutes(this.Minutes) < DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Call this method BEFORE processing the ScheduledTasks. This will help protect against long running Taskss 
        /// being fired multiple times. Note, it is not absolute protection. App restarts will cause Taskss to look like
        /// they were completed, even if they were not. Again, ScheduledTaskss are helpful...but not 100% reliable
        /// </summary>
        public void UpdateTime()
        {
            this.LastCompleted = DateTime.Now;
            dalScheduledTaskss.SetLastExecuteScheduledTasksDateTime(this.Key, Environment.MachineName, this.LastCompleted);
        }

        public static void SetLastExecuteScheduledTasksDateTime(string key, string servername, DateTime lastexecuted)
        {
            dalScheduledTaskss.SetLastExecuteScheduledTasksDateTime(key, servername, lastexecuted);
        }

        public static DateTime GetLastExecuteScheduledTasksDateTime(string key, string servername)
        {
            return dalScheduledTaskss.GetLastExecuteScheduledTasksDateTime(key, servername);
        } 
         * */
    }
}
