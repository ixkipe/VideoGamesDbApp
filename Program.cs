string connectionString = File.ReadAllText("C:/Users/user/Documents/postgres_login.txt");
var menu = new DbGameAdminMenu(new(connectionString));
await menu.AddGames();