using chatapi.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Conversations.V1;
using Twilio.Rest.Conversations.V1.Conversation;

namespace chatapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        string accountSid = "AC353569979ca23711777b6e187f4c98ee";
        string authToken = "7c7fb77b9fbcea14c711ffc4448500fb";

        [HttpPost]
        public IActionResult CreateChannel([FromBody] CreateChannelRequest request)
        {
            TwilioClient.Init(accountSid, authToken);

            var conversation = ConversationResource.Create(
                friendlyName: request.FriendlyName,
                uniqueName: request.UniqueName
            );

            var result = new
            {
                Sid = conversation.Sid,
                FriendlyName = conversation.FriendlyName,
                UniqueName = conversation.UniqueName,
                AccountSid = conversation.AccountSid,
                ChatServiceSid = conversation.ChatServiceSid,
                MessagingServiceSid = conversation.MessagingServiceSid,
                DateCreated = conversation.DateCreated,
                DateUpdated = conversation.DateUpdated,
                Url = conversation.Url,
                State = conversation.State,
                Links = conversation.Links,
                Timers = conversation.Timers,
                Attributes = conversation.Attributes,
                Bindings = conversation.Bindings
            };
            return Ok(result);
        }

        [HttpGet("{sid}")]
        public IActionResult GetConverastion([FromRoute]string sid)
        {
            TwilioClient.Init(accountSid, authToken);
            var conversation = ConversationResource.Fetch(pathSid: sid);
            var result = new
            {
                Sid = conversation.Sid,
                FriendlyName = conversation.FriendlyName,
                UniqueName = conversation.UniqueName,
                AccountSid = conversation.AccountSid,
                ChatServiceSid = conversation.ChatServiceSid,
                MessagingServiceSid = conversation.MessagingServiceSid,
                DateCreated = conversation.DateCreated,
                DateUpdated = conversation.DateUpdated,
                Url = conversation.Url,
                State = conversation.State,
                Links = conversation.Links,
                Timers = conversation.Timers,
                Attributes = conversation.Attributes,
                Bindings = conversation.Bindings
            };
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetConverastions([FromQuery] GetConversionsRequest request)
        {
            TwilioClient.Init(accountSid, authToken);
            var conversations = ConversationResource.Read(limit: 20);
            var results = conversations.Select(x => new
            {
                Sid = x.Sid,
                FriendlyName = x.FriendlyName,
                UniqueName = x.UniqueName,
                AccountSid = x.AccountSid,
                ChatServiceSid = x.ChatServiceSid,
                MessagingServiceSid = x.MessagingServiceSid,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                Url = x.Url,
                State = x.State,
                Links = x.Links,
                Timers = x.Timers,
                Attributes = x.Attributes,
                Bindings = x.Bindings
            });
            return Ok(results);
        }

        [HttpPut("{sid}")]
        public IActionResult UpdateConverastion([FromRoute] string sid, string friendlyName)
        {
            TwilioClient.Init(accountSid, authToken);
            var conversation = ConversationResource.Update(
            friendlyName: friendlyName,
            pathSid: sid
             );
            var result = new
            {
                Sid = conversation.Sid,
                FriendlyName = conversation.FriendlyName,
                UniqueName = conversation.UniqueName,
                AccountSid = conversation.AccountSid,
                ChatServiceSid = conversation.ChatServiceSid,
                MessagingServiceSid = conversation.MessagingServiceSid,
                DateCreated = conversation.DateCreated,
                DateUpdated = conversation.DateUpdated,
                Url = conversation.Url,
                State = conversation.State,
                Links = conversation.Links,
                Timers = conversation.Timers,
                Attributes = conversation.Attributes,
                Bindings = conversation.Bindings
            };

            return Ok(result);
        }

        [HttpDelete("{sid}")]
        public IActionResult DeleteConverastion([FromRoute] string sid)
        {
            TwilioClient.Init(accountSid, authToken);
            ConversationResource.Delete(pathSid: sid);
            return Ok("Deleted succesfully");
        }

        [HttpPost("channels/{sid}")]
        public IActionResult CreateConversation([FromRoute] string sid,[FromBody]string body)
        {
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
            author: "Nuru",
            body: body,
            pathConversationSid: sid);

            var result = new
            {
                Sid = message.Sid,
                AccountSid = message.AccountSid,
                ConversationSid = message.ConversationSid,
                ParticipantSid = message.ParticipantSid,
                Body = message.Body,
                Media = message.Media,
                Author = message.Author,
                Index = message.Index,
                Delivery = message.Delivery,
                DateCreated = message.DateCreated,
                DateUpdated = message.DateUpdated,
                Url = message.Url,
                Links = message.Links,
                Attributes = message.Attributes,
                ContentSid = message.ContentSid,
            };
            return Ok(result);
        }

        [HttpGet("channels/{conversationSid}/Messages/{sid}")]
        public IActionResult GetConversation([FromRoute] string conversationSid, [FromRoute] string sid)
        {
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Fetch(
            pathConversationSid: conversationSid,
            pathSid: sid);

            var result = new
            {
                Sid = message.Sid,
                AccountSid = message.AccountSid,
                ConversationSid = message.ConversationSid,
                ParticipantSid = message.ParticipantSid,
                Body = message.Body,
                Media = message.Media,
                Author = message.Author,
                Index = message.Index,
                Delivery = message.Delivery,
                DateCreated = message.DateCreated,
                DateUpdated = message.DateUpdated,
                Url = message.Url,
                Links = message.Links,
                Attributes = message.Attributes,
                ContentSid = message.ContentSid,
            };
            return Ok(result);
        }

        [HttpGet("channels/{conversationSid}/Messages")]
        public IActionResult GetConversations([FromRoute] string conversationSid)
        {
            TwilioClient.Init(accountSid, authToken);

            var messages = MessageResource.Read(
            order: MessageResource.OrderTypeEnum.Desc,
            pathConversationSid: conversationSid,
            limit: 20);

            var results = messages.Select(message => new
            {
                Sid = message.Sid,
                AccountSid = message.AccountSid,
                ConversationSid = message.ConversationSid,
                ParticipantSid = message.ParticipantSid,
                Body = message.Body,
                Media = message.Media,
                Author = message.Author,
                Index = message.Index,
                Delivery = message.Delivery,
                DateCreated = message.DateCreated,
                DateUpdated = message.DateUpdated,
                Url = message.Url,
                Links = message.Links,
                Attributes = message.Attributes,
                ContentSid = message.ContentSid,
            });
            return Ok(results);
        }

        [HttpPut("channels/{conversationSid}/Messages/{sid}")]
        public IActionResult UpdateConversation([FromRoute] string conversationSid, [FromRoute] string sid, string body)
        {
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Update(
            author: "regretfulUser",
            body: body,
            pathConversationSid: conversationSid,
            pathSid: sid);

            var result = new
            {
                Sid = message.Sid,
                AccountSid = message.AccountSid,
                ConversationSid = message.ConversationSid,
                ParticipantSid = message.ParticipantSid,
                Body = message.Body,
                Media = message.Media,
                Author = message.Author,
                Index = message.Index,
                Delivery = message.Delivery,
                DateCreated = message.DateCreated,
                DateUpdated = message.DateUpdated,
                Url = message.Url,
                Links = message.Links,
                Attributes = message.Attributes,
                ContentSid = message.ContentSid,
            };
            return Ok(result);
        }


        [HttpDelete("channels/{conversationSid}/Messages/{sid}")]
        public IActionResult DeleteConversation([FromRoute] string conversationSid, [FromRoute] string sid)
        {
            TwilioClient.Init(accountSid, authToken);

            MessageResource.Delete(
            pathConversationSid: conversationSid,
            pathSid: sid);

            return Ok("Deleted");
        }

    }
}
