global using System;
global using System.IO;
global using System.Text;
global using System.Net.Http;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using Xunit;
global using System.Text.Json;
global using System.Text.Json.Nodes;
global using Testcontainers.PostgreSql;
global using DotNet.Testcontainers.Builders;
global using DotNet.Testcontainers.Networks;

global using Kernel.Lib.Constant;
global using DynamicSearch.EfCore.Model;

global using Core.Api;
global using Core.Application.Model;
global using Core.Application.Command;
global using Core.Application.Service.Interface;

global using DynamicSearch.Test.Integration;