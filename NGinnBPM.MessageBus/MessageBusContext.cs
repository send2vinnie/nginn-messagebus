﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGinnBPM.MessageBus
{
    /// <summary>
    /// This class is used for exposing current message information
    /// </summary>
    public class MessageBusContext
    {
        
        [ThreadStatic]
        private static IMessageBus _curBus;

        /// <summary>
        /// Currently processed message or null if no message is currently processed
        /// </summary>
        public static CurrentMessageInfo CurrentMessage
        {
            get 
            {
                var cb = _curBus;
                return cb == null ? null : cb.CurrentMessageInfo; 
            }
        }

        /// <summary>
        /// Message bus that receives the current message
        /// </summary>
        public static IMessageBus CurrentMessageBus
        {
            get { return _curBus; }
            internal set { _curBus = value; }
        }

        [ThreadStatic]
        private static object _recvCon;
        [ThreadStatic]
        private static object _appCon;

        /// <summary>
        /// Database connection used for receiving current message
        /// This property is transport-specific, for sql
        /// transport it will be an SqlConnection object.
        /// It will be set only when processing an incoming message.
        /// This is an advanced functionality, should be used only because of specific performance reasons
        /// </summary>
        public static object ReceivingConnection
        {
            get
            {
                return _recvCon;
            }
            internal set
            {
                _recvCon = value;
            }
        }

        /// <summary>
        /// This property can be used to pass application's
        /// db connection to NGinn.MessageBus. MessageBus 
        /// can use it for sending messages in order to improve performance
        /// and to avoid initiating a distributed transaction.
        /// It depends on message bus configuration if this
        /// connection will be used or not. For sql transport this
        /// property should contain a SqlConnection object.
        /// The application is responsible for clearing this property
        /// when the connection is closed.
        /// This is an advanced functionality, should be used only because of specific performance reasons
        /// </summary>
        public static object AppManagedConnection
        {
            get { return _appCon; }
            set { _appCon = value; }
        }
            
    }
}
