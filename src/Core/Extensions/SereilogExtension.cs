﻿using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;

namespace Serede.CoreApi.Extensions;

public static class SereilogExtension
{
    public static void ConfigureSerilog(IHostBuilder builder)
    {
        builder.UseSerilog((ctx, lc) => lc
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .ReadFrom.Configuration(ctx.Configuration));
    }
}
