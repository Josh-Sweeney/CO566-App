using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace gha.mobile.common.resolver
{
    public static class Resolver
    {
        private static IContainer container;

        public static void Initialize(IContainer container)
        {
            Resolver.container = container;
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
