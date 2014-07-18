using System;
using System.Collections.Generic;
using WealthHealth.Data.Contracts;

namespace WealthHealth.Data
{
    public class DbOpResult : IDbOpResult
    {
        public DbOpResult()
        {
            this.AffectedIndices = new List<int>();
        }

        public bool OperationSuccessStatus { get; set; }

        public List<int> AffectedIndices { get; set; }

        public string Message { get; set; }

        public bool ExceptionOccurred { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExceptionStackTrace { get; set; }

        public string ExceptionInnerMessage { get; set; }

        public string ExceptionInnerStackTrace { get; set; }

        public static DbOpResult CreateFromException(string statusMessage, Exception ex)
        {
            var opStatus = new DbOpResult
            {
                OperationSuccessStatus = false,
                Message = statusMessage,
                AffectedIndices = null
            };

            if (ex != null)
            {
                opStatus.ExceptionMessage = ex.Message;
                opStatus.ExceptionStackTrace = ex.StackTrace;
                opStatus.ExceptionInnerMessage = (ex.InnerException != null) ? ex.InnerException.Message : null;
                opStatus.ExceptionInnerStackTrace = (ex.InnerException != null) ? ex.InnerException.StackTrace : null;
            }

            return opStatus;
        }
    }
}