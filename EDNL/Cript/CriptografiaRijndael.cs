using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace EDNL.Cript
{
    /// <summary>
    /// Classe de Criptografia Rijndael.
    /// </summary>	
    internal class CriptografiaRijndael
    {
        private string strKey = null;
        private string strIV = null;

        #region Implementação de ICriptografia 
        /// <summary>
        /// Gera o vetor de inicialização IV com 128 bits
        /// </summary>
        internal void GerarIV()
        {
            //Declaração de variáveis
            SymmetricAlgorithm objRijndael = null;
            string strIV = null;

            try
            {
                //Cria o objeto de criptografia default "Rijndael"
                objRijndael = SymmetricAlgorithm.Create();

                //Gera uma nova chave
                objRijndael.KeySize = 128;
                objRijndael.GenerateIV();

                //Converte a chave para base64
                strIV = Convert.ToBase64String(objRijndael.IV);
                //**************************//
                //							//
                //**************************//
            }
            catch (Exception p_objErro)
            {
                throw p_objErro;
            }
        }

        /// <summary>
        /// Gera chave de criptografia Key com 128 bits
        /// </summary>
        internal void GerarKey()
        {
            //Declaração de variáveis
            SymmetricAlgorithm objRijndael = null;
            string strKey = null;

            try
            {
                //Cria o objeto de criptografia default "Rijndael"
                objRijndael = SymmetricAlgorithm.Create();

                //Gera uma nova chave
                objRijndael.KeySize = 128;
                objRijndael.GenerateKey();

                //Converte a chave para base64
                strKey = Convert.ToBase64String(objRijndael.Key);
                //**************************//
                //							//
                //**************************//
            }
            catch (Exception p_objErro)
            {
                throw p_objErro;
            }
        }

        /// <summary>
		/// Decriptografa a string 
		/// </summary>
		/// <param name="pdstCriptografia">string criptografada</param>
		/// <returns>string decriptografada</returns>
        internal string Decriptografar(string p_strValorCriptografado)
        {
            //Declaração de variáveis
            MemoryStream objStream = null;
            CryptoStream objCriptoStream = null;
            SymmetricAlgorithm objRijndael = null;
            string strCriptografia = null;
            byte[] aryString;

            //Objeto responsável pela manipulação dos dados
            objStream = new MemoryStream();

            //Recupera o valor do dataset
            strCriptografia = p_strValorCriptografado;

            //Monta uma array de bytes com o texto a ser criptografado
            aryString = Convert.FromBase64String(strCriptografia);

            //Cria o objeto de criptografia default "Rijndael"
            objRijndael = SymmetricAlgorithm.Create();

            //Atribui as chaves
            objRijndael.Key = Convert.FromBase64String(CriptografiaInfo.KEY);
            objRijndael.IV = Convert.FromBase64String(CriptografiaInfo.IV);

            //Objeto responsável pela conversão
            objCriptoStream = new CryptoStream(objStream, objRijndael.CreateDecryptor(objRijndael.Key, objRijndael.IV), CryptoStreamMode.Write);

            //Decriptografa a string
            objCriptoStream.Write(aryString, 0, aryString.Length);
            objCriptoStream.FlushFinalBlock();
            objCriptoStream.Close();

            //Converte o retorno para o formato UTF-8
            strCriptografia = Encoding.UTF8.GetString(objStream.ToArray());

            //Retorna o dataset com o resultado
            return strCriptografia;
        }

        /// <summary>
        /// Criptografar a string do dataset 
        /// </summary>
        /// <param name="pdstCriptografia">Dataset com a 
        /// string a ser criptografada</param>
        /// <returns>String criptografada</returns>
        internal string Criptografar(string p_strValor)
        {
            //Declaração de variáveis
            MemoryStream objStream = null;
            CryptoStream objCriptoStream = null;
            SymmetricAlgorithm objRijndael = null;
            string strCriptografia = null;
            byte[] aryString;

            //Objeto responsável pela manipulação dos dados
            objStream = new MemoryStream();

            //Recupera o valor do dataset
            strCriptografia = p_strValor;

            //Monta uma array de bytes com o texto a ser criptografado
            aryString = Encoding.UTF8.GetBytes(strCriptografia);

            //Cria o objeto de criptografia default "Rijndael"
            objRijndael = SymmetricAlgorithm.Create();

            //Atribui as chaves
            objRijndael.Key = Convert.FromBase64String(CriptografiaInfo.KEY);
            objRijndael.IV = Convert.FromBase64String(CriptografiaInfo.IV);

            //Objeto responsável pela conversão
            objCriptoStream = new CryptoStream(objStream, objRijndael.CreateEncryptor(objRijndael.Key, objRijndael.IV), CryptoStreamMode.Write);

            //Criptografa a string
            objCriptoStream.Write(aryString, 0, aryString.Length);
            objCriptoStream.FlushFinalBlock();
            objCriptoStream.Close();

            //Converte o retorno para base 64
            strCriptografia = Convert.ToBase64String(objStream.ToArray());

            //Retorna o objeto
            return strCriptografia;
        }

        /// <summary>
        /// Fornece a chave de criptografia Key
        /// </summary>
        /// <returns>Key de criptografia no formato base64</returns>
        internal string RecuperarKey()
        {
            return strKey;
        }
        /// <summary>
        /// Fornece o vetor de inicialização IV
        /// </summary>
        /// <returns>Vetor de inivialização no formato base64</returns>
        internal string RecuperarIV()
        {
            return strIV;
        }
        #endregion
    }
}
