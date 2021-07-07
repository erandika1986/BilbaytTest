using Autofac;
using Bilbayt.Business;
using Bilbayt.Business.Interfaces;
using Bilbayt.Data;
using Bilbayt.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.WebApi.Infrastructure
{
  public class ApplicationModule : Autofac.Module
  {
    public ApplicationModule()
    {

    }


    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<HttpContextAccessor>()
          .As<IHttpContextAccessor>()
          .SingleInstance();

      builder.RegisterType<SendGridEmailManager>()
        .As<ISendGridEmailManager>()
        .SingleInstance();

      builder.RegisterType<IdentityService>()
        .As<IIdentityService>()
        .InstancePerLifetimeScope();

      builder.RegisterType<UserContext>()
        .As<IUserContext>()
        .InstancePerLifetimeScope();

      builder.RegisterType<AccountService>()
        .As<IAccountService>()
        .InstancePerLifetimeScope();

      builder.RegisterType<UserService>()
        .As<IUserService>()
        .InstancePerLifetimeScope();
    }
  }
}
