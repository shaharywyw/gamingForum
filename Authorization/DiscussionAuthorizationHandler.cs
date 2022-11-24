using GamingForum.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace GamingForum.Authorization
{
    public class DiscussionAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Discussion>
    {
        private readonly UserManager<ApplicationUser> userManager;
        public DiscussionAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager=userManager;   
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Discussion resource)
        {
            var applicationUser = await userManager.GetUserAsync(context.User); 

            if((requirement.Name == Operations.Update.Name || requirement.Name == Operations.Delete.Name) && applicationUser == resource.Creator)
            {
                context.Succeed(requirement);
            }

            if(requirement.Name == Operations.Read.Name && applicationUser == resource.Creator)
            {
                context.Succeed(requirement);
            }
        }
    } 
}
