namespace Lesson01_API.Exceptions
{
    public static class ErrorCodes
    {
        public static Error EntityNotFound(string entityName, object key)
        {
            return new Error($"{entityName} with ID {key} was not found.", System.Net.HttpStatusCode.NotFound, $"{entityName}Id");
        }
        public static Error ValidationError(string message, string key = "General")
        {
            return new Error(message, System.Net.HttpStatusCode.BadRequest, key);
        }
        public static Error InternalServerError(string message)
        {
            return new Error(message, System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
