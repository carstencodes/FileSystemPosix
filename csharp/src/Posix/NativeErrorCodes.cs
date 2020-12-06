/*
 * NativeErrorCodes.cs - (C) 2020 by Carsten Igel
 * 
 * Published using the MIT License
 */

namespace Posix.FileSystem.Permission.Posix
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    internal static class NativeErrorCodes
    {
        private const ushort NoError = 0;

        private const ushort ParameterIsNull = 1;

        private const ushort StringConversionError = 2;

        private const ushort FsErrorOffSet = 0x1000;

        private const ushort IoErrorOffset = 0x2000;

        private const ushort NumericConversionOffset = 0x4000;

        private const ushort IoErrorNotFound = 0;

        private const ushort IoErrorPermissionDenied = 1;

        private const ushort IoErrorBrokenPipe = 2;

        private const ushort IoErrorInvalidInput = 3;

        private const ushort IoErrorInvalidData = 4;

        private const ushort IoErrorInterrupted = 5;

        private const ushort IoErrorUnexpectedEof = 6;

        private const ushort IoErrorTimedOut = 7;

        private const ushort IoErrorUndefinedOrOther = 64;

        private const string UnknownErrorCode = "Error code {0:X} is not known";

        private const string NumericConversionError = "Failed to convert a numeric value internally.";

        private static IReadOnlyDictionary<ushort, string> BackendErrorMessages { get; }
            = new ReadOnlyDictionary<ushort, string>(new Dictionary<ushort, string>
            {
                { ParameterIsNull, "" },
                { StringConversionError, "" },
            });
        private static IReadOnlyDictionary<ushort, string> IoErrorMessages { get; }
            = new ReadOnlyDictionary<ushort, string>(new Dictionary<ushort, string>
            {
                { IoErrorOffset + IoErrorNotFound, "" },
                { IoErrorOffset + IoErrorPermissionDenied, "" },
                { IoErrorOffset + IoErrorBrokenPipe, "" },
                { IoErrorOffset + IoErrorInvalidInput, "" },
                { IoErrorOffset + IoErrorInvalidData, "" },
                { IoErrorOffset + IoErrorInterrupted, "" },
                { IoErrorOffset + IoErrorUnexpectedEof, "" },
                { IoErrorOffset + IoErrorTimedOut, "" },
                { IoErrorOffset + IoErrorUndefinedOrOther, "" },
            });

        internal static bool IsPermissionDenied(this ushort value)
        {
            return value == IoErrorOffset + IoErrorPermissionDenied;
        }

        internal static bool IsInterrupted(this ushort value)
        {
            return value == IoErrorOffset + IoErrorInterrupted;
        }

        internal static bool IsError(this ushort value)
        {
            return value != NoError;
        }

        internal static string GetErrorMessage(ushort errorCode)
        {
            if (!errorCode.IsError())
            {
                return string.Empty;
            }

            if ((errorCode & 0x0) == 0x0)
            {
                if (BackendErrorMessages.TryGetValue(errorCode, out string errorMessage))
                {
                    return errorMessage;
                }
            }
            else if ((errorCode & FsErrorOffSet) == FsErrorOffSet)
            {
                ushort localErrorCode = (ushort)(errorCode - FsErrorOffSet);
                if (FsErrorValues.Values.TryGetValue(localErrorCode, out FsError error)
                    && FsErrorValues.Texts.TryGetValue(error, out string errorMessage))
                {
                    return errorMessage;
                }
            }
            else if ((errorCode & IoErrorOffset) == IoErrorOffset)
            {
                ushort localErrorCode = (ushort)(errorCode - IoErrorOffset);
                if (IoErrorMessages.TryGetValue(errorCode, out string errorMessage))
                {
                    return errorMessage;
                }
            }
            else if ((errorCode & NumericConversionOffset) == NumericConversionOffset)
            {
                return NumericConversionError;
            }

            return string.Format(UnknownErrorCode, errorCode);
        }
    }
}