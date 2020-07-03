using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

namespace Toolkit
{
    public static class CryptoUtility
    {
        /// <summary>
        /// SHA256字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateSHA256(string txt)
        {
            using (SHA256Managed sha256 = new SHA256Managed())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                // 开始加密
                byte[] hash = sha256.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }

        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                // 开始加密
                byte[] hash = md5.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }

        /// <summary>
        /// SHA256流加密
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static string GenerateSHA256(Stream inputStream)
        {
            using (SHA256Managed sha256 = new SHA256Managed())
            {
                // 开始加密
                byte[] hash = sha256.ComputeHash(inputStream);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }

        /// <summary>
        /// MD5流加密
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static string GenerateMD5(Stream inputStream)
        {
            using (MD5 md5 = MD5.Create())
            {
                // 开始加密
                byte[] hash = md5.ComputeHash(inputStream);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }
    }
}