using TaskManagerSystem.Common.Enums;

namespace TaskManagerSystem.Common.Errors
{
    public sealed class Error(ResultCode resultCode, string? message)
    {
        public ResultCode Result { get; } = resultCode;
        public string? Message { get; } = message;
        public static Error None() => new(ResultCode.Success, null);
    }
}
