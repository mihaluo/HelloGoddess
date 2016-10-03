//using System.Reflection;
//using Abp.Modules;
//using Abp.MongoDb.Configuration;
//using HelloGoddess.Core.Repositories.MongoDb.Configuration;

//namespace Abp.MongoDb
//{
//    /// <summary>
//    /// This module is used to implement "Data Access Layer" in MongoDB.
//    /// </summary>
//    [DependsOn(typeof(AbpKernelModule))]
//    public class AbpMongoDbModule : AbpModule
//    {
//        public override void PreInitialize()
//        {
//            IocManager.Register<IMongoDbModuleConfiguration, MongoDbModuleConfiguration>();
//        }

//        public override void Initialize()
//        {
//            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
//        }
//    }
//}
