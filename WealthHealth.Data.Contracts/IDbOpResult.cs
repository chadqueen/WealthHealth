using System.Collections.Generic;

namespace WealthHealth.Data.Contracts
{
    public interface IDbOpResult
    {
        bool OperationSuccessStatus { get; set; }

        List<int> AffectedIndices { get; set; }

        string Message { get; set; }

        bool ExceptionOccurred { get; set; }

        string ExceptionMessage { get; set; }

        string ExceptionStackTrace { get; set; }

        string ExceptionInnerMessage { get; set; }

        string ExceptionInnerStackTrace { get; set; }
    }
}