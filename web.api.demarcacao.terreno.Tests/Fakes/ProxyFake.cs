using Castle.DynamicProxy;
using FluentValidation.Results;
using patterns.strategy;
using System.Collections.Generic;

namespace web.api.demarcacao.terreno.Tests.Fakes
{
    public class ProxyFake
    {
        public object CreateProxyValidatorFake<TRequest, TResponse>(object classProxy, IList<ValidationFailure> validationFailures)
        {
            var proxyGenerator = new ProxyGenerator();
            var proxy = proxyGenerator.CreateInterfaceProxyWithTarget(typeof(IStrategy<TRequest, TResponse>),
                                                                      classProxy,
                                                                      new ValidatorInterceptor(validationFailures));
            return proxy;
        }
    }
}
