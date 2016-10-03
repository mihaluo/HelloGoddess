﻿using System;

namespace HelloGoddess.Infrastructure.Notifications
{
    /// <summary>
    /// Can be used to store a simple message as notification data.
    /// </summary>
    
    public class MessageNotificationData : NotificationData
    {
        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Needed for serialization.
        /// </summary>
        private MessageNotificationData()
        {
            
        }

        public MessageNotificationData(string message)
        {
            Message = message;
        }
    }
}