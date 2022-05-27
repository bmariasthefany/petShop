using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Web.Models
{
    public class TipoProdutoModel
    {
        public short CodTipoProduto { get; set; }

        [Required(ErrorMessage = "Preencha o tipo.")]
        public string Tipo { get; set; }

        public bool Ativo { get; set; }

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select count(*) from TipoProduto";
                    ret = (int)comando.ExecuteScalar();
                }
            }

            return ret;
        }

        public static List<TipoProdutoModel> RecuperarLista(int pagina, int tamPagina)
        {
            var ret = new List<TipoProdutoModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    var pos = (pagina - 1) * tamPagina;

                    comando.Connection = conexao;
                    comando.CommandText = string.Format(
                        "select * from TipoProduto order by Tipo offset {0} rows fetch next {1} rows only",
                        pos > 0 ? pos - 1 : 0, tamPagina);
                    var reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ret.Add(new TipoProdutoModel
                        {
                            CodTipoProduto = (short)reader["CodTipoProduto"],
                            Tipo = (string)reader["Tipo"],
                            Ativo = (bool)reader["Ativo"]
                        });
                    }
                }
            }

            return ret;
        }

        public static TipoProdutoModel RecuperarPeloId(short CodTipoProduto)
        {
            TipoProdutoModel ret = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "select * from TipoProduto where (CodTipoProduto = @CodTipoProduto)";
                    comando.Parameters.Add("@CodTipoProduto", SqlDbType.SmallInt).Value = CodTipoProduto;
                    var reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        ret = new TipoProdutoModel
                        {
                            CodTipoProduto = (short)reader["CodTipoProduto"],
                            Tipo = (string)reader["Tipo"],
                            Ativo = (bool)reader["Ativo"]
                        };
                    }
                }
            }

            return ret;
        }

        public static bool ExcluirPeloId(short CodTipoProduto)
        {
            var ret = false;

            if (RecuperarPeloId(CodTipoProduto) != null)
            {
                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                    conexao.Open();
                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "delete from TipoProduto where (CodTipoProduto = @CodTipoProduto)";
                        comando.Parameters.Add("@CodTipoProduto", SqlDbType.SmallInt).Value = CodTipoProduto;
                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }

            return ret;
        }

        public int Salvar()
        {
            var ret = 0;

            var model = RecuperarPeloId(this.CodTipoProduto);

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["principal"].ConnectionString;
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    if (model == null)
                    {
                        comando.CommandText = "insert into TipoProduto (Tipo, Ativo) values (@Tipo, @Ativo); select convert(int, scope_identity())";

                        comando.Parameters.Add("@tipo", SqlDbType.VarChar).Value = this.Tipo;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (this.Ativo ? 1 : 0);
                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.CommandText = "update TipoProduto set Tipo=@tipo, Ativo=@ativo where CodTipoProduto = @CodTipoProduto";

                        comando.Parameters.Add("@tipo", SqlDbType.VarChar).Value = this.Tipo;
                        comando.Parameters.Add("@ativo", SqlDbType.VarChar).Value = (this.Ativo ? 1 : 0);
                        comando.Parameters.Add("@CodTipoProduto", SqlDbType.Int).Value = this.CodTipoProduto;

                        
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = this.CodTipoProduto;
                        }
                    }
                }
            }

            return ret;
        }
    }
}