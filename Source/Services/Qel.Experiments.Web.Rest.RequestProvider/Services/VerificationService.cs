using System.Net;
using Qel.Common.DateTimeUtils;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qel.Ef.DbClient;
using Qel.Experiments.Web.Rest.Domain;
using Qel.Experiments.Web.Rest.RequestProvider.Models;

namespace Qel.Experiments.Web.Rest.RequestProvider;

public class VerificationService(ILogger<VerificationService> logger,
    IHttpClientFactory clientFactory,
    IRequestRepository repo,
    IOptions<VerificationOptions> options
    )
{
    readonly VerificationOptions _options = options.Value;

    readonly IHttpClientFactory _clientFactory = clientFactory;
    readonly ILogger<VerificationService> _logger = logger;
    readonly IRequestRepository _repo = repo;

    /// <summary>
    /// Соответствуют ли паспортные данные информации о физическом лице
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ComparePassportInfo(Person person, Passport passport)
    {
        var httpOps = _options.HttpClientOptions.FirstOrDefault(x => x.Key == "PassportCompare");
        var client = _clientFactory.CreateClient("PassportClient");
        //var strBuilder = new StringBuilder();
        //strBuilder.Append(httpOps!.Host +
        //    (httpOps!.Port is not null ? $":{httpOps.Port}" : string.Empty)  + 
        //    (httpOps!.Host.EndsWith('/') ? string.Empty : "/"))
        //    .Append($"{httpOps.Address}{passport.Serie}/{passport.Number}");
        var urlBuilder = new UriBuilder(
            scheme: httpOps!.Schema,
            host: httpOps.Host,
            port: Convert.ToInt32(httpOps.Port),
            path: $"{httpOps.Address}{passport.Serie}/{passport.Number}",
            extraValue: string.Empty);
        Uri uri = urlBuilder.Uri;
        HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var deserOptions = JsonSerializerOptions.Web;
        var responseObj = JsonSerializer.Deserialize<Person>(responseContent, deserOptions);

        if(responseObj is null)
        {
            return false;
        }
        else
        {
            return responseObj!.FirstName.Equals(person.FirstName) &&
                responseObj!.LastName.Equals(person.LastName) &&
                responseObj!.BirthDate.EqualsDates(person.BirthDate);
        }
    }

    public bool SumVerify(double sum)
        => sum >= _options.MinCreditSum && sum <= _options.MaxCreditSum;

    public bool PeriodVerify(double duration) 
        => duration >= _options.MinCreditDuration && duration <= _options.MaxCreditDuration;

    public bool AgeVerify(int age)
        => age >= _options.MinAge;

    public async Task<bool> BlacklistCheck(Person person, Passport passport)
    {
        var httpOps = _options.HttpClientOptions.FirstOrDefault(x => x.Key == "BlacklistCheck");
        var client = _clientFactory.CreateClient("BlacklistClient");
        var urlBuilder = new UriBuilder(
            scheme: httpOps!.Schema,
            host: httpOps.Host,
            port: Convert.ToInt32(httpOps.Port),
            path: $"{httpOps.Address}{person.FirstName}/{person.LastName}/{passport.Serie}/{passport.Number}",
            extraValue: string.Empty);
        Uri uri = urlBuilder.Uri;
        var response = await client.GetAsync(uri).ConfigureAwait(false);
        if(response.StatusCode == HttpStatusCode.Accepted)
        {
            return true;
        }
        else if(response.StatusCode == HttpStatusCode.NoContent)
        {
            return false;
        }
        else
        {
            _logger.LogWarning("Необрабатываемый StatusCode {sc}", response.StatusCode);
            return false;
        }
    }

    public async Task<bool> CurrentDebtsVerify(Person person, Passport passport)
    {
        var allPersonRequests = await _repo.Get(person.FirstName, person.LastName, passport.Serie, passport.Number);
        var totalCredits = allPersonRequests.Count();
        var totalSum = allPersonRequests.Sum(x => x.Summa);
        return totalSum <= _options.MaxDebtsSum && totalCredits < _options.MaxDebtsCount;
    }

    public async Task AddRequest(FullRequest content)
    {
        await _repo.Add(new Ef.Models.Request()
        {
            Passport = new()
            { Number = content.Passport.Number, Serie = content.Passport.Serie },
            Person = new()
            { FirstName = content.Person.FirstName, LastName = content.Person.LastName, Birthdate = content.Person.BirthDate.ToUniversalTime() },
            Period = content.Request.Period,
            Summa = content.Request.Summa,
        });
    }
}