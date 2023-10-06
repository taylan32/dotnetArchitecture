using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class RefreshToken : Entity
	{

        public int UserId { get; set; }
		public string? Token { get; set; }
		public DateTime Expires { get; set; }
		public DateTime Created { get; set; }
		public User? User { get; set; }


        public RefreshToken()
        {
            
        }

		public RefreshToken(int id, int userId, string? token, DateTime expires, DateTime created) : base(id)
		{
			UserId = userId;
			Token = token;
			Expires = expires;
			Created = created;
		}
	}
}
