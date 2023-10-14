using System;
using System.Data;
using System.Data.SqlClient;

namespace Contatos
{
    class Program
    {
        static string connectionString = "Data Source=localhost;Initial Catalog=AGENDA;Integrated Security=True";

        static void Main(string[] args)
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("----- Cadastro de Contatos -----");
                Console.WriteLine("1 - Adicionar Contato");
                Console.WriteLine("2 - Listar Contatos");
                Console.WriteLine("3 - Sair");
                Console.WriteLine("Escolha uma opcao: ");

                string escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        AdicionarContato();
                        break;
                    case "2":
                        ListarContatos();
                        break;
                    case "3":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opcao invalida... Tente Novamente.");
                        break;
                }
            }
        }

        static void AdicionarContato()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO CONTATOS (nome, telefone, email) VALUES (@nome, @telefone, @email)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@telefone", telefone);
                    command.Parameters.AddWithValues("@Email", email);

                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Contato Salvo");
        }

        static void ListarContatos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT nome, telefone, email FROM CONTATO";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Lista de Contatos: ");
                            while (reader.Read())
                            {
                                Console.WriteLine($"Nome: {reader["nome"]}");
                                Console.WriteLine($"Telefone: {reader["telefone"]}");
                                Console.WriteLine($"Email: {reader["email"]}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum Contato Adicionado");
                        }
                    }
                }
            }
        }

    }
}
