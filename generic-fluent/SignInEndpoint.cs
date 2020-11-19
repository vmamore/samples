using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace generic_fluent
{
    public class SignInEndpoint : Endpoint
        .WithRequest<SignInRequest>
        .WithResponse<SignInResponse>
    {
        public override Task<ActionResult<SignInResponse>> ExecuteAsync(SignInRequest request, 
        CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MyEndpointWithoutResponse
    : Endpoint.WithRequest<SignInRequest>.WithoutResponse
    {
        public override Task<ActionResult> ExecuteAsync(SignInRequest request, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}