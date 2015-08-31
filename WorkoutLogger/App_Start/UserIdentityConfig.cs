using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using WorkoutLogger.Models;

namespace WorkoutLogger
{
    public class UserSignInManager : SignInManager<User, string>
    {
        public UserSignInManager(UserManager<User, string> userManager, IAuthenticationManager authenticationManager) 
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManager) UserManager);
        }

        public static UserSignInManager Create(IdentityFactoryOptions<UserSignInManager> options, IOwinContext context)
        {
            return new UserSignInManager(context.GetUserManager<UserManager>(), context.Authentication);
        }
    }

    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store) 
            : base(store)
        {
        }

        public static UserManager CreateManger(IdentityFactoryOptions<UserManager> options, IOwinContext context)
        {
            var userManager = new UserManager(new UserStore<User>(context.Get<IdentityDbContext<User>>()));

            userManager.UserValidator = new UserValidator<User>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                userManager.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return userManager;
        }
    }
}