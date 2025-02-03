using pgSQL;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class DatabaseInitialization
{
    public static void CreateAndSeed(WebApplication app)
    {
        for (int i = 0; i < 10; i++)
        {
            //try to migrate and seed the database
            try
            {
                var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                db.Database.Migrate();
                

            }
            //if an exception is thrown, catch it and print it to the console
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("\n\nIt seems database isn't ready yet...");
                Console.WriteLine("waitint for the databade to start...\n\n");

                //The thread will sleep for 5 seconds before retrying, due to the database not being ready.
                //I need to ensure that docker container is up and running before 
                //migrating and seeding the database.

                Thread.Sleep(5000);
                Console.WriteLine("Retrying...\n\n");
            }
        }
    }
}