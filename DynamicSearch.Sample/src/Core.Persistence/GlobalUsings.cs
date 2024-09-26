global using System;
global using System.Linq;
global using System.Data;
global using System.Dynamic;
global using System.Threading.Tasks;
global using System.Collections.Generic;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using Npgsql;
global using Dapper;

global using GenericRepository.Lib;

global using Core.Domain.Entity;
global using Core.Persistence.Context;
global using Core.Persistence.Repository;
global using Core.Persistence.Configuration;
global using Core.Application.Extension;
global using Core.Application.Repository.Interface;