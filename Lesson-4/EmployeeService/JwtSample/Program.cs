
using EmployeeService.Utils;
using JwtSample;

bool isLoop = true;
UserServices userServices = new UserServices();

while (isLoop)
{
    Console.WriteLine("1: Jwt Test.");
    Console.WriteLine("2: Reg Account.");
    int userEnter = 0;
    int.TryParse(Console.ReadLine(), out userEnter);

    switch (userEnter)
    {
        case 1:
            Console.WriteLine("User name - ");
            string userName = Console.ReadLine();
            Console.WriteLine("User password - ");
            string userPassword = Console.ReadLine();
            Console.WriteLine(userServices.Login(userName, userPassword));
            break;
        case 2:
            Console.WriteLine(PasswordUtils.CreatePasswordHash(Console.ReadLine()));
            break;
        case 0:
            isLoop = false;
            break;
        default:
            break;
    }

    Console.WriteLine("Press any key...");
    Console.ReadLine();
}

