//using HelloGoddess.Infrastructure.Application.Services;
//using HelloGoddess.Infrastructure.Dependency;
//using Castle.Core;
//using Castle.MicroKernel;

//namespace HelloGoddess.Infrastructure.Runtime.Validation.Interception
//{
//    internal static class ValidationInterceptorRegistrar
//    {
//        public static void Initialize(IIocManager iocManager)
//        {
//            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
//        }

//        private static void Kernel_ComponentRegistered(string key, IHandler handler)
//        {
//            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation))
//            {
//                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(ValidationInterceptor)));
//            }
//        }
//    }
//}