﻿namespace TaskManagerSystem.Common.Enums
{
    public enum ResultCode
    {
        Success = 200,
        Forbidden = 403,
        NotFound = 404,
        BadRequest = 400,
        UnAuthorize = 401,
        Conflict = 409,
        ServerError = 500
    }
}
