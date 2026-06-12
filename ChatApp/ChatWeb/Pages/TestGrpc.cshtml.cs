using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales; // Proto namespace

namespace MyApp.Namespace
{
    public class TestGrpcModel : PageModel
    {
        private readonly UserService.UserServiceClient _userClient;

        public List<UserMsg> Users { get; set; } = new();

        public TestGrpcModel(UserService.UserServiceClient userClient)
        {
            _userClient = userClient;
        }

        public async Task OnGet()
        {
            // Call ServerApp → GetAllUsers
            var response = await _userClient.GetAllUsersAsync(new Google.Protobuf.WellKnownTypes.Empty());
            Users = response.Users.ToList();
        }
    }
}
