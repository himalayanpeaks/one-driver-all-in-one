﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.Framework.Libs.Announcer
{
    public abstract class DataTunnel<TEventArgs> where TEventArgs : BaseDataForAnnouncement, new()
    {
        public delegate void DataEventHandler(object sender, TEventArgs e);
        public event DataEventHandler DataEvent;
        private CancellationToken AnnouncerCancellationToken { get; set; }
        private CancellationTokenSource AnnouncerCancellationTokenSource { get; set; }
        private Task DataTask { get; set; }
        private TEventArgs? PreviousData { get; set; }
        private void Announcer()
        {
            while (AnnouncerCancellationToken.IsCancellationRequested == false)
            {
                FetchDataForTunnel(out var newData);
                if (!newData.Equals(PreviousData))
                {
                    DataEvent?.Invoke(this, newData);
                    PreviousData = new TEventArgs();
                    PreviousData = newData;
                }
            }
            AnnouncerCancellationTokenSource.Dispose();
        }
        protected DataTunnel()
        {
            DataTask = new Task(Announcer);
            AnnouncerCancellationTokenSource = new CancellationTokenSource();
            AnnouncerCancellationToken = AnnouncerCancellationTokenSource.Token;
        }
        protected void StartAnnouncingData()
        {
            if (DataTask.Status == TaskStatus.Running)
                return;
            DataTask = new Task(Announcer);
            AnnouncerCancellationTokenSource = new CancellationTokenSource();
            AnnouncerCancellationToken = AnnouncerCancellationTokenSource.Token;
            DataTask.Start();
        }

        protected void StopAnnouncingData()
        {
            if (DataTask.Status == TaskStatus.Running) AnnouncerCancellationTokenSource.Cancel();
        }
        protected abstract void FetchDataForTunnel(out TEventArgs data);
    }
}
