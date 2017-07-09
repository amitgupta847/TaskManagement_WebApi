using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManagement.BusinessServices;

namespace TaskManagement.Web.Api
{
    public class TaskCreatedActionResult : IHttpActionResult
    {
        private readonly TaskManagement.BusinessService.Task _createdTask;
        private readonly HttpRequestMessage _requestMessage;

        public TaskCreatedActionResult(HttpRequestMessage requestMessage,
            TaskManagement.BusinessService.Task createdTask)
        {
            _requestMessage = requestMessage;
            _createdTask = createdTask;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var acceptHeader = _requestMessage.Headers.Accept.FirstOrDefault();
            var mediaType = acceptHeader == null ? null : acceptHeader.MediaType;

            var responseMessage = string.IsNullOrWhiteSpace(mediaType)
                ? _requestMessage.CreateResponse(HttpStatusCode.Created, _createdTask)
                : _requestMessage.CreateResponse(HttpStatusCode.Created, _createdTask, mediaType);

            responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_createdTask);

            return responseMessage;
        }
    }
}