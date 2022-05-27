using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PetShop.Web.Models
{
    public class UsuarioModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Informe o login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }
        public static UsuarioModel ValidarUsuario(string login, string senha)
        {
            UsuarioModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from Usuario where login=@login and senha=@senha";
                    comando.Parameters.Add("@login", SqlDbType.VarChar).Value = login;
                    comando.Parameters.Add("@senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(senha);
                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new UsuarioModel
                        {
                            Id = (short)reader["id"],
                            Login = (string)reader["login"],
                            Senha = (string)reader["senha"],
                            Nome = (string)reader["nome"]
                        };
                    }

                }
            }
            return ret;
        }
        public static List<UsuarioModel> RecuperarLista()
        {
            var ret = new List<UsuarioModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from Usuario order by Nome";
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new UsuarioModel
                        {
                            Id = (short)reader["id"],
                            Nome = (string)reader["nome"],
                            Login = (string)reader["login"]
                        });
                    }
                }
            }

            return ret;
        }

        public static UsuarioModel RecuperarPeloId(short Id)
        {
            UsuarioModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from Usuario where (Id = @Id)";
                    comando.Parameters.Add("@id", SqlDbType.SmallInt).Value = Id;
                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new UsuarioModel
                        {
                            Id = (short)reader["Id"],
                            Nome = (string)reader["nome"],
                            Login = (string)reader["login"]
                        };
                    }
                }
            }

            return ret;
        }

        public static bool ExcluirPeloId(short Id)
        {
            var ret = false;

            if (RecuperarPeloId(Id) != null)
            {
                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "delete from Usuario where (Id = @Id)";
                        comando.Parameters.Add("@Id", SqlDbType.SmallInt).Value = Id;
                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }

            return ret;
        }

        public int Salvar()
        {
            var ret = 0;

            var model = RecuperarPeloId(this.Id);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (model == null)
                    {
                        comando.CommandText = "insert into Usuario (Nome, Login, Senha) values (@Nome, @Login, @Senha); select convert(int, scope_identity())";

                        comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@Login", SqlDbType.VarChar).Value = this.Login;
                        comando.Parameters.Add("@Senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(this.Senha);
                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = 
                            "update Usuario set Nome=@Nome, Login=@Login" +  
                            (!string.IsNullOrEmpty(this.Senha) ? ", Senha=@Senha" : "") + 
                            " where Id = @Id";

                        comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = this.Nome;
                        comando.Parameters.Add("@Login", SqlDbType.VarChar).Value = this.Login;

                        if (!string.IsNullOrEmpty(this.Senha))
                        {
                            comando.Parameters.Add("@Senha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(this.Senha);
                        }
                        comando.Parameters.Add("@Id", SqlDbType.SmallInt).Value = this.Id;

                        if(comando.ExecuteNonQuery() > 0)
                        {
                            ret = this.Id;
                        }
                    }
                }
            }

            return ret;
        }
    }
}
