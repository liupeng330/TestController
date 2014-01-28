using System.Security.Principal;
using TestTechnology.Shared.DTO;

namespace Utils
{
    public static class EnumMapping
    {
        public static string ToAssignmentStatus(JobAssignmentStatus status)
        {
            switch (status)
            {
                case JobAssignmentStatus.Completed:
                    return "Completed";
                case JobAssignmentStatus.Running:
                    return "Running";
                case JobAssignmentStatus.UnTaken:
                    return "UnTaken";
                default:
                    return "Error";
            }
        }

        public static string ToAssignmentStatus(int statusInt)
        {
            return ToAssignmentStatus((JobAssignmentStatus) statusInt);
        }

        public static string ToAssigmentResult(JobAssignmentResult result)
        {
            switch (result)
            {
                case JobAssignmentResult.Pass:
                    return "Pass";
                case JobAssignmentResult.Fail:
                    return "Fail";
                case JobAssignmentResult.NotRun:
                    return "NotRun";
                default:
                    return "Error";
            }
        }

        public static string ToAssigmentResult(int resultInt)
        {
            return ToAssigmentResult((JobAssignmentResult) resultInt);
        }
    }
}
