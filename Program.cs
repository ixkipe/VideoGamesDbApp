string connectionString = File.ReadAllText("C:/Users/user/Documents/connectionstringforMSSQLServer.txt");
var menu = new DbGameAdminMenu(new(connectionString));
await menu.AddGame();