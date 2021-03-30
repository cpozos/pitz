
namespace Pitz.App
{
   public record Response<T>(T Data, string Message, bool WithError);

   public static class Response
   {
      public static Response<T> Fail<T>(string message, T data = default) =>
         new Response<T>(data, message, true);

      public static Response<T> Ok<T>(T data = default, string message = null) =>
         new Response<T>(data, message, false);
   }
}
