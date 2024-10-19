using UtilityApp;
using UtilityApp.Models;

string format = string.Empty, path = String.Empty;

Common com = new Common();
List<UserModel> userList = new List<UserModel>();
try
{
    userList = com.GetApiData();

    if (userList != null && userList.Count > 0)
    {
        Console.WriteLine("Enter Format like json or csv: ");
        format = Console.ReadLine();
        if(string.IsNullOrEmpty(format))
        {
            throw new Exception("Format required.");
        }
        Console.WriteLine("Enter path where to store: ");
        path = Console.ReadLine();
        if (string.IsNullOrEmpty(path))
        {
            throw new Exception("Path required.");
        }

        com.GenerateFile(userList, path, format);

        Console.WriteLine("Total Number of Users : " + userList.Count);
        Console.WriteLine("File stored in UserFolder at the path provided by you. ");
    }
    else
    {
        Console.WriteLine("No Data Found!!");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Something went wrong :" + ex.Message);
}
