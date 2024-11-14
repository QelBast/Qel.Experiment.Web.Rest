using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Qel.Api.Transport;
using Qel.Api.Transport.Behaviours;
using Qel.Common.DateTimeUtils;
using Qel.Ef.DbClient;
using Qel.Experiments.Web.Rest.Domain;

namespace Qel.Experiments.Web.Rest.RequestProvider;

public class RequestProcessor(ILogger<RequestProcessor> logger,
VerificationService verification
) : IProcessBehaviour
{
    readonly ILogger<RequestProcessor> _logger = logger;
    readonly VerificationService _verification = verification;

    public async Task<BaseMessage?> Process(BaseMessage? inObj)
    {
        string resultContent = string.Empty;
        try
        {
            FullRequest? content = null;
            try
            {
                var options = JsonSerializerOptions.Web;
                content = JsonSerializer.Deserialize<FullRequest>(inObj?.Content!, options)!;
                if (content == null)
                {
                    throw new NullReferenceException("Объект == null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка десериализации!\n{mes}\n{inner}", ex.Message, ex.InnerException?.Message);
            }

            try
            {
                bool success = false;
                success = await _verification.ComparePassportInfo(content!.Person, content!.Passport);
                success = success && _verification.SumVerify(content!.Request.Summa);
                success = success && _verification.PeriodVerify(content!.Request.Period);
                success = success && _verification.AgeVerify(AgeUtils.GetAge(content!.Person.BirthDate));
                bool isInBlacklist = await _verification.BlacklistCheck(content!.Person, content!.Passport);
                success = success && !isInBlacklist;
                bool isGoodCreditHistory = await _verification.CurrentDebtsVerify(content!.Person, content!.Passport);
                success = success && isGoodCreditHistory;
                if(success)
                {
                    resultContent = HttpStatusCode.OK.ToString();
                    await _verification.AddRequest(content);
                }
                else
                {
                    resultContent = HttpStatusCode.Conflict.ToString();
                }
                return new BaseMessage(resultContent);
            }
            catch
            {
                _logger.LogError("Ошибка проверок");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Ошибка сервиса проверок\n{mes}\n{inner}", ex.Message, ex.InnerException?.Message);
        }

        resultContent = HttpStatusCode.InternalServerError.ToString();
        return new BaseMessage(resultContent);
    }
}