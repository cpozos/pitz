namespace Users.Domain
{
   public class DataResponse<T> : Response
   {
      public T Data { get; private set; }
      public DataResponse(bool succeed, T data = default(T), string errors = null) : base(succeed, errors)
      {
         Data = data;
      }
   }

   public class Response
   {
      public bool Succeed { get; private set; }
      public string Errors { get; private set; }
      public Response(bool succeed, string errors = null)
      {
         Succeed = succeed;
         Errors = errors;
      }
   }
}
