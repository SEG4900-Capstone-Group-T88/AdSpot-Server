﻿global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.Linq.Expressions;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text.Json.Serialization;
global using AdSpot.Api;
global using AdSpot.Api.Mutations.Errors;
global using AdSpot.Api.Mutations.Inputs;
global using AdSpot.Api.Mutations.Payloads;
global using AdSpot.Api.Repositories;
global using AdSpot.Api.Services;
global using AdSpot.Api.Subscriptions;
global using AdSpot.Api.Utils;
global using AdSpot.Api.Validators;
global using AdSpot.Extensions;
global using AdSpot.Models;
global using AppAny.HotChocolate.FluentValidation;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using HotChocolate.Authorization;
global using HotChocolate.Data;
global using HotChocolate.Data.Filters;
global using HotChocolate.Data.Filters.Expressions;
global using HotChocolate.Execution;
global using HotChocolate.Language;
global using HotChocolate.Subscriptions;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;
