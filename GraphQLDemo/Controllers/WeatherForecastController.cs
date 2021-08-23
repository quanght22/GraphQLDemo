using App.Data.Contracts;
using App.Helper;
using App.Models.Settings;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using GraphQLDemo.Base;
using GraphQLDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GraphQLDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDocumentWriter _writer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISchema schema, IDocumentWriter writer)
        {
            _logger = logger;
            _schema = schema;
            _writer = writer;
        }

        private readonly ISchema _schema;



        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {

            var fields = "{ title }";
            var query = new GraphQLQuery()
            {
                Query = Utilities.BuildQuery<ServiceSetting>(typeof(ServiceSetting.NoteService).Name, ServiceSetting.NoteService.NoteById, fields)
            };
            try
            {
                var inputs = query.Variables?.ToInputs();
                var executionOptions = new ExecutionOptions
                {
                    Schema = _schema,
                    Query = query.Query,
                    Inputs = inputs
                };
                var result = await new DocumentExecuter()
                    .ExecuteAsync(executionOptions);
                var json = await _writer.WriteToStringAsync(result);
                var test = json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}
