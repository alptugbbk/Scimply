using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.Cryptographies
{
	public interface IAESEncryption
	{

		string EncryptData(string plainText);

		string DecryptData(string cipherText);

	}
}
