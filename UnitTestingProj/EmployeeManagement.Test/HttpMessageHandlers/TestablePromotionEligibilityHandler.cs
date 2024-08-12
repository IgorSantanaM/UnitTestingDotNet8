using EmployeeManagement.Business;
using System.Text;
using System.Text.Json;

namespace EmployeeManagement.Test.HttpMessageHandlers
{
    public class TestablePromotionEligibilityHandler : HttpMessageHandler
    {
        private readonly bool _isEligibileForPromotion;

        public TestablePromotionEligibilityHandler(bool isEligibileForPromotion)
        {
            _isEligibileForPromotion = isEligibileForPromotion;         
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var promotionEligibility = new PromotionEligibility()
            {
                EligibleForPromotion = _isEligibileForPromotion
            };
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                
               Content  = new StringContent(
                    JsonSerializer.Serialize(promotionEligibility, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }),
                Encoding.UTF8,
                "application/json")
            };
            return Task.FromResult(response);

        }
    }
}
