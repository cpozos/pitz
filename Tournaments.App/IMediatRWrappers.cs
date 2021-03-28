using MediatR;

namespace Tournaments.App
{
   public interface IRequestWrapper<TResponse> : IRequest<Response<TResponse>> { }
   public interface IHandlerWrapper<TRequest, TResponse> : IRequestHandler<TRequest, Response<TResponse>>
      where TRequest : IRequestWrapper<TResponse>
   {

   }
}
