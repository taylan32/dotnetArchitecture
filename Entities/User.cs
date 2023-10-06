using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class User : Entity
	{
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

		public virtual ICollection<UserOperationClaim>? UserOperationClaims { get; set; }
		public virtual RefreshToken? RefreshToken { get; set; }

        public User()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
            //RefreshToken = new RefreshToken();
        }

		public User(int id, string name, string lastName, string email, byte[] passwordSalt, byte[] passwordHash) : base(id)
		{
			Name = name;
			LastName = lastName;
			Email = email;
			PasswordSalt = passwordSalt;
			PasswordHash = passwordHash;
		}

	}
}
