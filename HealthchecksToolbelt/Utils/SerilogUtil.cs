﻿using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace HealthchecksToolbelt.Utils;

public class SerilogUtil
{
    public SerilogUtil()
    {
    }

    public ILogger CreateLogger()
    {
        return new LoggerConfiguration().WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
        {
            FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
            EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                               EmitEventFailureHandling.WriteToFailureSink |
                               EmitEventFailureHandling.RaiseCallback
        }).CreateLogger();
    }
}