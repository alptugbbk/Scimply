﻿using DbScimplyAPI.Application.Abstractions.Cryptographies;
using System.Security.Cryptography;
using System.Text;


namespace DbScimplyAPI.Persistence.Concretes.Cryptographies
{
    public class PasswordGenerator : IPasswordGenerator
    {

        private const string _characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int _passwordLenght = 10;
        private const int _twoFactorLenght = 4;


        public string GeneratePassword()
        {

            Random random = new Random();

            var password = "";

            for(int i = 0; i < _passwordLenght; i++)
            {
                int characterRange = random.Next(0, _characters.Length);
                password += _characters[characterRange];
            }

            return password;

        }



		public string GenerateTwoFactor()
		{

			var rnd = new Random();

            var code = "";

            for (int i = 0; i < _twoFactorLenght; i++)
            {
                var digit = rnd.Next(0, 9);
                code += digit;
            }

            return code;

		}



		public string SHAEncrypt(string password, string id)
        {

            var combined = password + id;

            using(var sha = SHA512.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(combined));

                return Convert.ToBase64String(bytes);
            }


        }


    }
}
