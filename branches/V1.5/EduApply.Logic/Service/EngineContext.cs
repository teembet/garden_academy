﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Mvc;

namespace EduApply.Logic.Service
{
    public class EngineContext
    {
        public static ILifetimeScope Current
        {
            get
            {
                return AutofacDependencyResolver.Current.RequestLifetimeScope;
            }
        }

        public static T Resolve<T>(string key = "") where T : class
        {

            if (string.IsNullOrEmpty(key))
            {
                return Current.Resolve<T>();
            }
            return Current.ResolveKeyed<T>(key);
        }

        public static object Resolve(Type type)
        {
            return Current.Resolve(type);
        }
    }
}
