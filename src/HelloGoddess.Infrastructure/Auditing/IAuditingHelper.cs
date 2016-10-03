using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace HelloGoddess.Infrastructure.Auditing
{
    public interface IAuditingHelper
    {
        bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false);

        AuditInfo CreateAuditInfo(MethodInfo method, object[] arguments);
        AuditInfo CreateAuditInfo(MethodInfo method, IDictionary<string, object> arguments);

        void Save(AuditInfo auditInfo);
        Task SaveAsync(AuditInfo auditInfo);
        bool ShouldSaveAudit(MemberInfo methodInvocationTarget);
        object CreateAuditInfo(MemberInfo methodInvocationTarget, object arguments);
    }
}