using System;
using System.Data.SqlClient;

namespace Treinamento.Banco.Console
{
    public static class Usuario
    {
        public static readonly string Id = "Id";
        public static readonly string Nome = "Nome";
        public static readonly string Idade = "Idade";
        public static readonly string Email = "Email";
        public static readonly string Nickname = "Nickname";
        public static readonly string Cidade = "Cidade";
    }

    public class Program
    {
        private static void Main()
        {
            var loop = true;

            do
            {
                System.Console.Clear();

                System.Console.WriteLine("--- Opcoes ---\n");

                System.Console.WriteLine("1. (C)reate");
                System.Console.WriteLine("2. (R)ead");
                System.Console.WriteLine("3. (U)pdate");
                System.Console.WriteLine("4. (D)elete");

                System.Console.WriteLine("\n");

                System.Console.WriteLine("0. Sair");

                System.Console.WriteLine("\n");

                System.Console.Write("Opcao: ");

                var option = System.Console.ReadKey();

                switch (option.Key)
                {
                    case ConsoleKey.C:
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        System.Console.Clear();
                        Create();
                        break;

                    case ConsoleKey.R:
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        System.Console.Clear();
                        Read();
                        break;

                    case ConsoleKey.U:
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        System.Console.Clear();
                        Update();
                        break;

                    case ConsoleKey.D:
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        System.Console.Clear();
                        Delete();
                        break;

                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        loop = false;
                        break;

                    default:
                        System.Console.Clear();
                        System.Console.WriteLine("Opcao Inválida! Tente novamente.\n");
                        break;
                }
            } while (loop);
        }

        private static void Create()
        {
            try
            {
                System.Console.WriteLine("--- CREATE ---\n");

                System.Console.Write("Nome....: ");
                string nome = System.Console.ReadLine();

                System.Console.Write("Idade...: ");
                int idade = Convert.ToInt32(System.Console.ReadLine());

                System.Console.Write("Email...: ");
                string email = System.Console.ReadLine();

                System.Console.Write("Nickname: ");
                string nickname = System.Console.ReadLine();

                System.Console.Write("Cidade..: ");
                string cidade = System.Console.ReadLine();

                System.Console.Write("\nDeseja realmente adicionar novo usuário? (y/n): ");
                var option = System.Console.ReadKey();

                if (option.Key == ConsoleKey.N)
                    return;

                using (var connection = new SqlConnection(Properties.Settings.Default.DefaultConnectionString))
                {
                    connection.Open();

                    const string sql = "INSERT INTO Usuario (Nome, Idade, Email, Nickname, Cidade) VALUES (@Nome, @Idade, @Email, @Nickname, @Cidade)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Nome", nome));
                        command.Parameters.Add(new SqlParameter("@Idade", idade));
                        command.Parameters.Add(new SqlParameter("@Email", email));
                        command.Parameters.Add(new SqlParameter("@Nickname", nickname));
                        command.Parameters.Add(new SqlParameter("@Cidade", cidade));

                        int result = command.ExecuteNonQuery();

                        System.Console.WriteLine(result > 0
                            ? $"\nNovo usuário {nome} foi criado com sucesso."
                            : "\nNenhum usuário foi criado.");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            System.Console.WriteLine("\n\nAperte qualquer tecla para continuar...");
            System.Console.ReadKey();
        }

        private static void Read()
        {
            try
            {
                System.Console.WriteLine("--- READ ---\n");

                using (var connection = new SqlConnection(Properties.Settings.Default.DefaultConnectionString))
                {
                    connection.Open();

                    // {Nome} tem {Idade} anos e mora em {Cidade}.

                    var cmd = new SqlCommand($"SELECT {Usuario.Nome}, {Usuario.Idade}, {Usuario.Cidade} FROM Usuario", connection);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                            System.Console.WriteLine("Tabela vazia.");

                        while (reader.Read())
                        {
                            string message = $"{reader[Usuario.Nome]} tem {reader[Usuario.Idade]} anos e mora em {reader[Usuario.Cidade]}";
                            System.Console.WriteLine(message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            System.Console.WriteLine("\n\nAperte qualquer tecla para continuar...");
            System.Console.ReadKey();
        }

        private static void Update()
        {
            try
            {
                System.Console.WriteLine("--- UPDATE ---\n");

                ListarTodos();

                System.Console.WriteLine("\n");

                System.Console.Write("Id......: ");
                int id = Convert.ToInt32(System.Console.ReadLine());

                System.Console.Write("Nome....: ");
                string nome = System.Console.ReadLine();

                System.Console.Write("Idade...: ");
                int idade = Convert.ToInt32(System.Console.ReadLine());

                System.Console.Write("Email...: ");
                string email = System.Console.ReadLine();

                System.Console.Write("Nickname: ");
                string nickname = System.Console.ReadLine();

                System.Console.Write("Cidade..: ");
                string cidade = System.Console.ReadLine();

                System.Console.Write($"\nDeseja realmente atualizar usuário Id = {id}? (y/n): ");
                var option = System.Console.ReadKey();

                if (option.Key == ConsoleKey.N)
                    return;

                using (var connection = new SqlConnection(Properties.Settings.Default.DefaultConnectionString))
                {
                    connection.Open();

                    string sql = $@"UPDATE Usuario
                                    SET {Usuario.Nome} = @Nome,
                                        {Usuario.Idade} = @Idade,
                                        {Usuario.Email} = @Email,
                                        {Usuario.Nickname} = @Nickname,
                                        {Usuario.Cidade} = @Cidade
                                    WHERE {Usuario.Id} = @Id";

                    var command = new SqlCommand(sql, connection);

                    command.Parameters.Add(new SqlParameter("@Id", id));
                    command.Parameters.Add(new SqlParameter("@Nome", nome));
                    command.Parameters.Add(new SqlParameter("@Idade", idade));
                    command.Parameters.Add(new SqlParameter("@Email", email));
                    command.Parameters.Add(new SqlParameter("@Nickname", nickname));
                    command.Parameters.Add(new SqlParameter("@Cidade", cidade));

                    int result = command.ExecuteNonQuery();

                    System.Console.WriteLine(result > 0
                        ? $"\nUsuário Id = {id} foi atualizado."
                        : "\nNenhum usuário foi atualizado.");
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            System.Console.WriteLine("\n\nAperte qualquer tecla para continuar...");
            System.Console.ReadKey();
        }

        private static void Delete()
        {
            try
            {
                System.Console.WriteLine("--- DELETE ---\n");

                ListarTodos();

                System.Console.Write("\nId: ");
                int id = Convert.ToInt32(System.Console.ReadLine());

                System.Console.Write($"\nDeseja realmente apagar usuário Id = {id}? (y/n): ");
                var option = System.Console.ReadKey();

                if (option.Key == ConsoleKey.N)
                    return;

                using (var connection = new SqlConnection(Properties.Settings.Default.DefaultConnectionString))
                {
                    connection.Open();

                    string sql = $"DELETE FROM Usuario WHERE {Usuario.Id} = @Id";

                    var command = new SqlCommand(sql, connection);

                    command.Parameters.Add(new SqlParameter("@Id", id));

                    int result = command.ExecuteNonQuery();

                    System.Console.WriteLine(result > 0
                        ? $"\nUsuário Id = {id} foi removido."
                        : "\nNenhum usuário foi removido.");
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }

            System.Console.WriteLine("\n\nAperte qualquer tecla para continuar...");
            System.Console.ReadKey();
        }

        private static void ListarTodos()
        {
            using (var connection = new SqlConnection(Properties.Settings.Default.DefaultConnectionString))
            {
                connection.Open();

                var cmd = new SqlCommand($"SELECT {Usuario.Id}, {Usuario.Nome} FROM Usuario", connection);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows == false)
                        throw new Exception("A Tabela está vazia.");

                    System.Console.WriteLine("-- TABELA: Usuario --");

                    while (reader.Read())
                    {
                        string message = $"Id: {reader[Usuario.Id]} - Nome: {reader[Usuario.Nome]}";
                        System.Console.WriteLine(message);
                    }
                }
            }
        }
    }
}