using FilesBackup.Models;

Backup backup = new Backup();

try
{
    backup.StartBackup();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("Backup ended.");
Console.WriteLine("Press any key to finish program.");

Console.ReadKey();