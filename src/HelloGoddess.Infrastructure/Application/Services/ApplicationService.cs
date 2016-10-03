﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloGoddess.Infrastructure.Authorization;
using HelloGoddess.Infrastructure.Runtime.Session;
using HelloGoddess.Infrastructure.Application.Features;

namespace HelloGoddess.Infrastructure.Application.Services
{
    /// <summary>
    /// This class can be used as a base class for application services. 
    /// </summary>
    public abstract class ApplicationService : AbpServiceBase, IApplicationService, IAvoidDuplicateCrossCuttingConcerns
    {

        public static string[] CommonPostfixes = { "AppService", "ApplicationService" };

        /// <summary>
        /// Gets current session information.
        /// </summary>
        public IAbpSession AbpSession { get; set; }
        
        /// <summary>
        /// Reference to the permission manager.
        /// </summary>
        public IPermissionManager PermissionManager { protected get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// </summary>
        public IPermissionChecker PermissionChecker { protected get; set; }

        /// <summary>
        /// Reference to the feature manager.
        /// </summary>
        public IFeatureManager FeatureManager { protected get; set; }

        /// <summary>
        /// Reference to the feature checker.
        /// </summary>
        public IFeatureChecker FeatureChecker { protected get; set; }

        /// <summary>
        /// Gets current session information.
        /// </summary>
        [Obsolete("Use AbpSession property instead. CurrentSession will be removed in future releases.")]
        protected IAbpSession CurrentSession { get { return AbpSession; } }

        /// <summary>
        /// Gets the applied cross cutting concerns.
        /// </summary>
        public List<string> AppliedCrossCuttingConcerns { get; } = new List<string>();

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ApplicationService()
        {
            AbpSession = NullAbpSession.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        protected virtual Task<bool> IsGrantedAsync(string permissionName)
        {
            return PermissionChecker.IsGrantedAsync(permissionName);
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        protected virtual bool IsGranted(string permissionName)
        {
            return PermissionChecker.IsGranted(permissionName);
        }

        /// <summary>
        /// Checks if given feature is enabled for current tenant.
        /// </summary>
        /// <param name="featureName">Name of the feature</param>
        /// <returns></returns>
        protected virtual Task<bool> IsEnabledAsync(string featureName)
        {
            return FeatureChecker.IsEnabledAsync(featureName);
        }

        /// <summary>
        /// Checks if given feature is enabled for current tenant.
        /// </summary>
        /// <param name="featureName">Name of the feature</param>
        /// <returns></returns>
        protected virtual bool IsEnabled(string featureName)
        {
            return FeatureChecker.IsEnabled(featureName);
        }
    }
}
